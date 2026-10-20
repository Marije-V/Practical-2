public class Turn : ICommand
{
    private TurnDirection turnDirection;

    public Turn(TurnDirection d)
    {
        turnDirection = d;
    }

    public void Execute(Character character)
    {
        if (turnDirection == TurnDirection.Right) character.direction++;
        if (turnDirection == TurnDirection.Left) character.direction--;
    }
}

public enum TurnDirection
{
    Left,
    Right
}