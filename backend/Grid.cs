namespace Practical2;

public class Grid
{
    private Cell[,] cells;
    private Character character;
    private List<string> textualTrace = new List<string>();
    private List<(int, int)> gridTrace = new List<(int, int)>();

    public Grid(Cell[,] cells)
    {
        this.cells = cells;
        character = new Character();
        gridTrace.Add((character.x, character.y));
    }

    public IReadOnlyList<string> GetTextualTrace()
    {
        return textualTrace;
    }

    public IReadOnlyList<(int, int)> GetGridTrace()
    {
        return gridTrace;
    }

    public void ClearTraces()
    {
        textualTrace.Clear();
        gridTrace.Clear();
    }

    public bool isTrue(Condition c)
    {
        return c switch
        {
            Condition.WallAhead => WallAhead(),
            Condition.GridEdge => GridEdge(),
            _ => false,
        };
    }

    private bool WallAhead()
    {
        (int nextX, int nextY) = nextCell();
        if (GridEdge()) return false;
        if (cells[nextX, nextY] == Cell.Blocked) return true;
        return false;
    }

    private bool GridEdge()
    {
        (int nextX, int nextY) = nextCell();
        if (nextX > cells.GetLength(0) || nextX < 0 || nextY < 0 || nextY > cells.GetLength(1)) return true;
        return false;
    }

    private (int, int) nextCell()
    {
        if (character.facing == Direction.North) return (character.x, character.y - 1);
        if (character.facing == Direction.East) return (character.x + 1, character.y);
        if (character.facing == Direction.South) return (character.x, character.y + 1);
        if (character.facing == Direction.West) return (character.x - 1, character.y);
        return (0, 0);
    }

    public void MoveCharacterForward(int steps)
    {
        if (steps < 0)
        {
            throw new Exception("Character can't move negative steps.");
        }
        int takenSteps = 0;
        while (takenSteps < steps)
        {
            if (WallAhead())
            {
                throw new Exception("Character can't move forward, there is a wall ahead.");
            }
            if (GridEdge())
            {
                throw new Exception("Character can't move forward, they are on the edge of the grid.");
            }
            character.MoveForward(1);
            gridTrace.Add((character.x, character.y));
            takenSteps++;
        }
        // TODO check if this exception thing works
        textualTrace.Add($"Move {steps}");
    }

    public void TurnCharacter(TurnDirection turnDirection)
    {
        character.Turn(turnDirection);
        textualTrace.Add($"Turn {turnDirection}");
    }

    public string CharacterPosition()
    {
        return character.PositionToString();
    }

    public string CharacterDirection()
    {
        return character.DirectionToString();
    }

}

public enum Cell
{
    Empty,
    Blocked,
    Goal
}

public enum Condition
{
    WallAhead,
    GridEdge
}