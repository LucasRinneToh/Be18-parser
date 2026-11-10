using System.Runtime.CompilerServices;

namespace Be18_parser.Models;

/// <summary>
/// Maps a property to a field (child element) of an entity in a Be18 file.
/// </summary>
/// <remarks>
/// <para>
/// Fields are written in the order they are declared: fields of a base class first, then the
/// fields of the derived class in source order. <see cref="Order"/> is filled in by the compiler.
/// </para>
/// <para>How property types map to field values:</para>
/// <list type="bullet">
/// <item><see cref="string"/>: the text as is.</item>
/// <item><see cref="double"/> and <see cref="int"/>: a number with <c>.</c> as decimal separator.
/// Nullable numbers are written as an empty element when <see langword="null"/>.</item>
/// <item><see cref="bool"/>: <c>.T.</c> or <c>.F.</c>.</item>
/// <item><see cref="IBe18Enum"/>: the value surrounded by dots, e.g. <c>.SUP_EL.</c>.</item>
/// <item>A <see cref="Be18Entity"/>: a reference such as <c>#12</c>, or <c>$</c> when <see langword="null"/>.</item>
/// <item>A list of entities: space-separated references such as <c>#3 #4 #5</c>, or an empty
/// element when the list is empty. A <see langword="null"/> item is written as <c>$</c>.</item>
/// </list>
/// </remarks>
/// <param name="name">Element name of the field, e.g. <c>th_heat</c>.</param>
/// <param name="order">Source line of the property; supplied by the compiler.</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class Be18FieldAttribute(string name, [CallerLineNumber] int order = 0) : Attribute
{
    /// <summary>Element name of the field, e.g. <c>th_heat</c>.</summary>
    public string Name { get; } = name;

    /// <summary>Position of the field within its declaring class.</summary>
    public int Order { get; } = order;
}
