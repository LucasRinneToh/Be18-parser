namespace Be18_parser.Models;

/// <summary>Calculation condition (<c>calc_condition</c> on <see cref="Building"/>).</summary>
/// <remarks>Only the values seen in example files are listed. Other values are kept as is.</remarks>
/// <param name="Value">The value as written in the file, without the surrounding dots.</param>
public readonly record struct CalculationCondition(string Value) : IBe18Enum
{
    /// <summary><c>.STANDARD.</c></summary>
    public static CalculationCondition Standard { get; } = new("STANDARD");

    /// <summary><c>.ACTUAL.</c></summary>
    public static CalculationCondition Actual { get; } = new("ACTUAL");

    /// <inheritdoc/>
    public override string ToString() => Value;
}
