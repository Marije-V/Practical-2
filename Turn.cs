namespace Practical2;

public class Turn : ICommand
{
    private TurnDirection turnDirection;

    public Turn(TurnDirection d)
    {
        turnDirection = d;
    }

    public void Execute(Character character)
    {
    }
}

public enum TurnDirection
{
    Left,
    Right
}