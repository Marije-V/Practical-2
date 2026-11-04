namespace Practical2;

public class ExampleLoader
{
    private List<Program> programs;
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
    }

    public Program GetRandomExample()
    {
        var rand = new Random();
        return programs[rand.Next(0, programs.Count)];
    }
}