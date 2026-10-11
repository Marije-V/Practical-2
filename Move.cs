namespace Practical2;

class Move : ICommand
{
    private int steps;

    public Move(int steps)
    {
        this.steps = steps;
    }
    public string ToString()
    {
        return $"Move {steps}";
    }
    public void executeCommand(Character character)
    {
        (int x, int y) = character.position;
        if (character.direction == Direction.North) y -= steps;
        if (character.direction == Direction.East) x += steps;
        if (character.direction == Direction.South) y += steps;
        if (character.direction == Direction.West) x -= steps;
    }
}