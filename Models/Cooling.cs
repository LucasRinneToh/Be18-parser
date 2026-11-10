namespace Be18_parser.Models;

/// <summary>Mechanical cooling (<c>COOLING</c>).</summary>
[Be18Entity("COOLING")]
public sealed class Cooling : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary><c>efficiency</c> (meaning not yet confirmed).</summary>
    [Be18Field("efficiency")]
    public double Efficiency { get; set; }

    /// <summary><c>additional_cooling</c> (meaning not yet confirmed).</summary>
    [Be18Field("additional_cooling")]
    public double AdditionalCooling { get; set; }

    /// <summary><c>documentation</c> (meaning not yet confirmed).</summary>
    [Be18Field("documentation")]
    public string Documentation { get; set; } = "";

    /// <summary><c>load_fac</c> (meaning not yet confirmed).</summary>
    [Be18Field("load_fac")]
    public double LoadFac { get; set; }

    /// <summary><c>el_dem</c> (meaning not yet confirmed).</summary>
    [Be18Field("el_dem")]
    public double ElDem { get; set; }

    /// <summary><c>heat_cap_pc</c> (meaning not yet confirmed).</summary>
    [Be18Field("heat_cap_pc")]
    public double HeatCapPc { get; set; }

    /// <summary><c>heat_dem</c> (meaning not yet confirmed).</summary>
    [Be18Field("heat_dem")]
    public double HeatDem { get; set; }
}
