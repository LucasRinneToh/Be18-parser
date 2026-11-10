namespace Be18_parser.Models;

/// <summary>Internal heat gains and special equipment of the building (<c>USAGE</c>).</summary>
[Be18Entity("USAGE")]
public sealed class Usage : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Power of special equipment, including any cooling of it, during the usage time in W ("Særligt apparatur, i brugstiden"). Not part of the energy demand (<c>app_use</c>).</summary>
    [Be18Field("app_use")]
    public double SpecialEquipmentInUse { get; set; }

    /// <summary>Power of special equipment, including any cooling of it, that is always on in W ("Særligt apparatur, altid i brug"). Not part of the energy demand (<c>app_always</c>).</summary>
    [Be18Field("app_always")]
    public double SpecialEquipmentAlways { get; set; }

    /// <summary>Internal heat gain zones ("Internt varmetilskud") (<c>has_usage_load</c>).</summary>
    [Be18Field("has_usage_load")]
    public List<UsageLoad> Loads { get; set; } = [];
}
