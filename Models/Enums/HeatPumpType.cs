namespace Be18_parser.Models;

/// <summary>What a heat pump supplies (<c>has_type</c> on <see cref="HeatPump"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct HeatPumpType(string Value) : IBe18Enum
{
    /// <summary>Domestic hot water only (<c>.VP_DHW.</c>, "Brugsvand").</summary>
    public static HeatPumpType DomesticHotWater { get; } = new("VP_DHW");

    /// <summary>Room heating only (<c>.VP_ROOM.</c>, "Rumopvarmning").</summary>
    public static HeatPumpType RoomHeating { get; } = new("VP_ROOM");

    /// <summary>Combined domestic hot water and room heating, on-off (<c>.VP_COMB.</c>, "Kombineret, on-off").</summary>
    public static HeatPumpType CombinedOnOff { get; } = new("VP_COMB");

    /// <summary>Duo (<c>.VP_DUO.</c>, "Duo").</summary>
    public static HeatPumpType Duo { get; } = new("VP_DUO");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
