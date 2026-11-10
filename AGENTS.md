# AGENTS.md

Guidance for AI coding agents (and humans) working in this repository.

## Project overview

Be18-parser is a C# class library for reading and writing project files from **Be18**, the Danish
building energy assessment tool used to document compliance with the energy requirements of the
Danish building regulations (BR18). Be18 stores projects as XML, with the `.bexml` or `.xml`
extension.

The purpose of the library is to convert `.bexml`/`.xml` data into C# classes so that other
applications can be built on top of it, such as Rhino/Grasshopper plug-ins, Revit/Dynamo plug-ins
and UI apps. Keep the library itself free of dependencies on any of these host applications.

Goals of the library:

- **Read** a Be18 file into a strongly typed object model.
- **Write** the object model back to a file that Be18 can open.
- **Round-trip safely**: reading a file and writing it back without changes should produce a file
  that Be18 treats as identical to the original.

`Be18Document` loads and saves files. A file that is loaded and saved without changes is written
back byte for byte; keep it that way.

## Repository layout

| Path                 | Purpose                                         |
| -------------------- | ----------------------------------------------- |
| `Be18-parser.slnx`   | Solution file (new XML `.slnx` format)          |
| `Be18-parser.csproj` | Class library project                           |
| `Be18Document.cs`    | Entry point: load, save and create files        |
| `Models/`            | Model classes, one per entity type in the file  |
| `Models/Enums/`      | Enumeration value types                         |
| `Serialization/`     | Reader and writer used by `Be18Document`        |
| `Engine/`            | Wrapper that runs files through the Be engine   |
| `Examples/`          | Console app with usage examples (own project)   |
| `TestFiles/`         | Local Be18 sample files (ignored by git)        |

`bin/`, `obj/` and `.vs/` are build or IDE output and are ignored by git.

`TestFiles/` holds real `.bexml`/`.xml` files to develop and test against. It is ignored by git
because the files may contain project data, so never commit its contents or copy them elsewhere in
the repository. Code and tests must not assume it exists or contains any particular file.

## Build and test

Requires the .NET 10 SDK.

```bash
dotnet build Be18-parser.slnx
```

The solution contains the library and `Examples/Be18-parser.Examples.csproj`. The library project
sits in the repository root, so `Be18-parser.csproj` excludes `Examples/**` from its own build;
do the same for any other project added in a subfolder. Keep the examples compiling and in line
with the public API when it changes. See `Examples/README.md` for how to run them.

There is no test project yet. When adding one, put it in its own project (for example
`Be18-parser.Tests/`), add it to `Be18-parser.slnx`, and run it with:

```bash
dotnet test Be18-parser.slnx
```

## Code conventions

- Target framework: `net10.0`. Nullable reference types and implicit usings are enabled; keep the
  build free of nullable warnings.
- Root namespace is `Be18_parser` (Visual Studio replaced the hyphen with an underscore). Use it,
  or sub-namespaces of it, for all types.
- Follow standard .NET naming: PascalCase for types, methods and properties; `_camelCase` for
  private fields.
- Public API types and members get XML doc comments (`///`), since this is a library consumed by
  other code.
- Do not add NuGet dependencies without a clear reason; `System.Xml.Linq` and `System.Xml` in the
  base library cover XML parsing and writing.

## File format

Learned from the example files in `TestFiles/` (Be15 version 8 exports). Be18 is the latest version
of the same application and uses the same file structure as Be15, so these files are representative.

- The root element is `<BE05>`. Each child element is one entity, such as `<BUILDING rid="#2">`.
  The `BE05` entity `#1` is a header whose `id` holds the program version.
- Each field is a child element of its entity, always in the same order for a given entity type.
- Values are encoded as follows:
  - **Booleans:** `.T.` (true) and `.F.` (false).
  - **Enumerations:** an upper-case name between dots, e.g. `.SUP_DISTRICT.`.
  - **Numbers:** `.` as decimal separator, e.g. `0.95`.
  - **References:** another entity's `rid`, e.g. `#12`. `$` means no reference (null).
  - **Lists of references:** space-separated, e.g. `#3 #4 #5`; an empty element is an empty list.
    A list can contain `$`, e.g. `$ #35`.
  - **Text:** as is. An empty element is an empty string, or "no value" for a number.
