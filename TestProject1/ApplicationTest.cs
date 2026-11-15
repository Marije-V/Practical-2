using Moq;
using Practical2;

namespace TestProject1;

public class ApplicationTest
{
    [Theory]
    [InlineData("e", "m", "Random selection gave", "Metrics:  |Amount:")]
    [InlineData("path", "e", "Move 1, Turn Right.", "End state: (1, 0) facing South.")]
    [InlineData("path", "f", "Congratulations.", ">:(")]
    public void MainHandlesUserChoices(
        string source, string choice, string expectedOutput, string expectedDetail)
    {
        var originalIn = Console.In;
        var originalOut = Console.Out;
        string input = source == "path"
            ? Path.Combine(AppContext.BaseDirectory, "TestMain.txt")
            : source;
        using var reader = new StringReader($"{input}\n{choice}\n");
        using var output = new StringWriter();

        try
        {
            Console.SetIn(reader);
            Console.SetOut(output);

            Application.Main([]);

            Assert.Contains(expectedOutput, output.ToString());
            Assert.Contains(expectedDetail, output.ToString());
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }

    [Theory]
    [InlineData(Difficulty.Basic)]
    [InlineData(Difficulty.Advanced)]
    [InlineData(Difficulty.Expert)]
    public void LoadExampleReturnsProgramTextAndName(Difficulty difficulty)
    {
        var program = new Program("Test example", [new Move(2)]);
        var loader = new Mock<IExampleLoader>();
        loader.Setup(l => l.GetRandomExampleByDifficulty(difficulty)).Returns(program);
        var application = new Application(loader.Object);

        var result = difficulty switch
        {
            Difficulty.Basic => application.LoadBasicExampleProgram(),
            Difficulty.Advanced => application.LoadAdvancedExampleProgram(),
            _ => application.LoadExpertExampleProgram()
        };

        Assert.Equal(program.ToString(), result.Item1);
        Assert.Equal("Test example", result.Item2);
        loader.Verify(l => l.GetRandomExampleByDifficulty(difficulty), Times.Once());
    }

    [Fact]
    public void LoadedProgramProvidesMetricsAndRestartsOnEachRun()
    {
        var application = new Application();
        application.LoadExercise("oox\nooo\nooo");
        application.LoadProgram("test", "Repeat 2\n    Move 1");

        string metrics = application.CalculateMetrics();
        Assert.Contains("Commands  |2", metrics);
        Assert.Contains("Repeats   |1", metrics);
        Assert.Contains("Nesting   |1", metrics);

        application.Run();
        string result = application.Run();

        Assert.Equal("(2, 0)", application.Grid.CharacterPosition());
        Assert.Contains("Success: goal reached.", result);
        Assert.Equal(Cell.Goal, application.Grid.GetCells()[2, 0]);
    }

    [Fact]
    public void FailedParsingPreservesTheLastValidProgram()
    {
        var application = new Application();
        Assert.Equal("", application.TryParseText("valid", "Move 1"));

        string error = application.TryParseText("invalid", "Move banana");

        Assert.NotEmpty(error);
        Assert.Contains("(1, 0)", application.Run());
    }

    [Fact]
    public void RandomExerciseReplacesTheGridWithAFreshExercise()
    {
        var application = new Application();
        var original = application.Grid;

        application.LoadRandomExercise();

        Assert.NotSame(original, application.Grid);
        Assert.True(application.Grid.HasGoalCell());
        Assert.Equal("(0, 0)", application.Grid.CharacterPosition());
    }

    [Fact]
    public void GridSizeAcceptsPositiveValuesAndRejectsInvalidValues()
    {
        var application = new Application();
        Assert.Equal(10, application.Grid.GetCells().GetLength(0));

        application.ChangeGridSize(3);

        Assert.Equal(3, application.Grid.GetCells().GetLength(0));
        Assert.Equal(3, application.Grid.GetCells().GetLength(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => application.ChangeGridSize(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => application.ChangeGridSize(-1));
    }
}
