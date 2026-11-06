namespace Practical2;

public class Turn : ICommand
{
    private TurnDirection turnDirection;

    public Turn(TurnDirection d)
    {
        turnDirection = d;
    }

    public void Execute(Grid grid)
    {
        grid.TurnCharacter(turnDirection);
    }

    public override string ToString()
    {
        return $"Turn {turnDirection}";
    }
}

public enum TurnDirection
{
    Left,
    Right
}