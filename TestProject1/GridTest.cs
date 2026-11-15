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
}
