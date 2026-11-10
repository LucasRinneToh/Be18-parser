namespace Be18_parser.Models;

/// <summary>A hot water tank (<c>DHW_TANK</c>).</summary>
[Be18Entity("DHW_TANK")]
public sealed class HotWaterTank : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Number of identical tanks ("Antal") (<c>no</c>).</summary>
    [Be18Field("no")]
    public int Count { get; set; }

    /// <summary>Tank volume in litres ("Beholdervolumen") (<c>vol</c>).</summary>
    [Be18Field("vol")]
    public double Volume { get; set; }

    /// <summary>Flow temperature from the central heating in °C ("Fremløb fra centralvarme") (<c>outlet_temp</c>).</summary>
    [Be18Field("outlet_temp")]
    public double CentralHeatingFlowTemperature { get; set; }

    /// <summary>Heat loss from the tank in W/K ("Varmetab beholder") (<c>heat_loss</c>).</summary>
    [Be18Field("heat_loss")]
    public double HeatLoss { get; set; }

    /// <summary>b-factor for heat loss from the tank ("Reduktionsfaktor b") (<c>b</c>).</summary>
    [Be18Field("b")]
    public double BFactor { get; set; }

    /// <summary>Whether the tank is a solar tank with the solar coil at the top ("Solvarmebeholder med solvarmespiral i top") (<c>solar_heat</c>).</summary>
    [Be18Field("solar_heat")]
    public bool IsSolarTank { get; set; }

    /// <summary>Fraction of the hot water consumption the tank covers ("Andel af varmtvandsforbrug") (<c>dhw_part</c>).</summary>
    [Be18Field("dhw_part")]
    public double HotWaterFraction { get; set; }

    /// <summary>Charging power in kW ("Ladekredspumpe, ladeeffekt") (<c>eff</c>).</summary>
    [Be18Field("eff")]
    public double ChargingPower { get; set; }

    /// <summary>Power of the charging circuit pump in W ("Ladekredspumpe, effekt") (<c>p</c>).</summary>
    [Be18Field("p")]
    public double ChargingPumpPower { get; set; }

    /// <summary>Whether the charging circuit pump is controlled ("Ladekredspumpe, styret") (<c>reg</c>).</summary>
    [Be18Field("reg")]
    public bool IsChargingPumpControlled { get; set; }

    /// <summary>Electric heating of the hot water ("El-opvarmning af VBV") (<c>has_el_heat</c>).</summary>
    [Be18Field("has_el_heat")]
    public TankElectricHeating ElectricHeating { get; set; }

    /// <summary>Connection pipes between the heat source and the tank (<c>has_dhw_pipe</c>).</summary>
    [Be18Field("has_dhw_pipe")]
    public List<HotWaterTankPipe> ConnectionPipes { get; set; } = [];

    /// <summary>Circulation pump (<c>has_dhw_pump_circ</c>).</summary>
    [Be18Field("has_dhw_pump_circ")]
    public CirculationPump? CirculationPump { get; set; }
}
