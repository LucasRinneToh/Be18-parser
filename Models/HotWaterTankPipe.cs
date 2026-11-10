namespace Be18_parser.Models;

/// <summary>A connection pipe between the heat source and a hot water tank (<c>DHW_PIPE</c>).</summary>
[Be18Entity("DHW_PIPE")]
public sealed class HotWaterTankPipe : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Description of the pipe (<c>tube_name</c>).</summary>
    [Be18Field("tube_name")]
    public string PipeName { get; set; } = "";

    /// <summary>Length in m (<c>tube_len</c>).</summary>
    [Be18Field("tube_len")]
    public double Length { get; set; }

    /// <summary>Linear heat loss in W/mK (<c>tube_psi</c>).</summary>
    [Be18Field("tube_psi")]
    public double LinearLoss { get; set; }

    /// <summary>b-factor for heat loss from the pipe (<c>tube_b</c>).</summary>
    [Be18Field("tube_b")]
    public double BFactor { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }
}
