using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security;
using Microsoft.Win32;

namespace Be18_parser.Engine;

/// <summary>
/// Finds installed Be calculation engine DLLs, by looking in the current folder and in the
/// install folders the Be installers record in the registry.
/// </summary>
[SupportedOSPlatform("windows")]
public static class BeEngineLocator
{
    /// <summary>
    /// File names of the engine DLLs, in order of preference. <c>Be15Eng.dll</c> and
    /// <c>Be10Eng.dll</c> are the names used by Be15 and Be10. <c>Be18Eng.dll</c> is assumed from
    /// that pattern and has not been confirmed against a Be18 installation.
    /// </summary>
    public static IReadOnlyList<string> EngineFileNames { get; } = ["Be18Eng.dll", "Be15Eng.dll", "Be10Eng.dll"];

    /// <summary>Registry key (under HKEY_LOCAL_MACHINE) whose subkeys hold the install folders.</summary>
    public const string RegistryKeyPath = @"SOFTWARE\WOW6432Node\SBi";

    /// <summary>Name of the registry value that holds an install folder.</summary>
    public const string InstallDirValueName = "InstallDir";

    /// <summary>
    /// Finds an engine DLL: first in the current folder, then the newest one found through the
    /// registry.
    /// </summary>
    /// <returns>Full path to the DLL, or <see langword="null"/> if none was found.</returns>
    public static string? FindEngine()
        => FindInDirectory(Environment.CurrentDirectory) ?? FindAllFromRegistry().FirstOrDefault();

    /// <summary>Looks for an engine DLL in a folder.</summary>
    /// <param name="directory">The folder to look in.</param>
    /// <returns>Full path to the first DLL in <see cref="EngineFileNames"/> that exists, or <see langword="null"/>.</returns>
    public static string? FindInDirectory(string directory)
    {
        if (!Directory.Exists(directory))
            return null;

        return EngineFileNames
            .Select(name => Path.GetFullPath(Path.Combine(directory, name)))
            .FirstOrDefault(File.Exists);
    }

    /// <summary>Finds all engine DLLs in the install folders recorded in the registry.</summary>
    /// <returns>Full paths to the DLLs, newest file version first.</returns>
    public static IReadOnlyList<string> FindAllFromRegistry()
    {
        using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
        using var key = baseKey.OpenSubKey(RegistryKeyPath);
        if (key == null)
            return [];

        return FindInstallDirs(key)
            .Select(FindInDirectory)
            .OfType<string>()
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(GetFileVersion)
            .ThenByDescending(path => path, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static IEnumerable<string> FindInstallDirs(RegistryKey key)
    {
        if (key.GetValue(InstallDirValueName) is string dir)
            yield return dir;

        foreach (var name in key.GetSubKeyNames())
        {
            RegistryKey? subKey;
            try
            {
                subKey = key.OpenSubKey(name);
            }
            catch (SecurityException)
            {
                continue;
            }

            if (subKey == null)
                continue;

            using (subKey)
            {
                foreach (var subDir in FindInstallDirs(subKey))
                    yield return subDir;
            }
        }
    }

    private static Version GetFileVersion(string path)
    {
        var info = FileVersionInfo.GetVersionInfo(path);
        return new Version(info.FileMajorPart, info.FileMinorPart, info.FileBuildPart, info.FilePrivatePart);
    }
}
