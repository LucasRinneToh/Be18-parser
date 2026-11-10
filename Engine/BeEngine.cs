using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace Be18_parser.Engine;

/// <summary>
/// Runs Be project files through a locally installed Be calculation engine DLL.
/// </summary>
/// <remarks>
/// <para>
/// This is a C# version of the Python <c>beeng</c> package by BUILD
/// (https://github.com/BUILD-Institut-for-Byggeri-By-og-Miljo/beeng-py). It loads the engine DLL
/// into the current process and calls its exported functions.
/// </para>
/// <para>
/// The Be10 and Be15 engines are 32-bit DLLs. A 32-bit DLL can only be loaded by a 32-bit
/// process, so the calling application must run as x86 to use them. The constructor checks this
/// and throws a <see cref="BeEngineException"/> that explains the problem.
/// </para>
/// <para>
/// All text passed to and from the engine is ISO-8859-1 (Latin-1). Calls are serialized with a
/// lock, as the engine is not known to be thread safe.
/// </para>
/// </remarks>
[SupportedOSPlatform("windows")]
public sealed class BeEngine : IDisposable
{
    /// <summary>The encoding used for all text passed to and from the engine.</summary>
    public static Encoding TextEncoding => Be18Document.TextEncoding;

    private const ushort MachineX86 = 0x014C;
    private const ushort MachineX64 = 0x8664;

    private readonly object _sync = new();
    private readonly LibraryHandle _library;
    private readonly NativeMethods.ResultFunction? _keys;
    private readonly NativeMethods.ResultFunction? _results;
    private readonly NativeMethods.SummerComfortFunction? _summerComfort;
    private readonly NativeMethods.LicenseFunction? _license;

    /// <summary>Loads the engine DLL.</summary>
    /// <param name="dllPath">
    /// Path to the engine DLL. If <see langword="null"/>, the DLL is found with
    /// <see cref="BeEngineLocator.FindEngine"/>.
    /// </param>
    /// <param name="bufferSize">Size in bytes of the buffer that receives result XML.</param>
    /// <param name="language">Language of the texts in the results.</param>
    /// <param name="checkForValidLicense">
    /// Whether to check for a valid license when loading. Only engines from version 8.17.1.17
    /// support the check; for older engines it is skipped. The engine always checks the license
    /// itself and returns no results without one, so this only helps report the problem early.
    /// </param>
    /// <exception cref="BeEngineException">The DLL could not be found or loaded, or no valid license was found.</exception>
    public BeEngine(
        string? dllPath = null,
        int bufferSize = 1_000_000,
        EngineLanguage language = EngineLanguage.Danish,
        bool checkForValidLicense = true)
    {
        if (bufferSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, "The buffer size must be positive.");

        DllPath = Path.GetFullPath(dllPath
            ?? BeEngineLocator.FindEngine()
            ?? throw new BeEngineException("The Be calculation engine could not be found. Install Be or pass the path to the engine DLL."));
        BufferSize = bufferSize;
        Language = language;

        if (!File.Exists(DllPath))
            throw new BeEngineException($"The engine DLL was not found: {DllPath}");

        CheckProcessArchitecture(DllPath);

        _library = NativeMethods.LoadLibraryEx(
            DllPath, IntPtr.Zero, NativeMethods.LoadLibrarySearchDllLoadDir | NativeMethods.LoadLibrarySearchDefaultDirs);
        if (_library.IsInvalid)
        {
            var error = new Win32Exception(Marshal.GetLastWin32Error());
            _library.Dispose();
            throw new BeEngineException($"The engine DLL could not be loaded: {DllPath}. {error.Message}", error);
        }

        _keys = GetFunction<NativeMethods.ResultFunction>("Be06Keys");
        _results = GetFunction<NativeMethods.ResultFunction>("Be06Res");
        _summerComfort = GetFunction<NativeMethods.SummerComfortFunction>("Be06Temp");
        _license = GetFunction<NativeMethods.LicenseFunction>("IsLicenseValid");

        if (checkForValidLicense && IsLicenseValid() == false)
        {
            Dispose();
            throw new BeEngineException("No valid license for the Be calculation engine was found.");
        }
    }

    /// <summary>Full path to the loaded engine DLL.</summary>
    public string DllPath { get; }

    /// <summary>Size in bytes of the buffer that receives result XML.</summary>
    public int BufferSize { get; }

    /// <summary>Language of the texts in the results.</summary>
    public EngineLanguage Language { get; }

    /// <summary>
    /// Reads a project file into a string that can be passed to <see cref="GetKeyXml"/> and
    /// <see cref="GetResultXml"/>. The bytes are decoded as ISO-8859-1, so they reach the engine
    /// exactly as they are in the file.
    /// </summary>
    /// <param name="path">Path to a <c>.bexml</c> or <c>.xml</c> project file.</param>
    /// <returns>The file content.</returns>
    public static string LoadModel(string path) => TextEncoding.GetString(File.ReadAllBytes(path));

    /// <summary>Calculates the key results of a project (engine function <c>Be06Keys</c>).</summary>
    /// <param name="modelXml">The project XML, e.g. from <see cref="LoadModel"/>.</param>
    /// <returns>The key results XML.</returns>
    /// <exception cref="BeEngineException">The result did not fit in <see cref="BufferSize"/>.</exception>
    public EngineResult GetKeyXml(string modelXml) => CallResultFunction(_keys, "Be06Keys", modelXml);

