namespace Be18_parser.Models;

/// <summary>Lighting in one zone of the building (<c>LIGHT_LOAD</c>).</summary>
[Be18Entity("LIGHT_LOAD")]
public sealed class LightingZone : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Minimum general lighting power in W/m² ("Belysning min.") (<c>gen_min</c>).</summary>
    [Be18Field("gen_min")]
    public double MinimumPower { get; set; }

    /// <summary>Installed general lighting power in W/m² ("Belysning inst.") (<c>gen_max</c>).</summary>
    [Be18Field("gen_max")]
    public double InstalledPower { get; set; }

    /// <summary>Illuminance in lux ("Belysning") (<c>light</c>).</summary>
    [Be18Field("light")]
    public double Illuminance { get; set; }

    /// <summary>Daylight factor in % ("Dagslysfaktor (DF)") (<c>df</c>).</summary>
    [Be18Field("df")]
    public double DaylightFactor { get; set; }

    /// <summary>Usage factor ("Benyttelsesfaktor (Fo)") (<c>fo</c>).</summary>
    [Be18Field("fo")]
    public double UsageFactor { get; set; }

    /// <summary>Work lighting in W/m² ("Arbejdsbelysning") (<c>work</c>).</summary>
    [Be18Field("work")]
    public double WorkLighting { get; set; }

    /// <summary>Other lighting in W/m² ("Anden belysning") (<c>other</c>).</summary>
    [Be18Field("other")]
    public double OtherLighting { get; set; }

    /// <summary>Standby power in W/m² ("Standby") (<c>stand_by</c>).</summary>
    [Be18Field("stand_by")]
    public double Standby { get; set; }

    /// <summary>Night lighting in W/m² ("Natbelysning") (<c>night</c>).</summary>
    [Be18Field("night")]
    public double NightLighting { get; set; }

    /// <summary>Floor area of the zone in m² (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }

    /// <summary>Lighting control ("Styring") (<c>has_reg</c>).</summary>
    [Be18Field("has_reg")]
    public LightingControl Control { get; set; }
}
