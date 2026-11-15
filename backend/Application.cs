namespace Practical2;

public class Application
{
    private Program mainProgram;
    private ExampleLoader exampleLoader = new ExampleLoader();

    private ExerciseLoader exerciseLoader = new ExerciseLoader();
    public Grid grid { get; private set; }

    public Application()
    {
        mainProgram = new Program("empty", []);
        grid = new Grid(new Cell[10, 10]);
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
            Console.WriteLine($"Random selection gave you program: {program.name}.");
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
            Console.WriteLine("----------+--------");
            Console.WriteLine("Metrics:  |Amount:");
            Console.WriteLine("----------+--------");
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
        mainProgram = exampleLoader.GetRandomExampleByDifficulty(Difficulty.Basic);
        return (mainProgram.ToString(), mainProgram.name);
    }

    public (string, string) LoadAdvancedExampleProgram()
    {
        mainProgram = exampleLoader.GetRandomExampleByDifficulty(Difficulty.Advanced);
        return (mainProgram.ToString(), mainProgram.name);
    }

    public (string, string) LoadExpertExampleProgram()
    {
        mainProgram = exampleLoader.GetRandomExampleByDifficulty(Difficulty.Expert);
        return (mainProgram.ToString(), mainProgram.name);
    }

    public string LoadProgramFromFile()
    {
        // TODO let this function take a filepath as argument?
        FileImporter importer = new FileImporter();
        return "Move 0";
    }

    public string Run()
    {
        grid = new Grid((Cell[,])grid.GetCells().Clone());
        foreach (var command in mainProgram.GetCommands())
            command.Execute(grid);

        return string.Join(", ", grid.GetTextualTrace()) + $".\nEnd state: {grid.CharacterPosition()} facing {grid.CharacterDirection()}.\n{grid.ExerciseResult()}";
    }

    public void LoadProgram(string name, string text)
    {
        // Assignment happens only after parsing succeeds.
        mainProgram = new FileImporter().TryParseText(name, text);
    }

    public void LoadExercise(string text)
    {
        grid = GridParser.Parse(text);
    }

    public string CalculateMetrics()
    {
        Metrics metrics;
        metrics = MetricsCalculator.Calculate(mainProgram);

        string[] result =
        {
            "----------+--------",
            "Metrics:  |Amount:",
            "----------+--------",
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
        var exercise = exerciseLoader.GetRandomExercise();
        grid = new Grid((Cell[,])exercise.GetCells().Clone());
    }

    public void ChangeGridSize(int size)
    {
        if (size < 1)
            throw new ArgumentOutOfRangeException(nameof(size), "Grid size must be positive.");
        grid = new Grid(new Cell[size, size]);
    }
}
