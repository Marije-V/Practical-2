namespace Practical2;

public static class GridParser
{
    public static Grid Parse(string text)
    {
        string[] rows = text.Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (rows.Length == 0)
            throw new FormatException("The exercise file is empty.");

        var cells = new Cell[rows.Length, rows.Length];
        for (int y = 0; y < rows.Length; y++)
        {
            if (rows[y].Length != rows.Length)
                throw new FormatException("An exercise must contain a square grid.");

            for (int x = 0; x < rows.Length; x++)
            {
                cells[x, y] = rows[y][x] switch
                {
                    'o' => Cell.Empty,
                    '+' => Cell.Blocked,
                    'x' => Cell.Goal,
                    _ => throw new FormatException("Use o, + and x for exercise cells.")
                };
            }
        }

        return new Grid(cells);
    }
}
