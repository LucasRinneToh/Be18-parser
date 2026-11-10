namespace Be18_parser.Models;

/// <summary>Basic heat supply of the building (<c>basic_heat_supply</c> on <see cref="Building"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct HeatSupply(string Value) : IBe18Enum
{
    /// <summary>Boiler (<c>.SUP_BOILER.</c>, "Kedel").</summary>
    public static HeatSupply Boiler { get; } = new("SUP_BOILER");

    /// <summary>District heating (<c>.SUP_DISTRICT.</c>, "Fjernvarme").</summary>
    public static HeatSupply DistrictHeating { get; } = new("SUP_DISTRICT");

    /// <summary>Block heating, a local heating plant shared by a group of buildings (<c>.SUP_BLOCK.</c>, "Blokvarme").</summary>
    public static HeatSupply BlockHeating { get; } = new("SUP_BLOCK");

    /// <summary>Electricity (<c>.SUP_EL.</c>, "El").</summary>
    public static HeatSupply Electricity { get; } = new("SUP_EL");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
