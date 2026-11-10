namespace Be18_parser.Models;

/// <summary>Common fields of the building envelope entities: <see cref="OpaqueConstruction"/>, <see cref="TransparentConstruction"/> and <see cref="ThermalBridge"/>.</summary>
public abstract class BuildingElement : Be18Entity
{
    /// <summary>Area in m², or length in m for a <see cref="ThermalBridge"/> (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>U-value in W/m²K, or linear loss (Ψ) in W/mK for a <see cref="ThermalBridge"/> (<c>u</c>).</summary>
    [Be18Field("u")]
    public double U { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }

    /// <summary>Design inside temperature in °C ("Ti"). When <see langword="null"/>, the design room temperature of the building is used (<c>td_in</c>).</summary>
    [Be18Field("td_in")]
    public double? DesignInsideTemperature { get; set; }

    /// <summary>Design outside or ground temperature in °C ("Te/Tj"). When <see langword="null"/>, the design outdoor temperature of the building is used (<c>td_out</c>).</summary>
    [Be18Field("td_out")]
    public double? DesignOutsideTemperature { get; set; }

    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>b-factor (reduction factor for heat loss) (<c>has_b</c>).</summary>
    [Be18Field("has_b")]
    public double BFactor { get; set; }
}
