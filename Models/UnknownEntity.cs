namespace Be18_parser.Models;

/// <summary>
/// An entity whose type this library does not model yet. All its fields, including <c>id</c>, are
/// kept in <see cref="Be18Entity.ExtraFields"/> so it survives a round trip.
/// </summary>
public sealed class UnknownEntity : Be18Entity
{
    /// <summary>Creates an unknown entity with the given element name.</summary>
    /// <param name="typeName">Element name of the entity in the file, e.g. <c>CLIMATE</c>.</param>
    public UnknownEntity(string typeName) => TypeName = typeName;

    /// <summary>Element name of the entity in the file, e.g. <c>CLIMATE</c>.</summary>
    public string TypeName { get; }
}
