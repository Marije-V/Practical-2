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
        character.Turn(turnDirection);
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