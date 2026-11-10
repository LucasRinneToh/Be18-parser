namespace Be18_parser.Models;

/// <summary>A domestic hot water distribution pipe (<c>DHW_TUBE</c>).</summary>
[Be18Entity("DHW_TUBE")]
public sealed class HotWaterPipe : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Length in m (<c>len</c>).</summary>
    [Be18Field("len")]
    public double Length { get; set; }

    /// <summary>Linear heat loss in W/mK (<c>psi</c>).</summary>
    [Be18Field("psi")]
    public double LinearLoss { get; set; }

    /// <summary>b-factor for heat loss from the pipe (<c>b</c>).</summary>
    [Be18Field("b")]
    public double BFactor { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }
}
