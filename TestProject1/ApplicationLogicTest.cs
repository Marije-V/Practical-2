using Practical2;

namespace TestProject1;

public class ApplicationLogicTest
{
    [Fact]
    public void LoadExerciseParsesCellsByColumnAndRow()
    {
        var application = new Application();
        application.LoadExercise("o+\r\nox\r\n");

        var cells = application.grid.GetCells();
        Assert.Equal(Cell.Blocked, cells[1, 0]);
        Assert.Equal(Cell.Empty, cells[0, 1]);
        Assert.Equal(Cell.Goal, cells[1, 1]);
    }

    [Theory]
    [InlineData("")]
    [InlineData("oo\no")]
    [InlineData("o?\nox")]
    public void InvalidExerciseCurrentGridUnchanged(string text)
    {
        var application = new Application();
        var original = application.grid;

        Assert.Throws<FormatException>(() => application.LoadExercise(text));
        Assert.Same(original, application.grid);
    }

    [Fact]
    public void EachRunResetsCharacterAndPreservesSelectedExercise()
    {
        var application = new Application();
        application.LoadExercise("oox\nooo\nooo");
        application.LoadProgram("test", "Move 1");

        application.Run();
        application.Run();

        Assert.Equal("(1, 0)", application.grid.CharacterPosition());
        Assert.Equal(Cell.Goal, application.grid.GetCells()[2, 0]);
        Assert.Equal(2, application.grid.GetGridTrace().Count);
    }

    [Fact]
    public void FailedMovementRetainsSuccessfulStepsForPlayback()
    {
        var application = new Application();
        application.LoadExercise("oo+\nooo\noox");
        application.LoadProgram("test", "Move 3");

        Assert.Throws<WallAheadException>(() => application.Run());
        Assert.Equal("(1, 0)", application.grid.CharacterPosition());
        Assert.Equal(2, application.grid.GetGridTrace().Count);
    }
}
