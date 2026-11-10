namespace Be18_parser.Models;

/// <summary>Fuel used by a boiler (<c>has_fuel</c> on <see cref="Boiler"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct FuelType(string Value) : IBe18Enum
{
    /// <summary>Oil (<c>.FU_OIL.</c>, "Olie").</summary>
    public static FuelType Oil { get; } = new("FU_OIL");

    /// <summary>Gas (<c>.FU_GAS.</c>, "Gas").</summary>
    public static FuelType Gas { get; } = new("FU_GAS");

    /// <summary>Biofuel, such as wood pellets or wood chips (<c>.FU_BIO.</c>, "Biobrændsel").</summary>
    public static FuelType Biofuel { get; } = new("FU_BIO");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
