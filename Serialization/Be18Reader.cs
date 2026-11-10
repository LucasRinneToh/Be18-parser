using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Be18_parser.Models;

namespace Be18_parser.Serialization;

/// <summary>Builds a <see cref="Be18Document"/> from the XML of a Be18 file.</summary>
internal static class Be18Reader
{
    public static Be18Document Read(Stream stream)
    {
        XDocument xml;
        try
        {
            xml = XDocument.Load(stream, LoadOptions.None);
        }
        catch (XmlException ex)
        {
            throw new Be18FormatException($"The file is not valid XML: {ex.Message}", ex);
        }

        return Read(xml);
    }

    public static Be18Document Read(XDocument xml)
    {
        var root = xml.Root;
        if (root == null || root.Name.LocalName != "BE05")
            throw new Be18FormatException($"The root element must be <BE05>, but is <{root?.Name.LocalName}>.");

        // The rids come from the file, so entities created while reading do not use the counter.
        Be18Document document;
        var byRid = new Dictionary<int, Be18Entity>();
        var elements = new List<(XElement Element, Be18Entity Entity)>();
        using (Be18Entity.SuppressRidCounter())
        {
            document = new Be18Document();
            document.FileComments.AddRange(xml.Nodes().TakeWhile(n => n != root).OfType<XComment>().Select(c => c.Value));

            // First create every entity, so references can be resolved in any order.
            foreach (var element in root.Elements())
            {
                var rid = ParseRid(element);
                var entity = Be18Schema.TryGet(element.Name.LocalName, out var schema)
                    ? schema.Create()
                    : new UnknownEntity(element.Name.LocalName);
                entity.Rid = rid;

                if (!byRid.TryAdd(rid, entity))
                    throw new Be18FormatException($"<{element.Name.LocalName} rid=\"#{rid}\">: another entity has the same rid.");

                document.Entities.Add(entity);
                elements.Add((element, entity));
            }
        }

        // Entities created after loading must not reuse a rid from this file.
        if (byRid.Count > 0)
            Be18Entity.EnsureRidCounterAbove(byRid.Keys.Max());

        foreach (var (element, entity) in elements)
            ReadFields(element, entity, byRid);

        document.Header = document.Entities.OfType<ModelHeader>().FirstOrDefault()
            ?? throw new Be18FormatException("The file has no BE05 header entity.");
        return document;
    }

    private static int ParseRid(XElement element)
    {
        var rid = (string?)element.Attribute("rid");
        if (rid == null || !rid.StartsWith('#')
            || !int.TryParse(rid.AsSpan(1), NumberStyles.None, CultureInfo.InvariantCulture, out var value))
            throw new Be18FormatException($"<{element.Name.LocalName}>: missing or invalid rid attribute '{rid}'.");

        return value;
    }

    private static void ReadFields(XElement element, Be18Entity entity, Dictionary<int, Be18Entity> byRid)
    {
        if (entity is UnknownEntity)
        {
            foreach (var child in element.Elements())
                entity.ExtraFields.Add(new Be18RawField(child.Name.LocalName, child.Value));
            return;
        }

        var schema = Be18Schema.Get(entity.GetType());
        foreach (var child in element.Elements())
        {
            var name = child.Name.LocalName;
            if (!schema.FieldsByName.TryGetValue(name, out var field))
            {
                entity.ExtraFields.Add(new Be18RawField(name, child.Value));
                continue;
            }

            try
            {
                field.Property.SetValue(entity, ParseValue(field, child.Value, byRid));
            }
            catch (FormatException ex)
            {
                throw new Be18FormatException(
                    $"<{schema.ElementName} rid=\"#{entity.Rid}\"> field <{name}>: {ex.Message}", ex);
            }
        }
    }

    private static object? ParseValue(FieldSchema field, string value, Dictionary<int, Be18Entity> byRid)
    {
        switch (field.Kind)
        {
            case FieldKind.String:
                return value;

            case FieldKind.Double:
                if (value.Length == 0 && field.IsNullable)
                    return null;
                return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var number)
                    ? number
                    : throw new FormatException($"'{value}' is not a number.");

            case FieldKind.Int:
                if (value.Length == 0 && field.IsNullable)
                    return null;
                return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var integer)
                    ? integer
                    : throw new FormatException($"'{value}' is not a whole number.");

            case FieldKind.Bool:
                return value switch
                {
                    ".T." => true,
                    ".F." => false,
                    _ => throw new FormatException($"'{value}' is not .T. or .F.."),
                };

            case FieldKind.Enum:
                if (value.Length < 3 || value[0] != '.' || value[^1] != '.')
                    throw new FormatException($"'{value}' is not an enumeration value such as .VALUE..");
                return Activator.CreateInstance(field.ValueType, value[1..^1]);

            case FieldKind.Reference:
                return value is "" or "$" ? null : Resolve(value, field.ValueType, byRid);

            case FieldKind.ReferenceList:
                var list = (IList)Activator.CreateInstance(field.Property.PropertyType)!;
                foreach (var item in value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (item == "$" && !field.IsNullable)
                        throw new FormatException("the list contains $, which this field does not allow.");
                    list.Add(item == "$" ? null : Resolve(item, field.ValueType, byRid));
                }
                return list;

            default:
                throw new InvalidOperationException($"Unhandled field kind {field.Kind}.");
        }
    }

    private static Be18Entity Resolve(string reference, Type type, Dictionary<int, Be18Entity> byRid)
    {
        if (!reference.StartsWith('#')
            || !int.TryParse(reference.AsSpan(1), NumberStyles.None, CultureInfo.InvariantCulture, out var rid))
            throw new FormatException($"'{reference}' is not a reference such as #12.");
        if (!byRid.TryGetValue(rid, out var entity))
            throw new FormatException($"{reference} does not refer to an entity in the file.");
        if (!type.IsInstanceOfType(entity))
            throw new FormatException($"{reference} refers to a {entity.GetType().Name}, but a {type.Name} is expected.");

        return entity;
    }
}
