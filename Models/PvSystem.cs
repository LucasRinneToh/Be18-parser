namespace Be18_parser.Models;

/// <summary>A solar cell (photovoltaic) system (<c>PV_CELL</c>).</summary>
[Be18Entity("PV_CELL")]
public sealed class PvSystem : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Panel area in m² ("Panelareal") (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>Orientation as Danish compass letters, e.g. <c>s</c> (<c>orient</c>).</summary>
    [Be18Field("orient")]
    public string Orientation { get; set; } = "";

    /// <summary>Slope in degrees from horizontal ("Hældning") (<c>slope</c>).</summary>
    [Be18Field("slope")]
    public double Slope { get; set; }

    /// <summary>Horizon angle in degrees ("Horisontafsk.") (<c>horizon</c>).</summary>
    [Be18Field("horizon")]
    public double Horizon { get; set; }

    /// <summary>Peak power at reference conditions in kW per m² ("Peak power (RS)") (<c>peak_power</c>).</summary>
    [Be18Field("peak_power")]
    public double PeakPower { get; set; }

    /// <summary>Efficiency of the system ("Systemets virkningsgrad") (<c>efficiency</c>).</summary>
    [Be18Field("efficiency")]
    public double SystemEfficiency { get; set; }

    /// <summary>Left side shading angle in degrees ("Skygge v.") (<c>sidefin_left</c>).</summary>
    [Be18Field("sidefin_left")]
    public double SideFinLeft { get; set; }

    /// <summary>Right side shading angle in degrees ("Skygge h.") (<c>sidefin_right</c>).</summary>
    [Be18Field("sidefin_right")]
    public double SideFinRight { get; set; }
}
