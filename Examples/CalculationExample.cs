using System.Runtime.Versioning;
using Be18_parser.Engine;

namespace Be18_parser.Examples;

/// <summary>Runs a Be18 file through a locally installed Be calculation engine.</summary>
/// <remarks>
/// The Be10 and Be15 engines are 32-bit DLLs and can only be loaded by a 32-bit process, so run
/// this example as x86: <c>dotnet run --project Examples -a x86 -- calculate &lt;file&gt;</c>.
/// </remarks>
[SupportedOSPlatform("windows")]
public static class CalculationExample
{
    public static void Run(string path, string? engineDllPath)
    {
        // The engine takes the project as XML text. A loaded or newly created document can be
        // passed with ToXml(), so changes made in code are calculated without saving a file first.
        var document = Be18Document.Load(path);
        var modelXml = document.ToXml();

        try
        {
            // Without a path, the engine is looked up in the current folder and the registry.
            using var engine = new BeEngine(engineDllPath);
            Console.WriteLine($"Using engine: {engine.DllPath}");

            var keys = engine.GetKeyXml(modelXml);
            Console.WriteLine(keys.Success ? "Key results:" : "The engine reported an error:");
            Console.WriteLine(keys.Xml);

            var results = engine.GetResultXml(modelXml);
            Console.WriteLine($"Full results: {(results.Success ? $"{results.Xml.Length} characters of XML" : "failed")}");

            // Summer comfort works on a file, and only for residential buildings.
            var comfort = engine.GetSummerComfort(path);
            Console.WriteLine($"Summer comfort: {(comfort.Success ? $"{comfort.Xml.Length} characters of XML" : comfort.Error)}");
        }
        catch (BeEngineException ex)
        {
            // For example: no engine installed, no valid license, or a 32-bit engine in a 64-bit process.
            Console.WriteLine($"Could not use the Be engine: {ex.Message}");
        }
    }
}
