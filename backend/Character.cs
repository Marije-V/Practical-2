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
        Direction rightNow = facing;
        if (turnDirection == TurnDirection.Left)
        {
            if (rightNow == Direction.North) facing = Direction.West;
            if (rightNow == Direction.East) facing = Direction.North;
            if (rightNow == Direction.South) facing = Direction.East;
            if (rightNow == Direction.West) facing = Direction.South;
        }
        if (turnDirection == TurnDirection.Right)
        {
            if (rightNow == Direction.North) facing = Direction.East;
            if (rightNow == Direction.East) facing = Direction.South;
            if (rightNow == Direction.South) facing = Direction.West;
            if (rightNow == Direction.West) facing = Direction.North;
        }
    }

    public string PositionToString()
    {
        return $"({x}, {y})";
    }

    public string DirectionToString()
    {
        return $"{facing}";
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}