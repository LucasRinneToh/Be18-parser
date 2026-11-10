namespace Be18_parser.Models;

/// <summary>The heat distribution system (<c>HEAT_DISTRIBUTION</c>).</summary>
[Be18Entity("HEAT_DISTRIBUTION")]
public sealed class HeatDistribution : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Pipe system type ("Anlægstype"); 2 is a two-pipe system ("2-streng") (<c>tube_type</c>).</summary>
    [Be18Field("tube_type")]
    public int PipeSystemType { get; set; }

    /// <summary>Design flow temperature ("Fremløbstemperatur") (<c>inlet_temp</c>).</summary>
    [Be18Field("inlet_temp")]
    public DesignTemperature? InletTemperature { get; set; }

    /// <summary>Design return temperature ("Returløbstemperatur") (<c>return_temp</c>).</summary>
    [Be18Field("return_temp")]
    public DesignTemperature? ReturnTemperature { get; set; }

    /// <summary>Pump in constant operation all year ("Konstant drift året rundt") (<c>pump_const_year</c>).</summary>
    [Be18Field("pump_const_year")]
    public DistributionPump? ConstantYearRoundPump { get; set; }

    /// <summary>Pump in constant operation in the heating season ("Konstant drift i opvarmningssæson") (<c>pump_const_hs</c>).</summary>
    [Be18Field("pump_const_hs")]
    public DistributionPump? ConstantHeatingSeasonPump { get; set; }

    /// <summary>Time-controlled pump in the heating season ("Tidsstyret drift i opvarmningssæson") (<c>pump_timereg_hs</c>).</summary>
    [Be18Field("pump_timereg_hs")]
    public DistributionPump? TimeControlledHeatingSeasonPump { get; set; }

    /// <summary>Combi pump, constant in the heating season ("Kombi-pumpe") (<c>pump_combi</c>).</summary>
    [Be18Field("pump_combi")]
    public DistributionPump? CombiPump { get; set; }

    /// <summary>Distribution pipes ("Varmerør") (<c>has_tube</c>).</summary>
    [Be18Field("has_tube")]
    public List<HeatDistributionPipe> Pipes { get; set; } = [];
}
