namespace Practical2;

public class Turn : ICommand
{
    private readonly TurnDirection _turnDirection;

    public Turn(TurnDirection d)
    {
        _turnDirection = d;
    }

    public void Execute(Grid grid)
        => grid.TurnCharacter(_turnDirection);

    public override string ToString()
        => $"Turn {_turnDirection}";

}

public enum TurnDirection
{
    Left,
    Right
}