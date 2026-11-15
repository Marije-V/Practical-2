
using Moq;
using Practical2;

namespace TestProject1;

public class RepeatUntilTest
{
    [Fact]
    public void RepeatsUntilGridEdge()
    {
        var grid = new Grid(new Cell[3, 1]);
        var command = new Mock<ICommand>();
        command.Setup(c => c.Execute(grid))
            .Callback(() => grid.MoveCharacterForward(1));

        var commands = new List<ICommand> { command.Object };
        var repeat = new RepeatUntil(Condition.GridEdge, commands);

        repeat.Execute(grid);

        command.Verify(c => c.Execute(grid), Times.Exactly(2));
        Assert.Same(commands, repeat.GetChildren());
        Assert.Equal("RepeatUntil GridEdge", repeat.ToString());
    }

    [Fact]
    public void SkipsCommandsWhenAlreadyAtEdge()
    {
        var grid = new Grid(new Cell[1, 1]);
        var command = new Mock<ICommand>();
        var repeat = new RepeatUntil(Condition.GridEdge, [command.Object]);

        repeat.Execute(grid);

        command.Verify(c => c.Execute(grid), Times.Never());
    }

    [Fact]
    public void ThrowsWhenCommandsMakeNoProgress()
    {
        var grid = new Grid(new Cell[2, 1]);
        var command = new Mock<ICommand>();
        var repeat = new RepeatUntil(Condition.GridEdge, [command.Object]);

        Assert.Throws<InvalidOperationException>(() => repeat.Execute(grid));
        command.Verify(c => c.Execute(grid), Times.Exactly(10000));
    }
}