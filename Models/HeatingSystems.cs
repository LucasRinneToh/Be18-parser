namespace Be18_parser.Models;

/// <summary>The heat producing systems of the building (<c>HEATING_SYSTEMS</c>).</summary>
[Be18Entity("HEATING_SYSTEMS")]
public sealed class HeatingSystems : Be18Entity
{
    /// <summary>Solar heating system (<c>has_solar_collector</c>).</summary>
    [Be18Field("has_solar_collector")]
    public SolarCollector? SolarCollector { get; set; }

    /// <summary>Solar cell systems. One example file has a <see langword="null"/> item (<c>$ #35</c>) before the actual system (<c>has_pv_cell</c>).</summary>
    [Be18Field("has_pv_cell")]
    public List<PvSystem?> PvSystems { get; set; } = [];

    /// <summary>Boiler (<c>has_boiler</c>).</summary>
    [Be18Field("has_boiler")]
    public Boiler? Boiler { get; set; }

    /// <summary>District heating exchanger (<c>has_d_h_exch</c>).</summary>
    [Be18Field("has_d_h_exch")]
    public DistrictHeatingExchanger? DistrictHeatingExchanger { get; set; }

    /// <summary>Supplementary direct room heating (<c>has_sup_room_heat</c>).</summary>
    [Be18Field("has_sup_room_heat")]
    public SupplementaryRoomHeating? SupplementaryRoomHeating { get; set; }

    /// <summary>Heat pumps. One example file has a <see langword="null"/> item (<c>$ #38</c>) before the actual heat pump (<c>has_heat_pump</c>).</summary>
    [Be18Field("has_heat_pump")]
    public List<HeatPump?> HeatPumps { get; set; } = [];

    /// <summary>Wind turbines; no example file has any yet (<c>has_wind_mill</c>).</summary>
    [Be18Field("has_wind_mill")]
    public List<Be18Entity> WindTurbines { get; set; } = [];
}
