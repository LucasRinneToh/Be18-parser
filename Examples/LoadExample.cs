using Be18_parser.Models;

namespace Be18_parser.Examples;

/// <summary>Loads a Be18 file and reads data from the models.</summary>
public static class LoadExample
{
    public static void Run(string path)
    {
        // .bexml and .xml files have the same content, so both load the same way.
        var document = Be18Document.Load(path);

        // The header holds the version of the program that wrote the file.
        Console.WriteLine($"Written by: {document.Header.Name}");

        // Everything else is reached through the building.
        var building = document.Building
            ?? throw new InvalidOperationException("The file has no building.");

        Console.WriteLine($"Building:   {building.Name}");
        Console.WriteLine($"Type:       {building.BuildingType}");
        Console.WriteLine($"Heated floor area: {building.HeatedFloorArea} m²");
        Console.WriteLine($"Heat supply: {building.BasicHeatSupply}");

        // Enumerations can be compared with the known values.
        if (building.BasicHeatSupply == HeatSupply.DistrictHeating)
            Console.WriteLine($"District heating exchanger: {building.HeatingSystems?.DistrictHeatingExchanger?.NominalPower} kW");

        // References are ordinary object references, so lists can be queried with LINQ.
        Console.WriteLine();
        Console.WriteLine("Windows and doors:");
        foreach (var window in building.TransparentConstructions)
        {
            Console.WriteLine(
                $"  {window.Name,-40} {window.Count,3} x {window.Area,6} m²  U = {window.U}  " +
                $"orientation {window.Orientation}, shading: {window.Shading?.Name ?? "none"}");
        }

        var windowArea = building.TransparentConstructions.Sum(w => w.Count * w.Area);
        Console.WriteLine($"Total window and door area: {windowArea:0.##} m²");

        Console.WriteLine();
        Console.WriteLine("Opaque constructions:");
        foreach (var construction in building.OpaqueConstructions)
            Console.WriteLine($"  {construction.Name,-40} {construction.Area,8} m²  U = {construction.U}");

        Console.WriteLine();
        Console.WriteLine("Ventilation zones:");
        foreach (var zone in building.VentilationZones)
            Console.WriteLine($"  {zone.Name,-40} {zone.Area,8} m²  qm = {zone.MechanicalVentilation} l/s m²");
    }
}
