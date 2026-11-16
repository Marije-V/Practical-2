using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Practical2;

namespace Cody.Views;

public partial class GridView : UserControl
{
    private const double DisplayWidth = 350;
    private const double DisplayHeight = 350;

    private int columns;
    private int rows;
    private double tileWidth;
    private double tileHeight;
    private bool showVisitedCells;

    public GridView()
    {
        InitializeComponent();
    }

    public void DisplayGrid(Practical2.Grid grid)
    {
        var cells = grid.GetCells();
        columns = cells.GetLength(0);
        rows = cells.GetLength(1);

        Tiles.Children.Clear();
        if (columns == 0 || rows == 0)
        {
            CodyTile.IsVisible = false;
            return;
        }

        Tiles.Columns = columns;
        Tiles.Rows = rows;
        tileWidth = DisplayWidth / columns;
        tileHeight = DisplayHeight / rows;
        showVisitedCells = grid.HasGoalCell();

        CodyTile.Width = tileWidth;
        CodyTile.Height = tileHeight;
        CodyTile.IsVisible = true;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Cell cell = cells[x, y];
                Tiles.Children.Add(new Border
                {
                    Background = GetCellBrush(cell),
                    BorderBrush = Brushes.Tan,
                    BorderThickness = new Thickness(1),
                    Tag = cell
                });
            }
        }

        // Matches the backend default starting position.
        DisplayCharacter(0, 0, Direction.East, markVisited: false);
    }

    public void DisplayCharacter(int x, int y, Direction direction, bool markVisited = true)
    {
        if (x < 0 || x >= columns || y < 0 || y >= rows)
            return;

        CodyTile.RenderTransform = new TranslateTransform(x * tileWidth, y * tileHeight);
        CodyCharacter.RenderTransform = new RotateTransform(GetRotation(direction));

        if (markVisited && showVisitedCells)
        {
            var tile = (Border)Tiles.Children[y * columns + x];
            if (tile.Tag is Cell.Empty)
                tile.Background = Brushes.LightBlue;
        }
    }

    private static IBrush GetCellBrush(Cell cell) => cell switch
    {
        Cell.Blocked => Brushes.DimGray,
        Cell.Goal => Brushes.LightGreen,
        _ => Brushes.AntiqueWhite
    };

    private static double GetRotation(Direction direction) => direction switch
    {
        Direction.North => -180,
        Direction.East => -90,
        Direction.South => 0,
        Direction.West => 90,
        _ => 0
    };
}
