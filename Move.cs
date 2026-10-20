public class Move : ICommand
{
    private int steps;

    public Move(int steps)
    {
        this.steps = steps;
    }

    public void Execute(Character character)
    {
        (int x, int y) = (character.x, character.y);
        if (character.direction == Direction.North) y -= steps;
        if (character.direction == Direction.East) x += steps;
        if (character.direction == Direction.South) y += steps;
        if (character.direction == Direction.West) x -= steps;
    }
}