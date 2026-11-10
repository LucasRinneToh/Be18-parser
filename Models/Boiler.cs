namespace Be18_parser.Models;

/// <summary>A boiler (<c>BOILER</c>).</summary>
[Be18Entity("BOILER")]
public sealed class Boiler : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Nominal power in kW ("Nominel effekt") (<c>nom_eff</c>).</summary>
    [Be18Field("nom_eff")]
    public double NominalPower { get; set; }

    /// <summary>Fraction of the nominal power used for domestic hot water ("Andel af nom. eff. til VBV produktion") (<c>part_dhw</c>).</summary>
    [Be18Field("part_dhw")]
    public double DhwPowerFraction { get; set; }

    /// <summary>Load at full load, normally 1 ("Belastning, fuldlast") (<c>full_load</c>).</summary>
    [Be18Field("full_load")]
    public double FullLoad { get; set; }

    /// <summary>Efficiency at full load ("Virkningsgrad, fuldlast") (<c>full_ngn</c>).</summary>
    [Be18Field("full_ngn")]
    public double FullLoadEfficiency { get; set; }

    /// <summary>Boiler temperature at full load in °C ("Kedeltemp., fuldlast") (<c>full_tgn</c>).</summary>
    [Be18Field("full_tgn")]
    public double FullLoadTestTemperature { get; set; }

    /// <summary>Efficiency correction per °C at full load ("Korrektion, fuldlast") (<c>full_fcor</c>).</summary>
    [Be18Field("full_fcor")]
    public double FullLoadCorrection { get; set; }

    /// <summary>Load at part load as a fraction of full load ("Belastning, dellast") (<c>part_load</c>).</summary>
    [Be18Field("part_load")]
    public double PartLoad { get; set; }

    /// <summary>Efficiency at part load ("Virkningsgrad, dellast") (<c>part_ngn</c>).</summary>
    [Be18Field("part_ngn")]
    public double PartLoadEfficiency { get; set; }

    /// <summary>Boiler temperature at part load in °C ("Kedeltemp., dellast") (<c>part_tgn</c>).</summary>
    [Be18Field("part_tgn")]
    public double PartLoadTestTemperature { get; set; }

    /// <summary>Efficiency correction per °C at part load ("Korrektion, dellast") (<c>part_fcor</c>).</summary>
    [Be18Field("part_fcor")]
    public double PartLoadCorrection { get; set; }

    /// <summary>Standby loss factor ("Tomgangstab, tabsfaktor") (<c>stb_frac</c>).</summary>
    [Be18Field("stb_frac")]
    public double StandbyLossFactor { get; set; }

    /// <summary>Fraction of the standby loss given off to the room ("Tomgangstab, andel til rum") (<c>stb_rgn</c>).</summary>
    [Be18Field("stb_rgn")]
    public double StandbyLossToRoomFraction { get; set; }

    /// <summary>Temperature difference for the standby loss in °C ("Tomgangstab, temp. dif") (<c>stb_dtgn</c>).</summary>
    [Be18Field("stb_dtgn")]
    public double StandbyTemperatureDifference { get; set; }

    /// <summary>Minimum boiler temperature in °C ("Kedeltemp., min") (<c>min_boil_temp</c>).</summary>
    [Be18Field("min_boil_temp")]
    public double MinBoilerTemperature { get; set; }

    /// <summary>b-factor for the room the boiler is in ("b-faktor for rum") (<c>b_factor</c>).</summary>
    [Be18Field("b_factor")]
    public double BFactor { get; set; }

    /// <summary>Power of the fan in W ("Blæsereffekt") (<c>fan_eff</c>).</summary>
    [Be18Field("fan_eff")]
    public double FanPower { get; set; }

    /// <summary>Electricity for automatics in W ("El til automatik") (<c>auto_stb</c>).</summary>
    [Be18Field("auto_stb")]
    public double AutomaticsPower { get; set; }

    /// <summary>Load for the standby loss ("Tomgangstab, belastning") (<c>stb_load</c>).</summary>
    [Be18Field("stb_load")]
    public double StandbyLoad { get; set; }

    /// <summary>Number of identical boilers ("Antal") (<c>no</c>).</summary>
    [Be18Field("no")]
    public int Count { get; set; }

    /// <summary>Fuel type ("Brændsel") (<c>has_fuel</c>).</summary>
    [Be18Field("has_fuel")]
    public FuelType Fuel { get; set; }
}
