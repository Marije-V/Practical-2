using System.Numerics;

namespace Practical2;

public class Character
{
    public string name;

    // Either change to (int, int) or change the rest to Vector2
    public Vector2 position;
    public Direction direction;

    public Character()
    {
        name = "Unnamed Character";
        position = new Vector2(0, 0);
        direction = Direction.East;
    }

    public Character(string name, Vector2 pos, Direction d)
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