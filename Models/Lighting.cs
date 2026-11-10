namespace Be18_parser.Models;

/// <summary>Lighting of the building (<c>LIGHTING</c>).</summary>
[Be18Entity("LIGHTING")]
public sealed class Lighting : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Daylight-controlled outdoor lighting in W ("Udebelysning"). Not part of the energy demand (<c>outdoor</c>).</summary>
    [Be18Field("outdoor")]
    public double OutdoorLighting { get; set; }

    /// <summary>Lighting zones ("Belysning") (<c>has_light_load</c>).</summary>
    [Be18Field("has_light_load")]
    public List<LightingZone> Zones { get; set; } = [];

    /// <summary>Lighting in parking basements and similar ("Belysning i parkeringskælder mv."); no example file has any yet (<c>has_light_add</c>).</summary>
    [Be18Field("has_light_add")]
    public List<Be18Entity> Additions { get; set; } = [];
}
