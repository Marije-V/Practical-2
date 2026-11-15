using System.Dynamic;

namespace Practical2;

public class Character
{
    public int x { get; private set; }
    public int y { get; private set; }
    public Direction facing { get; private set; }

    public Character()
    {
        x = 0;
        y = 0;
        facing = Direction.East;
    }

    public Character(int x, int y, Direction d)
    {
        this.x = x;
        this.y = y;
        facing = d;
    }

    public void MoveForward(int steps)
    {
        if (facing == Direction.North) y -= steps;
        if (facing == Direction.East) x += steps;
        if (facing == Direction.South) y += steps;
        if (facing == Direction.West) x -= steps;
    }

    // Change the enum direction depending on the turn and the compass direction
    public void Turn(TurnDirection turnDirection)
    {
        int offset = turnDirection == TurnDirection.Left ? 3 : 1;
        facing = (Direction)(((int)facing + offset) % 4);
    }

    public string PositionToString()
        => $"({x}, {y})";

    public string DirectionToString()
        => $"{facing}";

}

public enum Direction
{
    North,
    East,
    South,
    West
}