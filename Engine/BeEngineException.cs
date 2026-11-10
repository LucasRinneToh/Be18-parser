namespace Be18_parser.Engine;

/// <summary>Thrown when the Be calculation engine cannot be found, loaded or used.</summary>
public sealed class BeEngineException : Exception
{
    /// <summary>Creates the exception with a message.</summary>
    /// <param name="message">Description of the problem.</param>
    public BeEngineException(string message) : base(message) { }

    /// <summary>Creates the exception with a message and the exception that caused it.</summary>
    /// <param name="message">Description of the problem.</param>
    /// <param name="innerException">The exception that caused this one.</param>
    public BeEngineException(string message, Exception innerException) : base(message, innerException) { }
}
