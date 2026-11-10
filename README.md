# Be18-parser

A .NET library for reading, editing and writing project files from Be18, the Danish tool for
calculating the energy performance of buildings under the Danish Building Regulations (BR18).

Be18-parser maps the Be18 project format (`.bexml` / `.xml`) to strongly typed C# models. It is
meant as a foundation for tools that work with Be18 projects, such as Rhino/Grasshopper and
Revit/Dynamo plug-ins, desktop applications and automated workflows.

## Features

- **Read and write** Be18 project files. Unmodified files are written back byte for byte.
- **Typed object model** covering the building envelope, ventilation, internal gains, lighting,
  heat supply (boilers, district heating, heat pumps, solar heating, photovoltaics), heat
  distribution and domestic hot water.
- **Object references** between entities instead of raw record ids.
- **Lossless handling** of fields and entity types that are not modelled yet.
- **Documented properties** with units and the corresponding Danish labels from the Be18 user
  interface.
- **Calculation engine wrapper** for running projects through a locally installed Be calculation
  engine.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- For calculations only: Windows, a licensed Be installation, and a process architecture that
  matches the engine DLL (the Be10 and Be15 engines are 32-bit and require an x86 process).

## Getting started

Clone the repository and build the solution:

```bash
dotnet build Be18-parser.slnx
```

Then reference `Be18-parser.csproj` from your project.

### Reading a project

```csharp
using Be18_parser;

var document = Be18Document.Load("house.bexml");
var building = document.Building!;

Console.WriteLine($"{building.Name}: {building.HeatedFloorArea} m²");
foreach (var window in building.TransparentConstructions)
    Console.WriteLine($"{window.Name}: {window.Count} × {window.Area} m², U = {window.U} W/m²K");
```

### Editing a project

```csharp
building.HeatingSetpoint = 21;
foreach (var construction in building.OpaqueConstructions)
    construction.U *= 0.9;

document.Save("house-revised.bexml");
```

### Creating a project

```csharp
using Be18_parser.Models;

Be18Entity.ResetRidCounter();
var document = new Be18Document();

var building = new Building
{
    Name = "Detached house",
    BuildingType = BuildingType.DetachedHouse,
    BasicHeatSupply = HeatSupply.DistrictHeating,
    CalculationCondition = CalculationCondition.Standard,
    HeatedFloorArea = 150,
};
building.TransparentConstructions.Add(new TransparentConstruction
{
    Name = "Windows facing south", Count = 4, Area = 2.0, U = 0.8, Orientation = "s", Slope = 90,
});

document.Building = building;
document.Save("new-house.bexml");
```

### Running a calculation

```csharp
using Be18_parser.Engine;

using var engine = new BeEngine();
var result = engine.GetKeyXml(document.ToXml());
Console.WriteLine(result.Success ? result.Xml : "The calculation failed.");
```

The [Examples](Examples/) project contains complete, commented versions of these scenarios and can
be run from the command line:

```bash
dotnet run --project Examples -- create new-house.bexml
```

## Concepts

**Documents and entities.** A `Be18Document` holds a header entity, which links to the `Building`.
All other entities, such as windows, ventilation zones and boilers, are reached through the
building's properties.

**Record ids.** Each entity has a unique record id (`Rid`), written as `rid="#12"` in the file.
New entities receive sequential ids automatically. Loaded files keep their ids, and entities created
afterwards never reuse them.

**Enumerations.** Values such as building type and heat supply are exposed as value types with
named members, for example `BuildingType.DetachedHouse`. Values not yet known to the library are
preserved as read.

**File format.** `.bexml` and `.xml` files are identical in content. Files are encoded as
ISO-8859-1, and numbers always use `.` as the decimal separator, regardless of the system culture.

## Project structure

| Path | Description |
| --- | --- |
| [Be18Document.cs](Be18Document.cs) | Loading, saving and creating project files |
| [Models/](Models/) | Entity classes and enumeration types |
| [Serialization/](Serialization/) | Reader and writer for the Be18 file format |
| [Engine/](Engine/) | Wrapper for the Be calculation engine |
| [Examples/](Examples/) | Console application with usage examples |

## Known limitations

- A small number of fields have not yet been mapped to a confirmed meaning; their documentation
  notes this.
- The calculation engine wrapper has not yet been validated against a Be18 installation.
- The library targets .NET 10. Rhino 8 and Revit 2025/2026 run plug-ins on .NET 8, so support for
  those hosts requires an additional target framework. Hosting a 32-bit engine from a 64-bit
  application also requires an out-of-process helper.

## Acknowledgements

The calculation engine wrapper is based on
[beeng](https://github.com/BUILD-Institut-for-Byggeri-By-og-Miljo/beeng-py), BUILD's Python
wrapper for the Be10 and Be15 engines.
