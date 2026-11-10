namespace Be18_parser.Models;

/// <summary>Where a heat pump component delivers heat on its warm side (<c>has_type_warm</c> on <see cref="HeatPumpComponent"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct HeatPumpWarmSide(string Value) : IBe18Enum
{
    /// <summary>Room air (<c>.VP_R.</c>, "Rumluft").</summary>
    public static HeatPumpWarmSide RoomAir { get; } = new("VP_R");

    /// <summary>Supply air of the ventilation system (<c>.VP_I.</c>, "Indblæsning").</summary>
    public static HeatPumpWarmSide SupplyAir { get; } = new("VP_I");

    /// <summary>Heating system (<c>.VP_V.</c>, "Varmeanlæg").</summary>
    public static HeatPumpWarmSide HeatingSystem { get; } = new("VP_V");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
