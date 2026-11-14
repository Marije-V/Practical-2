using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Practical2;
using Avalonia.Media;
using Avalonia.Threading;

namespace Cody.Views;

public partial class MainWindow : Window
{
    private string pathName = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, "../../../../Examples"));
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
            DrawGrid();
            step = 0;
            timer.Start();
            OutputTextBlock.Text = "Running...";
            foreach (var command in program.GetCommands()) command.Execute(application.grid);
            runResult = string.Join(", ", application.grid.GetTextualTrace()) + $".\nEnd state: {application.grid.CharacterPosition()} facing {application.grid.CharacterDirection()}.\n{application.grid.ExerciseResult()}";
        }
        catch (Exception error) { runResult = "Runtime error: " + error.Message; OutputTextBlock.Text = timer.IsEnabled ? "Running..." : runResult; }
    }

    private void DrawNextStep(object? sender, EventArgs e)
    {
        var trace = application.grid.GetGridTrace();
        if (step >= trace.Count) { timer.Stop(); OutputTextBlock.Text = runResult; return; }
        var (x, y, facing) = trace[step++];
        if (application.grid.HasGoalCell())
        {
            // Walking tiles coloring
            var tiles = this.FindControl<Avalonia.Controls.Primitives.UniformGrid>("Tiles")!;
            if (application.grid.GetCells()[x, y] != Cell.Goal)
                ((Border)tiles.Children[y * tiles.Columns + x]).Background = Brushes.LightBlue;
        }
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

    private async void LoadProgramFromFile(object? sender, RoutedEventArgs e)
    {
        try
        {
            var folder = await StorageProvider.TryGetFolderFromPathAsync(new Uri(pathName));
            var files = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                Title = "Load program",
                AllowMultiple = false,
                SuggestedStartLocation = folder,
                FileTypeFilter = [new Avalonia.Platform.Storage.FilePickerFileType("Text files") { Patterns = ["*.txt"] }]
            });
            if (files.Count == 0) return;

            // open the file
            using var stream = await files[0].OpenReadAsync();
            using var reader = new System.IO.StreamReader(stream);
            string text = await reader.ReadToEndAsync();

            // give the file to the application and try to parse it
            string name = System.IO.Path.GetFileNameWithoutExtension(files[0].Name);
            string error = application.TryParseText(name, text);
            if (error != "") { OutputTextBlock.Text = error; return; }

            // load the correct info everywhere and reset the timer
            timer.Stop();
            ProgramTextBox.Text = text;
            ProgramName.Text = name;
            OutputTextBlock.Text = $"Loaded program: {files[0].Name}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not load program: " + error.Message; }
    }

    private async void SaveProgram(object? sender, RoutedEventArgs e)
    {
        try
        {
            var folder = await StorageProvider.TryGetFolderFromPathAsync(new Uri(pathName));
            // uses avalonia filepicker
            var file = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
            {
                Title = "Save program",
                SuggestedStartLocation = folder,
                SuggestedFileName = string.IsNullOrWhiteSpace(ProgramName.Text) ? "Untitled Program" : ProgramName.Text,
                DefaultExtension = "txt",
                ShowOverwritePrompt = true,
                FileTypeChoices = [new Avalonia.Platform.Storage.FilePickerFileType("Text files") { Patterns = ["*.txt"] }]
            });
            if (file == null) return;

            // Open the file and put in the program text, if the text is empty make the file empty aswell 
            using (var stream = await file.OpenWriteAsync())
            {
                stream.SetLength(0);
                using var writer = new System.IO.StreamWriter(stream);
                await writer.WriteAsync(ProgramTextBox.Text ?? "");
            }

            // Give the cody app the correct programname and give an output
            ProgramName.Text = System.IO.Path.GetFileNameWithoutExtension(file.Name);
            OutputTextBlock.Text = $"Saved program: {file.Name}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not save program: " + error.Message; }
    }

    private void LoadRandomExercise(object? sender, RoutedEventArgs e)
    {
        // load random exercise in application
        application.LoadRandomExercise();
        DrawGrid();

        // TODO display on screen
    }

    private async void LoadExerciseFromFile(object? sender, RoutedEventArgs e)
    {
        try
        {
            var folder = await StorageProvider.TryGetFolderFromPathAsync(new Uri(pathName));
            var files = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                Title = "Load exercise",
                AllowMultiple = false,
                SuggestedStartLocation = folder,
                FileTypeFilter = [new Avalonia.Platform.Storage.FilePickerFileType("Text files") { Patterns = ["*.txt"] }]
            });
            if (files.Count == 0) return;

            // Open the files content
            using var stream = await files[0].OpenReadAsync();
            using var reader = new System.IO.StreamReader(stream);

            // put the file into rows of string
            string[] rows = (await reader.ReadToEndAsync()).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length == 0) throw new FormatException("The exercise file is empty.");

            // Make a square grid
            var cells = new Cell[rows.Length, rows.Length];
            for (int y = 0; y < rows.Length; y++)
            {
                if (rows[y].Length != rows.Length) throw new FormatException("The current grid view requires a square exercise.");

                // Parse each cell to give a correct Cell back
                for (int x = 0; x < rows.Length; x++)
                    cells[x, y] = rows[y][x] switch { 'o' => Cell.Empty, '+' => Cell.Blocked, 'x' => Cell.Goal, _ => throw new FormatException("Use o, + and x for exercise cells.") };
            }
            // Reset timer and reset the whole grid
            timer.Stop();
            GridSize.Value = rows.Length;
            application.grid = new Practical2.Grid(cells);
            DrawGrid();
            OutputTextBlock.Text = $"Loaded exercise: {files[0].Name}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not load exercise: " + error.Message; }
    }

    private void GridSize_ValueChanged(object? sender, RoutedEventArgs e)
    {
        // change the gridsize in application if the value is not null
        if (GridSize?.Value != null)
            application.ChangeGridSize((int)GridSize.Value);
        if (cody != null) DrawGrid();
    }
}
