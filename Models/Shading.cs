namespace Be18_parser.Models;

/// <summary>Shading of windows by surroundings (<c>SHADING</c>). Can be shared by several windows.</summary>
[Be18Entity("SHADING")]
public sealed class Shading : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Horizon angle in degrees ("Horisont") (<c>horizon</c>).</summary>
    [Be18Field("horizon")]
    public double Horizon { get; set; }

    /// <summary>Overhang angle in degrees ("Udhæng") (<c>overhang</c>).</summary>
    [Be18Field("overhang")]
    public double Overhang { get; set; }

    /// <summary>Left side shading angle in degrees ("Venstre") (<c>sidefin_left</c>).</summary>
    [Be18Field("sidefin_left")]
    public double SideFinLeft { get; set; }

    /// <summary>Right side shading angle in degrees ("Højre") (<c>sidefin_right</c>).</summary>
    [Be18Field("sidefin_right")]
    public double SideFinRight { get; set; }

    /// <summary>Window reveal in %: the smaller of depth/width and depth/height, times 100 ("Vindueshul") (<c>opening</c>).</summary>
    [Be18Field("opening")]
    public double WindowReveal { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }
}
