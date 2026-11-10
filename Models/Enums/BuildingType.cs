namespace Be18_parser.Models;

/// <summary>Building type (<c>has_btype</c> on <see cref="Building"/>).</summary>
/// <remarks>Any other value read from a file is kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct BuildingType(string Value) : IBe18Enum
{
    /// <summary>Detached house (<c>.FTYPE.</c>, "Fritliggende bolig").</summary>
    public static BuildingType DetachedHouse { get; } = new("FTYPE");

    /// <summary>Attached houses, such as terraced or semi-detached houses (<c>.STYPE.</c>, "Sammenbyggede boliger").</summary>
    public static BuildingType AttachedHouses { get; } = new("STYPE");

    /// <summary>Apartment building (<c>.ETYPE.</c>, "Etagebolig").</summary>
    public static BuildingType ApartmentBuilding { get; } = new("ETYPE");

    /// <summary>Warehouse (<c>.LTYPE.</c>, "Lager").</summary>
    public static BuildingType Warehouse { get; } = new("LTYPE");

    /// <summary>Other, non-residential building (<c>.ATYPE.</c>, "Andet (ikke bolig)").</summary>
    public static BuildingType NonResidential { get; } = new("ATYPE");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
