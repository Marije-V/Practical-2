using System.Text;
using Practical2;

namespace TestProject1;

public class CharacterTest
{
    [Fact]
    public void StandardCharacter()
    {
        string expPosition = "(0, 0)";
        string expdirection = "East";
        var character = new Character();
        Assert.Equal(character.PositionToString(), expPosition);
        Assert.Equal(character.DirectionToString(), expdirection);
    }
    [Theory]
    [InlineData(Direction.North, 0, -5)]
    [InlineData(Direction.East, 5, 0)]
    [InlineData(Direction.South, 0, 5)]
    [InlineData(Direction.West, -5, 0)]
    public void MoveForward(Direction direction, int expectedX, int expectedY)
    {
        var character = new Character(0, 0, direction);
        character.MoveForward(5);
        string expectedString = $"({expectedX}, {expectedY})";
        Assert.Equal(expectedString, character.PositionToString());
    }

    [Theory]
    [InlineData(TurnDirection.Left, Direction.North, "West")]
    [InlineData(TurnDirection.Left, Direction.East, "North")]
    [InlineData(TurnDirection.Left, Direction.South, "East")]
    [InlineData(TurnDirection.Left, Direction.West, "South")]
    [InlineData(TurnDirection.Right, Direction.North, "East")]
    [InlineData(TurnDirection.Right, Direction.East, "South")]
    [InlineData(TurnDirection.Right, Direction.South, "West")]
    [InlineData(TurnDirection.Right, Direction.West, "North")]
    public void Turn(TurnDirection direction, Direction currentlyFacing, string expected)
    {
        var character = new Character(0, 0, currentlyFacing);
        character.Turn(direction);
        Assert.Equal(expected, character.DirectionToString());
    }
    [Fact]
    public void Trace()
    {
        var character = new Character();
        character.MoveForward(5);
        character.Turn(TurnDirection.Right);
        character.MoveForward(4);
        character.Turn(TurnDirection.Left);

        var trace = character.GetTrace();
        IReadOnlyList<string> expected = new List<string>
        {
            "Move 5",
            "Turn Right",
            "Move 4",
            "Turn Left"
        };
        Assert.Equal(trace[0], expected[0]);
        Assert.Equal(trace.Count, expected.Count);
        Assert.Equal(trace, expected);
    }
}