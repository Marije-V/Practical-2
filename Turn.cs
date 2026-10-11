namespace Practical2;

class Turn : ICommand
{
    private TurnType way;

    public Turn(TurnType way)
    {
        this.way = way;
    }
    public String ToString()
    {
        return $"Turn {way}";
    }
    public void executeCommand(Character character)
    {
        if (way == TurnType.Right) character.direction++;
        if (way == TurnType.Left) character.direction--;
    }
}

enum TurnType
{
    Left,
    Right
}