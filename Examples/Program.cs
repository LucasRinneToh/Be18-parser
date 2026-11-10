using Be18_parser.Examples;

// Runs one of the examples. See README.md for how to run them.
switch (args.FirstOrDefault())
{
    case "load" when args.Length == 2:
        LoadExample.Run(args[1]);
        break;
    case "write" when args.Length == 3:
        WriteExample.Run(args[1], args[2]);
        break;
    case "create" when args.Length == 2:
        CreateProjectExample.Run(args[1]);
        break;
    case "calculate" when args.Length is 2 or 3:
        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("The Be calculation engine only runs on Windows.");
            return 1;
        }
        CalculationExample.Run(args[1], args.ElementAtOrDefault(2));
        break;
    default:
        Console.WriteLine("""
            Usage:
              load      <file>                  Load a .bexml/.xml file and print a summary
              write     <input> <output>        Load a file, change it and save it as a new file
              create    <output>                Create a new project from the models and save it
              calculate <file> [engine dll]     Run a file through the Be calculation engine
            """);
        return 1;
}

return 0;
