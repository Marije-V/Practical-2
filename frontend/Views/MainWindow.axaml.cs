using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Practical2;
using Avalonia.Media;
using Avalonia.Threading;

namespace Cody.Views;

public partial class MainWindow : Window
{
    private Application application = new Application();
    private DispatcherTimer timer = new DispatcherTimer();
    private int step;
    private string runResult = "";
    private Image cody => this.FindControl<Image>("CodyCharacter")!;
    private Border codyTile => this.FindControl<Border>("CodyTile")!;
    public MainWindow()
    {
        InitializeComponent();
        application.ChangeGridSize(5);
        DrawGrid();
        timer.Interval = TimeSpan.FromMilliseconds(250);
        timer.Tick += DrawNextStep;
        //Closed += (_, _) => timer.Stop();
    }

    // TODO? make the program code (text) have different colors for functions, numbers, direction and condition

    private void Run(object? sender, RoutedEventArgs e)
    {
        timer.Stop();
        try
        {
            var program = new FileImporter().TryParseText(ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            application.grid = new Practical2.Grid(application.grid.GetCells());
            step = 0;
            timer.Start();
            OutputTextBlock.Text = "Running...";
            foreach (var command in program.GetCommands()) command.Execute(application.grid);
            runResult = string.Join(", ", application.grid.GetTextualTrace()) + $".\nEnd state: {application.grid.CharacterPosition()} facing {application.grid.CharacterDirection()}.";
        }
        catch (Exception error) { runResult = error.Message; OutputTextBlock.Text = timer.IsEnabled ? "Running..." : runResult; }
    }

    private void DrawNextStep(object? sender, EventArgs e)
    {
        var trace = application.grid.GetGridTrace();
        if (step >= trace.Count) { timer.Stop(); OutputTextBlock.Text = runResult; return; }
        var (x, y, facing) = trace[step++];
        codyTile.RenderTransform = new TranslateTransform(x * codyTile.Width, y * codyTile.Height);
        cody.RenderTransform = new RotateTransform((int)facing * 90 - 180);
    }

    private void DrawGrid()
    {
        timer.Stop();
        var cells = application.grid.GetCells();
        var tiles = this.FindControl<Avalonia.Controls.Primitives.UniformGrid>("Tiles")!;
        tiles.Children.Clear();
        tiles.Columns = cells.GetLength(0);
        codyTile.Width = codyTile.Height = 350.0 / tiles.Columns;
        codyTile.RenderTransform = new TranslateTransform();
        cody.RenderTransform = new RotateTransform(-90);
        for (int y = 0; y < cells.GetLength(1); y++)
            for (int x = 0; x < cells.GetLength(0); x++)
                tiles.Children.Add(new Border
                {
                    Background = cells[x, y] == Cell.Blocked ? Brushes.DimGray : cells[x, y] == Cell.Goal ? Brushes.LightGreen : Brushes.AntiqueWhite,
                    BorderBrush = Brushes.Tan,
                    BorderThickness = new Avalonia.Thickness(1)
                });
    }

    private void CalculateMetrics(object? sender, RoutedEventArgs e)
    {
        // update the mainProgram by trying to parse the text from the textblock
        OutputTextBlock.Text = application.TryParseText(ProgramName.Text, ProgramTextBox.Text);

        // calculate the metrics so we can display them in the textblock output
        OutputTextBlock.Text = application.CalculateMetrics();
    }

    private void LoadBasicExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        (ProgramTextBox.Text, ProgramName.Text) = application.LoadBasicExampleProgram();
    }

    private void LoadAdvancedExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        (ProgramTextBox.Text, ProgramName.Text) = application.LoadAdvancedExampleProgram();
    }

    private void LoadExpertExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        (ProgramTextBox.Text, ProgramName.Text) = application.LoadExpertExampleProgram();
    }

    private void LoadProgramFromFile(object? sender, RoutedEventArgs e)
    {
        // TODO open file dialog
        // https://docs.avaloniaui.net/docs/how-to/dialogs-how-to

        // TODO give filepath as argument to function below
        ProgramTextBox.Text = application.LoadProgramFromFile();
    }

    private void SaveProgram(object? sender, RoutedEventArgs e)
    {
        // TODO open file dialog?
        // https://docs.avaloniaui.net/docs/how-to/dialogs-how-to

        // TODO do something with the program name (ProgramName) textbox

        // TODO write to textfile and save on computer
    }

    private void LoadRandomExercise(object? sender, RoutedEventArgs e)
    {
        // load random exercise in application
        application.LoadRandomExercise();
        DrawGrid();

        // TODO display on screen
    }

    private void LoadExerciseFromFile(object? sender, RoutedEventArgs e)
    {
        // TODO open file dialog
        // https://docs.avaloniaui.net/docs/how-to/dialogs-how-to

        // TODO display on screen
    }

    private void GridSize_ValueChanged(object? sender, RoutedEventArgs e)
    {
        // change the gridsize in application if the value is not null
        if (GridSize?.Value != null)
            application.ChangeGridSize((int)GridSize.Value);
        if (cody != null) DrawGrid();
    }
}
