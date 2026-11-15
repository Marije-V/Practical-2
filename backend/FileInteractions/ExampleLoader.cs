namespace Practical2;

public class ExampleLoader : IExampleLoader
{
    private readonly List<Program> _programs;
    private readonly List<Program> _basicPrograms;
    private readonly List<Program> _advancedPrograms;
    private readonly List<Program> _expertPrograms;
    public ExampleLoader()
    {
        _programs = new List<Program>
        {
            //Example 1
            new Program("Rectangle1", new List<ICommand>
            {
                new Move(10),
                new Turn(TurnDirection.Right),
                new Move(10),
                new Turn(TurnDirection.Right),
                new Move(10),
                new Turn(TurnDirection.Right),
                new Move(10),
                new Turn(TurnDirection.Right)
            }),
            // Example 2
            new Program("Rectangle2", new List<ICommand>
            {
                new Repeat(4, new List<ICommand>
                {
                    new Move(10),
                    new Turn(TurnDirection.Right)
                })
            }),
            // Example 3
            new Program("Random", new List<ICommand>
            {
                new Move(5),
                new Turn(TurnDirection.Left),
                new Turn(TurnDirection.Left),
                new Move(3),
                new Turn(TurnDirection.Right),
                new Repeat(3, new List<ICommand>
                {
                    new Move(1),
                    new Turn(TurnDirection.Right),
                    new Repeat(5, new List<ICommand>
                    {
                        new Move(2)
                    })
                }),
                new Turn(TurnDirection.Left)
            }),
            // Example 4
            new Program("Heart", new List<ICommand>
            {
                new Repeat(2, new List<ICommand>
                {
                    new Turn(TurnDirection.Left),
                    new Move(1),
                    new Turn(TurnDirection.Right),
                    new Move(1)
                }),
                new Turn(TurnDirection.Right),
                new Move(1),
                new Turn(TurnDirection.Left),
                new Move(1),
                new Turn(TurnDirection.Right),
                new Move(2),
                new Repeat(3, new List<ICommand>
                {
                    new Turn(TurnDirection.Right),
                    new Move(1),
                    new Turn(TurnDirection.Left),
                    new Move(1)
                }),
                new Turn(TurnDirection.Right),
                new Repeat(3, new List<ICommand>
                {
                    new Turn(TurnDirection.Right),
                    new Move(1),
                    new Turn(TurnDirection.Left),
                    new Move(1)
                }),
                new Turn(TurnDirection.Right),
                new Move(2),
                new Turn(TurnDirection.Right),
                new Move(1),
                new Turn(TurnDirection.Left),
                new Move(1),
                new Turn(TurnDirection.Right),
                new Repeat(2, new List<ICommand>
                {
                    new Move(1),
                    new Turn(TurnDirection.Right),
                    new Move(1),
                    new Turn(TurnDirection.Left),
                })
            })
        };
        _basicPrograms = new List<Program>
        {
            // Rectangle Basic
            new Program("Rectangle1", new List<ICommand>
            {
                new Move(10),
                new Turn(TurnDirection.Right),
                new Move(10),
                new Turn(TurnDirection.Right),
                new Move(10),
                new Turn(TurnDirection.Right),
                new Move(10),
                new Turn(TurnDirection.Right)
            })
        };
        _advancedPrograms = new List<Program>
        {
            // Rectangle Advanced
            new Program("Rectangle2", new List<ICommand>
            {
                new Repeat(4, new List<ICommand>
                {
                    new Move(10),
                    new Turn(TurnDirection.Right)
                })
            }),
            // Heart
            new Program("Heart", new List<ICommand>
            {
                new Repeat(2, new List<ICommand>
                {
                    new Turn(TurnDirection.Left),
                    new Move(1),
                    new Turn(TurnDirection.Right),
                    new Move(1)
                }),
                new Turn(TurnDirection.Right),
                new Move(1),
                new Turn(TurnDirection.Left),
                new Move(1),
                new Turn(TurnDirection.Right),
                new Move(2),
                new Repeat(3, new List<ICommand>
                {
                    new Turn(TurnDirection.Right),
                    new Move(1),
                    new Turn(TurnDirection.Left),
                    new Move(1)
                }),
                new Turn(TurnDirection.Right),
                new Repeat(3, new List<ICommand>
                {
                    new Turn(TurnDirection.Right),
                    new Move(1),
                    new Turn(TurnDirection.Left),
                    new Move(1)
                }),
                new Turn(TurnDirection.Right),
                new Move(2),
                new Turn(TurnDirection.Right),
                new Move(1),
                new Turn(TurnDirection.Left),
                new Move(1),
                new Turn(TurnDirection.Right),
                new Repeat(2, new List<ICommand>
                {
                    new Move(1),
                    new Turn(TurnDirection.Right),
                    new Move(1),
                    new Turn(TurnDirection.Left),
                })
            })
        };
        _expertPrograms = new List<Program>
        {
            // Random Expert
            new Program("Random", new List<ICommand>
            {
                new Move(5),
                new Turn(TurnDirection.Left),
                new Turn(TurnDirection.Left),
                new Move(3),
                new Turn(TurnDirection.Right),
                new Repeat(3, new List<ICommand>
                {
                    new Move(1),
                    new Turn(TurnDirection.Right),
                    new Repeat(5, new List<ICommand>
                    {
                        new Move(2)
                    })
                }),
                new Turn(TurnDirection.Left)
            })
        };
        _advancedPrograms.Add(FileImporter.TryParseText("FindEdges", "Repeat 4\n    RepeatUntil GridEdge\n        Move 1\n    Turn Right"));
        _expertPrograms.Add(FileImporter.TryParseText("FindEdgesNested", "Repeat 2\n    Repeat 2\n        RepeatUntil GridEdge\n            Move 1\n        Turn Right"));
    }

    public Program GetRandomExample()
    {
        var rand = new Random();
        return _programs[rand.Next(0, _programs.Count)];
    }

    public Program GetRandomExampleByDifficulty(Difficulty difficulty)
    {
        var rand = new Random();
        if (difficulty == Difficulty.Basic)
        {
            return _basicPrograms[rand.Next(0, _basicPrograms.Count)];
        }
        if (difficulty == Difficulty.Advanced)
        {
            return _advancedPrograms[rand.Next(0, _advancedPrograms.Count)];
        }

        return _expertPrograms[rand.Next(0, _expertPrograms.Count)];

    }
}
public interface IExampleLoader
{
    Program GetRandomExampleByDifficulty(Difficulty difficulty);
}

public enum Difficulty
{
    Basic,
    Advanced,
    Expert
}
