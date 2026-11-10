namespace Be18_parser.Models;

/// <summary>Maps a model class to the element name of an entity type in a Be18 file.</summary>
/// <param name="name">Element name in the file, e.g. <c>BUILDING</c>.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class Be18EntityAttribute(string name) : Attribute
{
    /// <summary>Element name in the file, e.g. <c>BUILDING</c>.</summary>
    public string Name { get; } = name;
}
