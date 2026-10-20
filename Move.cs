namespace Practical2;

public class Move : ICommand
{
    private int steps;

    public Move(int steps)
    {
        this.steps = steps;
    }

    public void Execute(Character character)
    {
        character.MoveForward(steps);
    }
}