namespace Be18_parser.Models;

/// <summary>Header entity of a Be18 file (the <c>BE05</c> entity, normally <c>#1</c>). Its <see cref="Be18Entity.Name"/> holds the program version that wrote the file, e.g. <c>Be15, version 8.16.2.4</c>.</summary>
[Be18Entity("BE05")]
public sealed class ModelHeader : Be18Entity
{
    /// <summary>The building described by the file (<c>has_building</c>).</summary>
    [Be18Field("has_building")]
    public Building? Building { get; set; }

    /// <summary>Climate data; no example file sets it yet (<c>has_climate</c>).</summary>
    [Be18Field("has_climate")]
    public Be18Entity? Climate { get; set; }
}
