namespace Practical2;

public class Application
{
    public static void Main(string[] args)
    {
        ExampleLoader ex = new ExampleLoader();
        Program program;
        Metrics metrics;

        // choosing a program:
        Console.Write("Type 'e' for a random example program, or upload your own file by typing the filepath");
        string input = Console.ReadLine();
        if (input == "e")
        {
            program = ex.GetRandomExample();
        }
        else
        {
            program = FileImporter.Import(input);
        }

        // choosing executing or calculating metrics:
        Console.Write("Type 'e' if you want to execute, and 'm' if you want to calculate the metrics of this program");
        input = Console.ReadLine();
        if (input == "e")
        {
            Character c = program.Execute();
            IReadOnlyList<string> trace = c.GetTrace();
            for (int t = 0; t < trace.Count - 1; t++)
            {
                Console.Write(trace[t]);
                Console.Write(", ");
            }
            Console.Write(trace);
            Console.WriteLine(".");
            Console.WriteLine($"End state: {c.PositionToString()} facing {c.DirectionToString()}.");
        }
        else if (input == "m")
        {
            //TODO formatting mooi maken met aantal whitespaces toevoegen enzo
            metrics = MetricsCalculator.Calculate(program);
            Console.WriteLine("+----------+-------+");
            Console.WriteLine("|Metrics:  |Amount:|");
            Console.WriteLine("+----------+-------+");
            Console.WriteLine($"|Commands  |{metrics.CommandCount}     |");
        }
        else
        {
            Console.WriteLine("Wow! You're so funny! However, we asked you to either type 'e' or 'm'.");
            Console.WriteLine("Congratulations. You broke the program.");
            Console.WriteLine(">:(");
        }
    }
}