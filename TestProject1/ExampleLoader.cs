
using System.Runtime.CompilerServices;
using Practical2;

namespace TestProject1;

public class ExampleLoaderTest
{
    List<Program> programs;
    public ExampleLoaderTest()
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
    [Fact]
    public void TestRandomLoader()
    {
        ExampleLoader exampleLoader = new ExampleLoader();
        var randomTest = exampleLoader.GetRandomExample();

        bool isProgramFromTest = false;
        foreach (Program program in programs)
        {
            if (program.Name == randomTest.Name)
            {
                isProgramFromTest = true;
                break;
            }
        }
        Assert.True(isProgramFromTest);
    }

    [Theory]
    [InlineData(Difficulty.Basic)]
    [InlineData(Difficulty.Advanced)]
    [InlineData(Difficulty.Expert)]
    public void TestRandomExampleByDifficulty(Difficulty difficulty)
    {
        var loader = new ExampleLoader();

        var program = loader.GetRandomExampleByDifficulty(difficulty);

        string[] expectedNames = difficulty switch
        {
            Difficulty.Basic => ["Rectangle1"],
            Difficulty.Advanced => ["Rectangle2", "Heart", "FindEdges"],
            Difficulty.Expert => ["Random", "FindEdgesNested"],
            _ => []
        };

        Assert.Contains(program.Name, expectedNames);
    }
}