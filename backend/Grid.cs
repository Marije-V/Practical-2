using System.Reflection.Metadata.Ecma335;

namespace Practical2;

public class Grid
{
    private readonly Cell[,] _cells;
    public Cell[,] GetCells() => _cells;

    private readonly Character _character;
    private readonly List<string> _textualTrace = new List<string>();
    private readonly List<(int, int, Direction)> _gridTrace = new List<(int, int, Direction)>();

    public Grid(Cell[,] cells)
    {
        _cells = cells;
        _character = new Character();
        _gridTrace.Add((_character.x, _character.y, _character.facing));
    }
    public string ExerciseResult()
    {
        if (!HasGoalCell()) return "Finished.";
        return _cells[_character.x, _character.y] == Cell.Goal ? "Success: goal reached." : "Failure: goal not reached.";
    }
    public bool HasGoalCell()
        => _cells.Cast<Cell>().Contains(Cell.Goal);
    public IReadOnlyList<string> GetTextualTrace()
        => _textualTrace;
    public IReadOnlyList<(int, int, Direction)> GetGridTrace()
        => _gridTrace;

    public void ClearTraces()
    {
        _textualTrace.Clear();
        _gridTrace.Clear();
    }

    public bool IsTrue(Condition c)
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
        (int nextX, int nextY) = NextCell();
        if (GridEdge()) return false;
        if (_cells[nextX, nextY] == Cell.Blocked) return true;
        return false;
    }

    private bool GridEdge()
    {
        (int nextX, int nextY) = NextCell();
        if (nextX >= _cells.GetLength(0) || nextX < 0 || nextY < 0 || nextY >= _cells.GetLength(1)) return true;
        return false;
    }

    public void MoveCharacterForward(int steps)
    {
        if (steps < 0)
        {
            throw new FormatException("Character can't move negative steps.");
        }
        int takenSteps = 0;
        while (takenSteps < steps)
        {
            if (WallAhead())
            {
                throw new WallAheadException();
            }
            if (GridEdge())
            {
                throw new GridEdgeException();
            }
            _character.MoveForward(1);
            _gridTrace.Add((_character.x, _character.y, _character.facing));
            takenSteps++;
        }
        _textualTrace.Add($"Move {steps}");
    }

    public void TurnCharacter(TurnDirection turnDirection)
    {
        _character.Turn(turnDirection);
        _gridTrace.Add((_character.x, _character.y, _character.facing));
        _textualTrace.Add($"Turn {turnDirection}");
    }

    public string CharacterPosition()
        => _character.PositionToString();

    public string CharacterDirection()
        => _character.DirectionToString();

    private (int X, int Y) NextCell()
    {
        return _character.facing switch
        {
            Direction.North => (_character.x, _character.y - 1),
            Direction.East => (_character.x + 1, _character.y),
            Direction.South => (_character.x, _character.y + 1),
            _ => (_character.x - 1, _character.y),
        };
    }
}

public class WallAheadException : Exception
{
    public WallAheadException() : base("Character can't move forward, there is a wall ahead.") { }
}
public class GridEdgeException : Exception
{
    public GridEdgeException() : base("Character can't move forward, they are on the edge of the grid.") { }
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
