namespace Be18_parser.Engine;

/// <summary>The result of a calculation by <see cref="BeEngine"/>.</summary>
/// <param name="Success">Whether the engine reported success.</param>
/// <param name="Xml">The result XML returned by the engine. Can be empty on failure.</param>
/// <param name="Error">A description of the failure, when known.</param>
public sealed record EngineResult(bool Success, string Xml, string? Error = null);
