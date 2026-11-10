namespace Be18_parser.Models;

/// <summary>A heat pump (<c>HEAT_PUMP</c>).</summary>
[Be18Entity("HEAT_PUMP")]
public sealed class HeatPump : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Fraction of the floor area the heat pump serves ("Andel af etageareal") (<c>a_frac</c>).</summary>
    [Be18Field("a_frac")]
    public double FloorAreaFraction { get; set; }

    /// <summary>Monthly source temperatures for another heat source ("Kildetemperaturer"). Empty in all example files, so the format is not known yet (<c>t_source</c>).</summary>
    [Be18Field("t_source")]
    public string SourceTemperatures { get; set; } = "";

    /// <summary>Room heating part of the heat pump ("Rumopvarmning") (<c>room_heating</c>).</summary>
    [Be18Field("room_heating")]
    public HeatPumpComponent? RoomHeating { get; set; }

    /// <summary>Domestic hot water part of the heat pump ("Varmt brugsvand") (<c>dhw</c>).</summary>
    [Be18Field("dhw")]
    public HeatPumpComponent? HotWater { get; set; }

    /// <summary>What the heat pump supplies ("Type") (<c>has_type</c>).</summary>
    [Be18Field("has_type")]
    public HeatPumpType Type { get; set; }
}
