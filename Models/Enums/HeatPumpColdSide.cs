namespace Be18_parser.Models;

/// <summary>Heat source on the cold side of a heat pump component (<c>has_type_cold</c> on <see cref="HeatPumpComponent"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct HeatPumpColdSide(string Value) : IBe18Enum
{
    /// <summary>Ground coil (<c>.VP_J.</c>, "Jordslange").</summary>
    public static HeatPumpColdSide GroundCoil { get; } = new("VP_J");

    /// <summary>Exhaust air (<c>.VP_A.</c>, "Aftræk").</summary>
    public static HeatPumpColdSide ExhaustAir { get; } = new("VP_A");

    /// <summary>Outdoor air (<c>.VP_U.</c>, "Udeluft").</summary>
    public static HeatPumpColdSide OutdoorAir { get; } = new("VP_U");

    /// <summary>Other heat source (<c>.VP_X.</c>, "Anden kilde").</summary>
    public static HeatPumpColdSide OtherSource { get; } = new("VP_X");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
