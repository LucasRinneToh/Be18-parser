namespace Be18_parser.Models;

/// <summary>A ventilation zone (<c>VENTILATION</c>). Rates are in l/s per m² floor area; the Be symbols are given in quotes.</summary>
[Be18Entity("VENTILATION")]
public sealed class VentilationZone : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Floor area of the zone in m² (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>Mechanical ventilation in winter ("qm") (<c>qvm</c>).</summary>
    [Be18Field("qvm")]
    public double MechanicalVentilation { get; set; }

    /// <summary>Heat recovery efficiency ("nvgv") (<c>nvgv</c>).</summary>
    [Be18Field("nvgv")]
    public double HeatRecoveryEfficiency { get; set; }

    /// <summary>Inlet air temperature in °C ("Ti") (<c>tin</c>).</summary>
    [Be18Field("tin")]
    public double InletTemperature { get; set; }

    /// <summary>Whether the inlet air is heated by an electric heating coil ("El-VF") (<c>el_vf</c>).</summary>
    [Be18Field("el_vf")]
    public bool HasElectricHeatingCoil { get; set; }

    /// <summary>Natural ventilation in winter ("qn") (<c>qid</c>).</summary>
    [Be18Field("qid")]
    public double NaturalVentilation { get; set; }

    /// <summary>"qi,n"; meaning not yet confirmed (<c>qis</c>).</summary>
    [Be18Field("qis")]
    public double QiN { get; set; }

    /// <summary>Specific electricity use for air transport in kJ/m³ ("SEL") (<c>sel</c>).</summary>
    [Be18Field("sel")]
    public double Sel { get; set; }

    /// <summary>Mechanical ventilation in summer ("qm,s") (<c>qvm_day</c>).</summary>
    [Be18Field("qvm_day")]
    public double MechanicalVentilationSummer { get; set; }

    /// <summary>Natural ventilation in summer ("qn,s") (<c>qid_day</c>).</summary>
    [Be18Field("qid_day")]
    public double NaturalVentilationSummer { get; set; }

    /// <summary>Mechanical ventilation at night ("qm,n") (<c>qvm_night</c>).</summary>
    [Be18Field("qvm_night")]
    public double MechanicalVentilationNight { get; set; }

    /// <summary>Natural ventilation at night ("qn,n") (<c>qis_night</c>).</summary>
    [Be18Field("qis_night")]
    public double NaturalVentilationNight { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }

    /// <summary>Usage factor ("Fo") (<c>fo</c>).</summary>
    [Be18Field("fo")]
    public double UsageFactor { get; set; }
}
