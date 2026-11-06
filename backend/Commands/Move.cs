namespace Practical2;

public class Move : ICommand
{
    private int steps;

    public Move(int steps)
    {
        this.steps = steps;
    }

    public void Execute(Grid grid)
    {
        grid.MoveCharacterForward(steps);
    }

    public override string ToString()
    {
        return $"Move {steps}";
    }
}