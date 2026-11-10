namespace Be18_parser.Models;

/// <summary>A pipe in the heat distribution system (<c>HEAT_TUBE</c>). Covers pipes without outdoor temperature compensation and pipes outside the heated part of the building.</summary>
[Be18Entity("HEAT_TUBE")]
public sealed class HeatDistributionPipe : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Length in m ("l") (<c>len</c>).</summary>
    [Be18Field("len")]
    public double Length { get; set; }

    /// <summary>Linear heat loss in W/mK ("Tab") (<c>psi</c>).</summary>
    [Be18Field("psi")]
    public double LinearLoss { get; set; }

    /// <summary>b-factor for heat loss from the pipe ("b") (<c>b</c>).</summary>
    [Be18Field("b")]
    public double BFactor { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }

    /// <summary>Whether the flow temperature is compensated for the outdoor temperature ("Udekomp.") (<c>outdoor_comp</c>).</summary>
    [Be18Field("outdoor_comp")]
    public bool HasOutdoorCompensation { get; set; }

    /// <summary>Whether the pipe is shut off in summer ("Afb. sommer") (<c>closed_summer</c>).</summary>
    [Be18Field("closed_summer")]
    public bool ShutOffInSummer { get; set; }
}
