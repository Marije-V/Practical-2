namespace Practical2;

public class Move : ICommand
{
    private readonly int _steps;

    public Move(int steps)
    {
        this._steps = steps;
    }

    public void Execute(Grid grid)
    {
        grid.MoveCharacterForward(_steps);
    }

    public override string ToString()
    {
        return $"Move {_steps}";
    }
}