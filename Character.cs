namespace Practical2;

public class Character
{
    private int x, y;
    private Direction facing;
    private List<string> trace;

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
        if (facing == Direction.North) y += steps;
        if (facing == Direction.East) x += steps;
        if (facing == Direction.South) y -= steps;
        if (facing == Direction.West) x -= steps;
        trace.Add($"Move {steps}");
    }

    public void Turn(TurnDirection turnDirection)
    {
        if (turnDirection == TurnDirection.Left) facing--;
        if (turnDirection == TurnDirection.Right) facing++;
        // TODO check if this works or do we need to do if .. then "left" etc.?
        trace.Add($"Turn {turnDirection}");
    }

    public IReadOnlyList<string> GetTrace()
    {
        return trace;
    }

    public string PositionToString()
    {
        return $"({x}, {y})";
    }

    public string DirectionToString()
    {
        return facing.ToString();
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}