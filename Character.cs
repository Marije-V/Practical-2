using System.Numerics;

namespace Practical2;

public class Character
{
    public string name;
    public Vector2 position;
    public Direction direction;

    public Character()
    {
        name = "Unnamed Character";
        position = new Vector2(0, 0);
        direction = Direction.East;
    }

    public Character(string name, (Int, Int) pos, Direction d)
    {
        this.name = name;
        position = pos;
        direction = d;
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}