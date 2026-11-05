namespace Practical2;

public class ExampleLoader
{
    private List<Program> programs;
    private List<Program> basicPrograms;
    private List<Program> advancedPrograms;
    private List<Program> expertPrograms;
    public ExampleLoader()
    {
        programs = new List<Program>
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
        basicPrograms = new List<Program>
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
        advancedPrograms = new List<Program>
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
        expertPrograms = new List<Program>
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
    }

    public Program GetRandomExample()
    {
        var rand = new Random();
        return programs[rand.Next(0, programs.Count)];
    }

    public Program GetRandomExampleByDifficulty(Difficulty d)
    {
        var rand = new Random();
        if (d == Difficulty.Basic)
        {
            return basicPrograms[rand.Next(0, basicPrograms.Count)];
        }
        if (d == Difficulty.Advanced)
        {
            return advancedPrograms[rand.Next(0, advancedPrograms.Count)];
        }
        if (d == Difficulty.Expert)
        {
            return expertPrograms[rand.Next(0, expertPrograms.Count)];
        }
        return programs[rand.Next(0, programs.Count)];
    }
}

public enum Difficulty
{
    Basic,
    Advanced,
    Expert
}