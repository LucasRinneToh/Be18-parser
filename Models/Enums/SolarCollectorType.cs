namespace Be18_parser.Models;

/// <summary>What a solar heating system supplies (<c>has_type</c> on <see cref="SolarCollector"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct SolarCollectorType(string Value) : IBe18Enum
{
    /// <summary>Domestic hot water only (<c>.B_TYPE.</c>, "Brugsvand").</summary>
    public static SolarCollectorType DomesticHotWater { get; } = new("B_TYPE");

    /// <summary>Room heating only (<c>.R_TYPE.</c>, "Rumopvarmning").</summary>
    public static SolarCollectorType RoomHeating { get; } = new("R_TYPE");

    /// <summary>Both domestic hot water and room heating (<c>.K_TYPE.</c>, "Kombineret").</summary>
    public static SolarCollectorType Combined { get; } = new("K_TYPE");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
