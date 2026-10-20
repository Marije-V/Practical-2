namespace Practical2;

public class Character
{
    private int x, y;
    private Direction facing;
    private List<string> trace;

    public Character()
    {
        x = 0; y = 0;
        facing = Direction.East;
    }

    public Character(int x, int y, Direction d)
    {
        this.x = x; this.y = y;
        facing = d;
    }

    public void MoveForward(int steps)
    {

    }

    public void Turn(TurnDirection turnDirection)
    {

    }

    public IReadOnlyList<string> GetTrace()
    {
        return trace;
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}