- The XML declaration says `ISO-8859-1`.
- `.bexml` and `.xml` files have exactly the same content.
- Layout: XML declaration, Be's comment lines, `<BE05>`, then one entity per line indented with a
  tab, then `</BE05>` with no newline after it. Lines end with CRLF. Only `&`, `<` and `>` are
  escaped in text. `Serialization/Be18Writer.cs` reproduces this layout.

## Models

- Every entity type has a class in `Models/` deriving from `Be18Entity`, marked with
  `[Be18Entity("ELEMENT_NAME")]`. Every field is a property marked with `[Be18Field("field_name")]`.
  Fields are written in declaration order (base class first), so keep properties in the same order
  as in the file.
- References are typed object references (`Shading? Shading`, `List<VentilationZone>`), not ids.
  Use `Be18Entity` as the type when no example file shows what a reference points to.
- Enumerations are `readonly record struct` types implementing `IBe18Enum`, not C# enums, so values
  not seen yet survive a round trip. Booleans are `bool`.
- Unknown fields go in `Be18Entity.ExtraFields`; unknown entity types become `UnknownEntity`.
- `Be18Entity.Rid` is the number from `rid="#12"`. Every new entity takes the next rid from a
  counter shared by all entities in the process. The reader does not use the counter (rids come
  from the file) and then moves it past the highest rid in the file. The writer only renumbers
  entities whose rid is not positive or is a duplicate. Rids do not have to be in file order; Be's
  own files are not either.
- Property names are descriptive where the meaning is known. Otherwise they are the field name in
  PascalCase (e.g. `ThStore`) and the doc comment says the meaning is not yet confirmed. Rename them
  once the meaning is confirmed.

## Calculation engine

`Engine/BeEngine.cs` runs a project file through a locally installed Be calculation engine DLL. It
is a C# version of BUILD's Python package
[beeng](https://github.com/BUILD-Institut-for-Byggeri-By-og-Miljo/beeng-py/tree/master/beeng); keep
its behaviour in line with that package unless there is a reason not to.

- The DLL exports `Be06Keys` and `Be06Res` (model XML in, result XML out, non-zero = success),
  `Be06Temp` (project path in, writes `<name>_tmp.xml` next to it, 0 = success) and
  `IsLicenseValid` (0 = valid; only from engine version 8.17.1.17). All use `stdcall` and
  ISO-8859-1 strings.
- The Be10 and Be15 DLLs are 32-bit and can only be loaded by a 32-bit (x86) process. Rhino and
  Revit are 64-bit, so plug-ins cannot load them in-process.
- `BeEngineLocator` looks in the current folder, then under `HKLM\SOFTWARE\WOW6432Node\SBi` for
  `InstallDir` values. The DLL name `Be18Eng.dll` is assumed and has not been confirmed.
- No Be engine is installed on the development machine, so the calls into the DLL itself have not
  been tested. Do not claim they work until they have been run against a real engine.

## Working with Be18 files

- **Do not guess the file format.** Base element names, attributes, units and value ranges on real
  Be18 sample files (see `TestFiles/`) or official documentation.
- **Rockwool Energy Design** is an alternative front end for Be18. Its screen labels (in Danish)
  helped identify many fields, and doc comments quote them. Its XML export adds models and fields
  that Be18 does not use: never add those to this project. Its XML import uploads the file to
  energy.rockwool.dk, so do not import files without the user's permission. If something is unknown, say so instead of inventing
  a schema.
- **Preserve what you don't model.** Unknown elements and attributes must survive a read/write
  round trip, so files from newer Be18 versions are not silently damaged.
- **Numbers and culture.** Parse and format numbers with an explicit culture
  (`CultureInfo.InvariantCulture`, or whatever the sample files show is in use) and never rely on
  the machine's current culture. Danish Windows uses a decimal comma, which silently breaks parsing.
- **Encoding.** Keep the original file's text encoding and XML declaration when writing. Danish
  characters (æ, ø, å) must survive a round trip.
- **Tests.** Every new feature should have a round-trip test against a sample file. Real files
  belong in `TestFiles/` (not committed). Sample files committed with a test project must be small
  and free of customer or project data that is not meant to be public.

## Git

- Default branch is `main`.
- Write commit messages in the imperative mood ("Add wall parser", not "Added wall parser").
- Do not commit build output or IDE files; `.gitignore` already covers them.
