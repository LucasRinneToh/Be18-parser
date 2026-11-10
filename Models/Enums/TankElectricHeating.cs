namespace Be18_parser.Models;

/// <summary>When the hot water in a tank is heated electrically (<c>has_el_heat</c> on <see cref="HotWaterTank"/>).</summary>
/// <remarks>
/// Be also offers "Altid" (always) and "Sommer" (in summer), but their values have not been seen in
/// a file yet. Any other value read from a file is kept as is.
/// </remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct TankElectricHeating(string Value) : IBe18Enum
{
    /// <summary>No electric heating (<c>.DHW_N.</c>, "Nej").</summary>
    public static TankElectricHeating None { get; } = new("DHW_N");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