    /// <summary>Calculates the full results of a project (engine function <c>Be06Res</c>).</summary>
    /// <param name="modelXml">The project XML, e.g. from <see cref="LoadModel"/>.</param>
    /// <returns>The full results XML.</returns>
    /// <exception cref="BeEngineException">The result did not fit in <see cref="BufferSize"/>.</exception>
    public EngineResult GetResultXml(string modelXml) => CallResultFunction(_results, "Be06Res", modelXml);

    /// <summary>
    /// Calculates the summer comfort temperature for every hour of the year (engine function
    /// <c>Be06Temp</c>). Only works for residential buildings.
    /// </summary>
    /// <remarks>
    /// The engine writes the result to <c>&lt;project name&gt;_tmp.xml</c> next to the project
    /// file. That file is read and then deleted, also if it existed before the call.
    /// </remarks>
    /// <param name="projectPath">Path to the project file.</param>
    /// <returns>The summer comfort XML.</returns>
    public EngineResult GetSummerComfort(string projectPath)
    {
        var function = RequireFunction(_summerComfort, "Be06Temp");
        var fullPath = Path.GetFullPath(projectPath);
        var resultPath = Path.Combine(
            Path.GetDirectoryName(fullPath)!, Path.GetFileNameWithoutExtension(fullPath) + "_tmp.xml");

        int code;
        lock (_sync)
            code = function(ToCString(fullPath));

        try
        {
            if (code != 0)
            {
                var error = code switch
                {
                    2 => "The building is non-residential.",
                    3 => "There was a problem with the climate data.",
                    _ => $"Unknown error. Return code: {code}.",
                };
                return new EngineResult(false, "", error);
            }

            if (!File.Exists(resultPath))
                return new EngineResult(false, "", $"The engine reported success, but the result file was not found: {resultPath}");

            return new EngineResult(true, TextEncoding.GetString(File.ReadAllBytes(resultPath)));
        }
        finally
        {
            if (File.Exists(resultPath))
                File.Delete(resultPath);
        }
    }

    /// <summary>
    /// Checks whether a valid license for the engine is installed (engine function
    /// <c>IsLicenseValid</c>).
    /// </summary>
    /// <returns>
    /// Whether a valid license was found, or <see langword="null"/> if the engine is older than
    /// version 8.17.1.17 and cannot check.
    /// </returns>
    public bool? IsLicenseValid()
    {
        ObjectDisposedException.ThrowIf(_library.IsClosed, this);
        if (_license == null)
            return null;

        lock (_sync)
            return _license() == 0;
    }

    /// <summary>Unloads the engine DLL.</summary>
    public void Dispose() => _library.Dispose();

    private EngineResult CallResultFunction(NativeMethods.ResultFunction? function, string name, string modelXml)
    {
        var call = RequireFunction(function, name);
        var model = ToCString(modelXml);
        var buffer = new byte[BufferSize];
        var size = buffer.Length;

        int code;
        lock (_sync)
            code = call(model, buffer, ref size, (int)Language);

        var length = Array.IndexOf(buffer, (byte)0);
        if (length < 0)
            throw new BeEngineException($"The result of {name} did not fit in the buffer of {BufferSize} bytes. Use a larger buffer size.");

        return new EngineResult(code != 0, TextEncoding.GetString(buffer, 0, length).Trim('\n'));
    }

    private T RequireFunction<T>(T? function, string name) where T : Delegate
    {
        ObjectDisposedException.ThrowIf(_library.IsClosed, this);
        return function ?? throw new NotSupportedException($"The engine DLL does not export {name}: {DllPath}");
    }

    private T? GetFunction<T>(string name) where T : Delegate
    {
        var address = NativeMethods.GetProcAddress(_library, name);
        return address == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer<T>(address);
    }

    private static byte[] ToCString(string text)
    {
        var bytes = new byte[TextEncoding.GetByteCount(text) + 1];
        TextEncoding.GetBytes(text, 0, text.Length, bytes, 0);
        return bytes;
    }

    private static void CheckProcessArchitecture(string dllPath)
    {
        ushort machine;
        try
        {
            using var reader = new BinaryReader(File.OpenRead(dllPath));
            reader.BaseStream.Position = 0x3C;
            reader.BaseStream.Position = reader.ReadInt32() + 4;
            machine = reader.ReadUInt16();
        }
        catch (Exception ex) when (ex is IOException or EndOfStreamException or ArgumentOutOfRangeException)
        {
            // Not a readable PE file; let LoadLibraryEx report the problem.
            return;
        }

        if (machine == MachineX86 && Environment.Is64BitProcess)
            throw new BeEngineException(
                $"The engine DLL is 32-bit, but this process is 64-bit, so it cannot be loaded: {dllPath}. " +
                "Run the application as x86 (32-bit).");

        if (machine == MachineX64 && !Environment.Is64BitProcess)
            throw new BeEngineException(
                $"The engine DLL is 64-bit, but this process is 32-bit, so it cannot be loaded: {dllPath}. " +
                "Run the application as x64 (64-bit).");
    }
}
