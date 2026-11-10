# Examples

A console app showing how to use the Be18-parser library. Each example is a small, commented
class:

| Example | File | What it shows |
| --- | --- | --- |
| `load` | [LoadExample.cs](LoadExample.cs) | Load a `.bexml`/`.xml` file and read data from the models |
| `write` | [WriteExample.cs](WriteExample.cs) | Load a file, change data and save it as a new file |
| `create` | [CreateProjectExample.cs](CreateProjectExample.cs) | Create a new project from the models and save it |
| `calculate` | [CalculationExample.cs](CalculationExample.cs) | Run a file through a locally installed Be calculation engine |

`.bexml` and `.xml` files have the same content, so every example works with both.

## Running

From the repository root:

```bash
dotnet run --project Examples -- create my-house.bexml
dotnet run --project Examples -- load my-house.bexml
dotnet run --project Examples -- write my-house.bexml my-house-changed.bexml
```

The Be10 and Be15 calculation engines are 32-bit DLLs, which only a 32-bit process can load, so
run the calculation example as x86. It needs the x86 .NET runtime and an installed Be with a valid
license:

```bash
dotnet run --project Examples -a x86 -- calculate my-house.bexml
```

Pass the path to the engine DLL as a third argument if it is not found automatically.
