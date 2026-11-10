namespace Be18_parser.Models;

/// <summary>
/// An enumeration value from a Be18 file, such as <c>.SUP_DISTRICT.</c>.
/// </summary>
/// <remarks>
/// Enumerations are modelled as structs wrapping the raw value rather than as C# enums, because
/// only the values seen in example files are known. Any other value read from a file is kept as
/// is and written back unchanged.
/// </remarks>
public interface IBe18Enum
{
    /// <summary>The value as written in the file, without the surrounding dots, e.g. <c>SUP_DISTRICT</c>.</summary>
    string Value { get; }
}
