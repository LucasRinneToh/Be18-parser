namespace Be18_parser.Models;

/// <summary>The domestic hot water system (<c>DHW_SYSTEM</c>).</summary>
[Be18Entity("DHW_SYSTEM")]
public sealed class DomesticHotWater : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Hot water consumption in litres per m² per year (<c>consumption</c>).</summary>
    [Be18Field("consumption")]
    public double Consumption { get; set; }

    /// <summary>Hot water temperature in °C (<c>temp</c>).</summary>
    [Be18Field("temp")]
    public double Temperature { get; set; }

    /// <summary>Hot water tank (<c>has_dhw_tank</c>).</summary>
    [Be18Field("has_dhw_tank")]
    public HotWaterTank? Tank { get; set; }

    /// <summary>No example file sets it yet (<c>old_hot_water</c>).</summary>
    [Be18Field("old_hot_water")]
    public Be18Entity? OldHotWater { get; set; }
}
