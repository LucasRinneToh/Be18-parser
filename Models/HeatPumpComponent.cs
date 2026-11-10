namespace Be18_parser.Models;

/// <summary>Room heating or domestic hot water part of a <see cref="HeatPump"/> (<c>VP_COMP</c>).</summary>
[Be18Entity("VP_COMP")]
public sealed class HeatPumpComponent : Be18Entity
{
    /// <summary>Nominal power in kW ("Nominel effekt") (<c>nom_eff</c>).</summary>
    [Be18Field("nom_eff")]
    public double NominalPower { get; set; }

    /// <summary>Nominal COP including pumps etc. ("Nominel COP, incl. pumper, mm.") (<c>nom_cop</c>).</summary>
    [Be18Field("nom_cop")]
    public double NominalCop { get; set; }

    /// <summary>Relative COP at 50% load ("Rel. COP ved 50% last") (<c>rel_cop50</c>).</summary>
    [Be18Field("rel_cop50")]
    public double RelativeCopAt50PercentLoad { get; set; }

    /// <summary>Test temperature on the cold side in °C ("Testtemperaturer, kold side") (<c>temp_cold</c>).</summary>
    [Be18Field("temp_cold")]
    public double TestTemperatureColdSide { get; set; }

    /// <summary>Test temperature on the warm side in °C ("Testtemperaturer, varm side") (<c>temp_warm</c>).</summary>
    [Be18Field("temp_warm")]
    public double TestTemperatureWarmSide { get; set; }

    /// <summary>Power of special auxiliary equipment in W ("Særligt hjælpeudstyr") (<c>equip</c>).</summary>
    [Be18Field("equip")]
    public double AuxiliaryEquipmentPower { get; set; }

    /// <summary>Standby power of the automatics etc. in W ("Automatik, standby, mm.") (<c>auto_stb</c>).</summary>
    [Be18Field("auto_stb")]
    public double AutomaticsStandbyPower { get; set; }

    /// <summary>Temperature efficiency of heat recovery before the heat pump, for heat pumps connected to ventilation ("Temp. virk. grad for VGV før VP") (<c>ny_vgv</c>).</summary>
    [Be18Field("ny_vgv")]
    public double HeatRecoveryEfficiencyBeforeHeatPump { get; set; }

    /// <summary>Design supply air temperature in °C, for heat pumps connected to ventilation ("Dim. indblæsningstemperatur") (<c>dim_ti</c>).</summary>
    [Be18Field("dim_ti")]
    public double DesignSupplyAirTemperature { get; set; }

    /// <summary>Air flow in m³/s, for heat pumps connected to ventilation ("Luftstrømsbehov") (<c>air_flow</c>).</summary>
    [Be18Field("air_flow")]
    public double AirFlow { get; set; }

    /// <summary>Temperature difference of the exchanger on the cold side in °C, for another heat source ("Temp. dif. veksler på kold side") (<c>tdif_cold</c>).</summary>
    [Be18Field("tdif_cold")]
    public double ColdSideExchangerTemperatureDifference { get; set; }

    /// <summary>Heat source on the cold side ("Kold side") (<c>has_type_cold</c>).</summary>
    [Be18Field("has_type_cold")]
    public HeatPumpColdSide ColdSide { get; set; }

    /// <summary>Heat delivery on the warm side ("Varm side") (<c>has_type_warm</c>).</summary>
    [Be18Field("has_type_warm")]
    public HeatPumpWarmSide WarmSide { get; set; }
}
