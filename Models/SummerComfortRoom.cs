namespace Be18_parser.Models;

/// <summary>The room used for the summer comfort (overheating) check of a dwelling (<c>HEATED_ROOM</c>). Ventilation rates are in l/s per m².</summary>
[Be18Entity("HEATED_ROOM")]
public sealed class SummerComfortRoom : Be18Entity
{
    /// <summary>Floor area in m² ("Gulvareal") (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>Ventilation in summer, daytime ("Sommer, dag") (<c>vent_day</c>).</summary>
    [Be18Field("vent_day")]
    public double VentilationSummerDay { get; set; }

    /// <summary><c>vent_evening</c> (meaning not yet confirmed).</summary>
    [Be18Field("vent_evening")]
    public double VentEvening { get; set; }

    /// <summary>Ventilation in summer, at night ("nat") (<c>vent_night</c>).</summary>
    [Be18Field("vent_night")]
    public double VentilationSummerNight { get; set; }

    /// <summary>Ventilation in winter ("Vinter") (<c>vent_basic</c>).</summary>
    [Be18Field("vent_basic")]
    public double VentilationWinter { get; set; }
}
