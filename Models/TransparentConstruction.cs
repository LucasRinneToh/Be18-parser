namespace Be18_parser.Models;

/// <summary>A window or door (<c>TRANSP_CONSTR</c>). <see cref="BuildingElement.Area"/> is the area of one unit.</summary>
[Be18Entity("TRANSP_CONSTR")]
public sealed class TransparentConstruction : BuildingElement
{
    /// <summary>Number of identical units ("Antal") (<c>no</c>).</summary>
    [Be18Field("no")]
    public int Count { get; set; }

    /// <summary>Orientation as Danish compass letters, e.g. <c>n</c>, <c>s</c>, <c>ø</c>, <c>v</c> or <c>sv</c> (<c>orient</c>).</summary>
    [Be18Field("orient")]
    public string Orientation { get; set; } = "";

    /// <summary>Slope in degrees from horizontal ("Hældning") (<c>slope</c>).</summary>
    [Be18Field("slope")]
    public double Slope { get; set; }

    /// <summary>Glazing fraction of the unit area ("Glasandel, Ff") (<c>ff</c>).</summary>
    [Be18Field("ff")]
    public double GlazingFraction { get; set; }

    /// <summary>Total solar energy transmittance of the glazing ("Solvarmetransmittans, g") (<c>g</c>).</summary>
    [Be18Field("g")]
    public double GValue { get; set; }

    /// <summary>Reduction factor for solar shading devices ("Afskærmning, Fc") (<c>fc</c>).</summary>
    [Be18Field("fc")]
    public double ShadingFactor { get; set; }

    /// <summary>Whether the window is included in the summer comfort (overheating) check ("Ot") (<c>ot</c>).</summary>
    [Be18Field("ot")]
    public bool IncludeInSummerComfort { get; set; }

    /// <summary>Shading by surroundings ("Skygger") (<c>has_shading</c>).</summary>
    [Be18Field("has_shading")]
    public Shading? Shading { get; set; }
}
