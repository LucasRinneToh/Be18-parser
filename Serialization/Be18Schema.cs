using System.Reflection;
using Be18_parser.Models;

namespace Be18_parser.Serialization;

/// <summary>How a field value is stored in the file.</summary>
internal enum FieldKind
{
    String,
    Double,
    Int,
    Bool,
    Enum,
    Reference,
    ReferenceList,
}

/// <summary>A property mapped to a field with <see cref="Be18FieldAttribute"/>.</summary>
internal sealed class FieldSchema(string name, PropertyInfo property, FieldKind kind, Type valueType, bool isNullable)
{
    /// <summary>Element name of the field.</summary>
    public string Name { get; } = name;

    public PropertyInfo Property { get; } = property;

    public FieldKind Kind { get; } = kind;

    /// <summary>The value type without <see cref="Nullable{T}"/>, or the item type of a reference list.</summary>
    public Type ValueType { get; } = valueType;

    /// <summary>Whether the value (or, for a reference list, an item) can be <see langword="null"/>.</summary>
    public bool IsNullable { get; } = isNullable;
}

/// <summary>A model class mapped to an entity type with <see cref="Be18EntityAttribute"/>.</summary>
internal sealed class EntitySchema
{
    public EntitySchema(Type type, string elementName, IReadOnlyList<FieldSchema> fields)
    {
        Type = type;
        ElementName = elementName;
        Fields = fields;
        FieldsByName = fields.ToDictionary(f => f.Name);
    }

    public Type Type { get; }

    public string ElementName { get; }

    /// <summary>Fields in the order they are written: base class first, then declaration order.</summary>
    public IReadOnlyList<FieldSchema> Fields { get; }

    public IReadOnlyDictionary<string, FieldSchema> FieldsByName { get; }

    public Be18Entity Create() => (Be18Entity)Activator.CreateInstance(Type)!;
}

/// <summary>Reflection cache of the model classes, built once.</summary>
internal static class Be18Schema
{
    private static readonly Dictionary<Type, EntitySchema> ByType = [];
    private static readonly Dictionary<string, EntitySchema> ByElementName = [];

    static Be18Schema()
    {
        var types = typeof(Be18Entity).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && typeof(Be18Entity).IsAssignableFrom(t));

        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<Be18EntityAttribute>();
            if (attribute == null)
                continue;

            var schema = new EntitySchema(type, attribute.Name, GetFields(type));
            ByType.Add(type, schema);
            ByElementName.Add(attribute.Name, schema);
        }
    }

    public static bool TryGet(string elementName, out EntitySchema schema)
        => ByElementName.TryGetValue(elementName, out schema!);

    public static EntitySchema Get(Type type)
        => ByType.TryGetValue(type, out var schema)
            ? schema
            : throw new InvalidOperationException($"{type.Name} has no [Be18Entity] attribute.");

    private static List<FieldSchema> GetFields(Type type)
    {
        var chain = new List<Type>();
        for (var t = type; t != null && t != typeof(object); t = t.BaseType)
            chain.Insert(0, t);

        var nullability = new NullabilityInfoContext();
        return chain
            .SelectMany(t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(p => (Property: p, Attribute: p.GetCustomAttribute<Be18FieldAttribute>()))
                .Where(x => x.Attribute != null)
                .OrderBy(x => x.Attribute!.Order))
            .Select(x => CreateField(x.Attribute!.Name, x.Property, nullability))
            .ToList();
    }

    private static FieldSchema CreateField(string name, PropertyInfo property, NullabilityInfoContext nullability)
    {
        var type = property.PropertyType;
        var underlying = Nullable.GetUnderlyingType(type);
        var info = nullability.Create(property);

        if (type == typeof(string))
            return new FieldSchema(name, property, FieldKind.String, type, false);
        if ((underlying ?? type) == typeof(double))
            return new FieldSchema(name, property, FieldKind.Double, typeof(double), underlying != null);
        if ((underlying ?? type) == typeof(int))
            return new FieldSchema(name, property, FieldKind.Int, typeof(int), underlying != null);
        if (type == typeof(bool))
            return new FieldSchema(name, property, FieldKind.Bool, type, false);
        if (typeof(IBe18Enum).IsAssignableFrom(type))
            return new FieldSchema(name, property, FieldKind.Enum, type, false);
        if (typeof(Be18Entity).IsAssignableFrom(type))
            return new FieldSchema(name, property, FieldKind.Reference, type, info.ReadState == NullabilityState.Nullable);
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)
            && typeof(Be18Entity).IsAssignableFrom(type.GetGenericArguments()[0]))
        {
            var itemNullable = info.GenericTypeArguments[0].ReadState == NullabilityState.Nullable;
            return new FieldSchema(name, property, FieldKind.ReferenceList, type.GetGenericArguments()[0], itemNullable);
        }

        throw new InvalidOperationException(
            $"{property.DeclaringType!.Name}.{property.Name} has a type that cannot be stored in a Be18 file: {type.Name}.");
    }
}
