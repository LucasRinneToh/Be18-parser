using Be18_parser.Models;

namespace Be18_parser.Examples;

/// <summary>Loads a Be18 file, changes some data and saves it as a new file.</summary>
public static class WriteExample
{
    public static void Run(string inputPath, string outputPath)
    {
        var document = Be18Document.Load(inputPath);
        var building = document.Building
            ?? throw new InvalidOperationException("The file has no building.");

        // Change values directly on the models.
        building.Name += " (changed)";
        building.HeatingSetpoint = 21;

        // Improve the insulation of all opaque constructions by 10 %.
        foreach (var construction in building.OpaqueConstructions)
            construction.U = Math.Round(construction.U * 0.9, 3);

        // Add a new window. It can share a shading entity with existing windows. A new entity gets
        // the next free rid when it is created, above every rid in the loaded file, and only needs
        // to be referenced from the building to be saved.
        var shading = building.TransparentConstructions.FirstOrDefault()?.Shading;
        building.TransparentConstructions.Add(new TransparentConstruction
        {
            Name = "New window towards south",
            Count = 1,
            Area = 1.8,
            U = 0.8,
            Orientation = "s",
            Slope = 90,
            GlazingFraction = 0.75,
            GValue = 0.5,
            ShadingFactor = 1,
            BFactor = 1,
            Shading = shading,
        });

        // Save as .bexml or .xml; the content is the same. Unchanged parts are written exactly as
        // they were read.
        document.Save(outputPath);
        Console.WriteLine($"Saved {outputPath}");

        // Load the new file again to show that the changes were saved.
        var saved = Be18Document.Load(outputPath).Building!;
        Console.WriteLine($"Building:  {saved.Name}");
        Console.WriteLine($"Heating setpoint: {saved.HeatingSetpoint} °C");
        Console.WriteLine($"Windows and doors: {saved.TransparentConstructions.Count}, the new one has rid #{saved.TransparentConstructions[^1].Rid}");
    }
}
