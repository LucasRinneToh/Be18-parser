namespace Be18_parser.Models;

/// <summary>A district heating exchanger (<c>D_H_EXCH</c>).</summary>
[Be18Entity("D_H_EXCH")]
public sealed class DistrictHeatingExchanger : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Nominal power in kW ("Nominel effekt") (<c>nom_eff</c>).</summary>
    [Be18Field("nom_eff")]
    public double NominalPower { get; set; }

    /// <summary>Heat loss from the exchanger in W/K ("Varmetab fra veksler") (<c>heat_loss</c>).</summary>
    [Be18Field("heat_loss")]
    public double HeatLoss { get; set; }

    /// <summary>Whether domestic hot water is heated through the exchanger ("VBV opvarmning gennem veksler") (<c>dhw</c>).</summary>
    [Be18Field("dhw")]
    public bool HeatsDomesticHotWater { get; set; }

    /// <summary>Minimum exchanger temperature in °C ("Min. vekslertemperatur") (<c>min_temp</c>).</summary>
    [Be18Field("min_temp")]
    public double MinTemperature { get; set; }

    /// <summary>b-factor for heat loss from the exchanger ("b-faktor") (<c>b_factor</c>).</summary>
    [Be18Field("b_factor")]
    public double BFactor { get; set; }

    /// <summary>Standby power of the automatics in W ("Automatik, standby") (<c>auto_stb</c>).</summary>
    [Be18Field("auto_stb")]
    public double AutomaticsStandbyPower { get; set; }
}
