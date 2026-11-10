# Be18-parser

A C# class library for reading and writing project files from **Be18**, the Danish building energy
assessment tool used to document compliance with the energy requirements of the Danish building
regulations (BR18).

Be18 saves projects as XML in `.bexml` or `.xml` files; both contain exactly the same thing. This
library turns those files into C# classes, so other applications can be built on top of Be18
projects: Rhino/Grasshopper plug-ins, Revit/Dynamo plug-ins, UI apps and so on. It can also run a
project through a locally installed Be calculation engine.

## Features

- **Load and save** `.bexml`/`.xml` files. A file that is loaded and saved without changes is
  written back byte for byte.
- **Typed models** for all 33 entity types in Be's files: building, windows and doors, opaque
  constructions, thermal bridges, ventilation, lighting, boilers, heat pumps, solar heating, solar
  cells, heat distribution, domestic hot water and more. References between entities are ordinary
  object references.
- **Danish labels in the docs.** Each property's doc comment gives its meaning, unit and the Danish
  label used in the Be18 user interface, such as `("Udluftning")`.
- **Nothing is lost.** Fields and entity types the library does not know yet are kept and written
  back unchanged.
- **New projects from code.** New entities get unique, sequential ids automatically.
- **Calculation engine wrapper.** A C# version of BUILD's Python package
  [beeng](https://github.com/BUILD-Institut-for-Byggeri-By-og-Miljo/beeng-py) runs a project
  through the Be engine DLL.

## Requirements

- .NET 10 SDK.
- For calculations: Windows, an installed Be with a valid license, and a process with the same
  bitness as the engine DLL. The Be10 and Be15 engines are 32-bit, so they need an x86 process.

## Getting started

Build the library and the examples:

```bash
dotnet build Be18-parser.slnx
```

### Load a file

```csharp
using Be18_parser;

var document = Be18Document.Load("house.bexml");
var building = document.Building!;

Console.WriteLine($"{building.Name}: {building.HeatedFloorArea} m²");
foreach (var window in building.TransparentConstructions)
    Console.WriteLine($"{window.Name}: {window.Count} x {window.Area} m², U = {window.U}");
```

### Change and save a file

```csharp
building.HeatingSetpoint = 21;
foreach (var construction in building.OpaqueConstructions)
    construction.U *= 0.9;

document.Save("house-changed.bexml");
```

### Create a new project

```csharp
using Be18_parser.Models;

Be18Entity.ResetRidCounter();          // number the new project from #1
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
    Name = "Windows towards south", Count = 4, Area = 2.0, U = 0.8, Orientation = "s", Slope = 90,
});

document.Building = building;          // everything reachable from the building is saved
document.Save("new-house.bexml");
```

### Run a calculation

```csharp
using Be18_parser.Engine;

using var engine = new BeEngine();     // finds the engine in the current folder or the registry
var result = engine.GetKeyXml(document.ToXml());
Console.WriteLine(result.Success ? result.Xml : "The calculation failed.");
```

More complete, commented examples are in [Examples/](Examples/), a console app you can run:

```bash
dotnet run --project Examples -- create my-house.bexml
```

## Project layout

| Path | Contents |
| --- | --- |
| [Be18Document.cs](Be18Document.cs) | Entry point: load, save and create files |
| [Models/](Models/) | One class per entity type, plus enumeration types in `Models/Enums/` |
| [Serialization/](Serialization/) | Reader and writer used by `Be18Document` |
| [Engine/](Engine/) | Wrapper for the Be calculation engine DLL |
| [Examples/](Examples/) | Console app with usage examples |
| `TestFiles/` | Local Be files for development. Ignored by git, because they may contain project data |

## Status and limitations

- **File format.** The models are based on Be15 version 8 example files. Be18 uses the same file
  structure.
- **Unconfirmed fields.** A few fields still have no confirmed meaning, for example `ae2`, the
  mechanical cooling fields and some circulation pump fields. Their doc comments say so.
- **Engine calls untested.** The calls into the engine DLL follow the beeng package but have not
  been run against a real engine yet. The name `Be18Eng.dll` is assumed.
- **New projects untested.** Projects created from code mirror the structure of Be's own files,
  but have not been checked by the engine yet.
- **Plug-in frameworks.** The library targets .NET 10. Rhino 8 and Revit 2025/2026 plug-ins run on
  .NET 8, so the library has to be retargeted before they can use it. Since the engine DLLs are
  32-bit, 64-bit hosts like Rhino and Revit would also need a 32-bit helper process to run
  calculations.
