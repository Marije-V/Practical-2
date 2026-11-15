using Practical2;

namespace TestProject1;

public class GridTest
{
    [Fact]
    public void TraceRecordsMovementAndTurnsInOrder()
    {
        var grid = new Grid(new Cell[6, 5]);
        grid.MoveCharacterForward(5);
        grid.TurnCharacter(TurnDirection.Right);
        grid.MoveCharacterForward(4);
        grid.TurnCharacter(TurnDirection.Left);

        string[] expected =
        [
            "Move 5",
            "Turn Right",
            "Move 4",
            "Turn Left"
        ];

        Assert.Equal(expected, grid.GetTextualTrace());
    }

    [Fact]
    public void IsTrueReturnsTrueWhenWallIsAhead()
    {
        var cells = new Cell[2, 2];
        cells[1, 0] = Cell.Blocked;
        var grid = new Grid(cells);

        Assert.True(grid.IsTrue(Condition.WallAhead));
    }

    [Fact]
    public void IsTrueReturnsTrueAtGridEdge()
    {
        var grid = new Grid(new Cell[1, 1]);

        Assert.True(grid.IsTrue(Condition.GridEdge));
    }

    [Fact]
    public void IsTrueReturnsFalseWhenPathIsClear()
    {
        var grid = new Grid(new Cell[2, 2]);

        Assert.False(grid.IsTrue(Condition.WallAhead));
        Assert.False(grid.IsTrue(Condition.GridEdge));
    }

    [Fact]
    public void IsTrueReturnsFalseForUnknownCondition()
    {
        var grid = new Grid(new Cell[2, 2]);

        Assert.False(grid.IsTrue((Condition)99));
    }

    [Fact]
    public void ThrowExceptionIfCharacterMovesNegativeForward()
    {
        var grid = new Grid(new Cell[1, 1]);
        Assert.Throws<FormatException>(() => grid.MoveCharacterForward(-1));
        Assert.Throws<GridEdgeException>(() => grid.MoveCharacterForward(1));
    }
}
