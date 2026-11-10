using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Be18_parser.Engine;

/// <summary>Windows API functions and engine function signatures used by <see cref="BeEngine"/>.</summary>
internal static class NativeMethods
{
    /// <summary>Search the folder of the DLL being loaded for its dependencies.</summary>
    public const uint LoadLibrarySearchDllLoadDir = 0x00000100;

    /// <summary>Search the application folder, System32 and added folders for dependencies.</summary>
    public const uint LoadLibrarySearchDefaultDirs = 0x00001000;

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW")]
    public static extern LibraryHandle LoadLibraryEx(string fileName, IntPtr file, uint flags);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true)]
    public static extern IntPtr GetProcAddress(LibraryHandle module, string procName);

    [DllImport("kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr module);

    /// <summary><c>Be06Keys</c> and <c>Be06Res</c>: model XML in, result XML written to <paramref name="buffer"/>. Non-zero means success.</summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int ResultFunction(byte[] model, [In, Out] byte[] buffer, ref int size, int english);

    /// <summary><c>Be06Temp</c>: project file path in, writes <c>&lt;name&gt;_tmp.xml</c> next to it. Zero means success.</summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int SummerComfortFunction(byte[] projectPath);

    /// <summary><c>IsLicenseValid</c>: zero means a valid license was found.</summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int LicenseFunction();
}

/// <summary>Handle to a loaded DLL, freed with <c>FreeLibrary</c>.</summary>
internal sealed class LibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public LibraryHandle() : base(ownsHandle: true) { }

    protected override bool ReleaseHandle() => NativeMethods.FreeLibrary(handle);
}
