namespace Be18_parser.Serialization;

/// <summary>Thrown when a file is not a valid Be18 file.</summary>
public sealed class Be18FormatException : Exception
{
    /// <summary>Creates the exception with a message.</summary>
    /// <param name="message">Description of the problem, including where in the file it is.</param>
    public Be18FormatException(string message) : base(message) { }

    /// <summary>Creates the exception with a message and the exception that caused it.</summary>
    /// <param name="message">Description of the problem, including where in the file it is.</param>
    /// <param name="innerException">The exception that caused this one.</param>
    public Be18FormatException(string message, Exception innerException) : base(message, innerException) { }
}
