namespace Be18_parser.Models;

/// <summary>A field stored exactly as it appears in the file, without interpretation.</summary>
/// <param name="Name">Element name of the field, e.g. <c>th_heat</c>.</param>
/// <param name="Value">Text content of the field, e.g. <c>20</c>, <c>.T.</c> or <c>#12 #13</c>.</param>
public sealed record Be18RawField(string Name, string Value);
