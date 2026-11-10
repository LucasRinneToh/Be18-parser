namespace Be18_parser.Models;

/// <summary>Daylight control of the lighting in a zone (<c>has_reg</c> on <see cref="LightingZone"/>).</summary>
/// <remarks>
/// Be also offers manual control ("Manuelt"), but its value has not been seen in a file yet. Any
/// other value read from a file is kept as is.
/// </remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct LightingControl(string Value) : IBe18Enum
{
    /// <summary>No daylight control (<c>.U.</c>, "Uden dagslysstyring").</summary>
    public static LightingControl None { get; } = new("U");

    /// <summary>Automatic control (<c>.A.</c>, "Automatisk").</summary>
    public static LightingControl Automatic { get; } = new("A");

    /// <summary>Continuous control by daylight (<c>.K.</c>, "Kontinuert styring efter dagslys").</summary>
    public static LightingControl Continuous { get; } = new("K");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
