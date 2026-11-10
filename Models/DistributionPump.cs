namespace Be18_parser.Models;

/// <summary>A pump in the heat distribution system (<c>DIST_PUMP</c>).</summary>
[Be18Entity("DIST_PUMP")]
public sealed class DistributionPump : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Nominal power in W ("Pnom") (<c>pnom</c>).</summary>
    [Be18Field("pnom")]
    public double NominalPower { get; set; }

    /// <summary>"Fp" in Be; meaning not yet confirmed (<c>fp</c>).</summary>
    [Be18Field("fp")]
    public double Fp { get; set; }

    /// <summary>Number of identical pumps ("Antal") (<c>no</c>).</summary>
    [Be18Field("no")]
    public int Count { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }
}
