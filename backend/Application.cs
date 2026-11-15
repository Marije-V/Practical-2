using System.Runtime.CompilerServices;

namespace Practical2;


public class Application
{
    private Program _mainProgram;
    private readonly IExampleLoader _exampleLoader = new ExampleLoader();

    private readonly ExerciseLoader _exerciseLoader = new ExerciseLoader();
    public Grid Grid { get; private set; }

    private const string LineArt = "----------+--------";
    public Application() : this(new ExampleLoader())
    {

    }
    public Application(IExampleLoader exampleLoader)
    {
        _exampleLoader = exampleLoader;
        _mainProgram = new Program("empty", []);
        Grid = new Grid(new Cell[10, 10]);
    }
    public static void Main(string[] args)
    {
        Program program;
        ExampleLoader ex = new ExampleLoader();
        Metrics metrics;
        // choosing a program:
        Console.Write("Type 'e' for a random example program, or upload your own file by typing the filepath: ");
        string input = Console.ReadLine();
        if (input == "e")
        {
            // Get a random example program
            program = ex.GetRandomExample();
            Console.WriteLine($"Random selection gave you program: {program.Name}.");
        }
        else
        {
            // Get the file from a certain filepath
            FileImporter importer = new FileImporter();
            program = importer.Import(input);
        }

        // choosing executing or calculating metrics:
        Console.Write("Type 'e' if you want to execute, and 'm' if you want to calculate the metrics of this program: ");
        input = Console.ReadLine();
        if (input == "e")
        {
            // execute the program you have selected
            Grid g = program.Execute();
            IReadOnlyList<string> trace = g.GetTextualTrace();
            for (int t = 0; t < trace.Count - 1; t++)
            {
                Console.Write(trace[t]);
                Console.Write(", ");
            }
            Console.Write(trace.Last());
            Console.WriteLine(".");
            Console.WriteLine($"End state: {g.CharacterPosition()} facing {g.CharacterDirection()}.");
        }
        else if (input == "m")
        {
            // Calculate the metrics for the program
            metrics = MetricsCalculator.Calculate(program);
            Console.WriteLine(LineArt);
            Console.WriteLine("Metrics:  |Amount:");
            Console.WriteLine(LineArt);
            Console.WriteLine($"Commands  |{metrics.CommandCount}");
            Console.WriteLine($"Repeats   |{metrics.RepeatCount}");
            Console.WriteLine($"Nesting   |{metrics.MaxNestingLevel}");
        }
        else
        {
            // When not giving the correct input
            Console.WriteLine("Wow! You're so funny! However, we asked you to either type 'e' or 'm'.");
            Console.WriteLine("Congratulations. You broke the program.");
            Console.WriteLine(">:(");
        }
    }

    public (string, string) LoadBasicExampleProgram()
    {
        _mainProgram = _exampleLoader.GetRandomExampleByDifficulty(Difficulty.Basic);
        return (_mainProgram.ToString(), _mainProgram.Name);
    }

    public (string, string) LoadAdvancedExampleProgram()
    {
        _mainProgram = _exampleLoader.GetRandomExampleByDifficulty(Difficulty.Advanced);
        return (_mainProgram.ToString(), _mainProgram.Name);
    }

    public (string, string) LoadExpertExampleProgram()
    {
        _mainProgram = _exampleLoader.GetRandomExampleByDifficulty(Difficulty.Expert);
        return (_mainProgram.ToString(), _mainProgram.Name);
    }

    public string Run()
    {
        Grid = new Grid((Cell[,])Grid.GetCells().Clone());
        foreach (var command in _mainProgram.GetCommands())
            command.Execute(Grid);

        return string.Join(", ", Grid.GetTextualTrace()) + $".\nEnd state: {Grid.CharacterPosition()} facing {Grid.CharacterDirection()}.\n{Grid.ExerciseResult()}";
    }

    public void LoadProgram(string name, string text)
    {
        // Assignment happens only after parsing succeeds.
        _mainProgram = FileImporter.TryParseText(name, text);
    }

    public void LoadExercise(string text)
    {
        Grid = GridParser.Parse(text);
    }

    public string CalculateMetrics()
    {
        Metrics metrics;
        metrics = MetricsCalculator.Calculate(_mainProgram);

        string[] result =
        {
            LineArt,
            "Metrics:  |Amount:",
            LineArt,
            $"Commands  |{metrics.CommandCount}",
            $"Repeats   |{metrics.RepeatCount}",
            $"Nesting   |{metrics.MaxNestingLevel}"
        }
        ;
        return string.Join("\n", result);
    }

    public string TryParseText(string name, string filecontent)
    {
        try
        {
            LoadProgram(name, filecontent);
            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return e.Message;
        }
    }

    public void LoadRandomExercise()
    {
        // assign grid an exercisegrid
        var exercise = _exerciseLoader.GetRandomExercise();
        Grid = new Grid((Cell[,])exercise.GetCells().Clone());
    }

    public void ChangeGridSize(int size)
    {
        if (size < 1)
            throw new ArgumentOutOfRangeException(nameof(size), "Grid size must be positive.");
        Grid = new Grid(new Cell[size, size]);
    }
}
