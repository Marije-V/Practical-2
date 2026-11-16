using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Practical2;
using Avalonia.Threading;
using Cody.Services;

namespace Cody.Views;

public partial class MainWindow : Window
{
    private readonly FileDialogService _fileDialogs;
    private readonly Application _application = new Application();
    private readonly DispatcherTimer _timer = new DispatcherTimer();
    private int step;
    private string runResult = "";
    private bool updatingGridSize;
    private GridView? ExerciseGrid => this.FindControl<GridView>("GridDisplay");
    public MainWindow()
    {
        _fileDialogs = new FileDialogService(StorageProvider,
            System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, "../../../../Examples")));
        InitializeComponent();
        _application.ChangeGridSize(5);
        DrawGrid();
        _timer.Interval = TimeSpan.FromMilliseconds(250);
        _timer.Tick += DrawNextStep;
    }

    private void Run(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        try
        {
            _application.LoadProgram(ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            try
            {
                runResult = _application.Run();
            }
            catch (Exception error)
            {
                runResult = "Runtime error: " + error.Message;
            }
            DrawGrid();
            step = 0;
            _timer.Start();
            OutputTextBlock.Text = "Running...";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not run program: " + error.Message; }
    }

    private void DrawNextStep(object? sender, EventArgs e)
    {
        var trace = _application.Grid.GetGridTrace();
        if (step >= trace.Count) { _timer.Stop(); OutputTextBlock.Text = runResult; return; }
        var (x, y, facing) = trace[step++];
        ExerciseGrid?.DisplayCharacter(x, y, facing);
    }

    private void DrawGrid()
    {
        _timer.Stop();
        ExerciseGrid?.DisplayGrid(_application.Grid);
    }

    private void CalculateMetrics(object? sender, RoutedEventArgs e)
    {
        try
        {
            _application.LoadProgram(ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            OutputTextBlock.Text = _application.CalculateMetrics();
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not calculate metrics: " + error.Message; }
    }

    private void LoadBasicExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        (ProgramTextBox.Text, ProgramName.Text) = _application.LoadBasicExampleProgram();
    }

    private void LoadAdvancedExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        (ProgramTextBox.Text, ProgramName.Text) = _application.LoadAdvancedExampleProgram();
    }

    private void LoadExpertExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        (ProgramTextBox.Text, ProgramName.Text) = _application.LoadExpertExampleProgram();
    }

    private async void LoadProgramFromFile(object? sender, RoutedEventArgs e)
    {
        try
        {
            var file = await _fileDialogs.OpenText("Load program");
            if (file is null) return;

            _application.LoadProgram(file.Name, file.Text);

            // load the correct info everywhere and reset the timer
            _timer.Stop();
            ProgramTextBox.Text = file.Text;
            ProgramName.Text = file.Name;
            OutputTextBlock.Text = $"Loaded program: {file.FileName}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not load program: " + error.Message; }
    }

    private async void SaveProgram(object? sender, RoutedEventArgs e)
    {
        try
        {
            var fileName = await _fileDialogs.SaveText("Save program", ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            if (fileName is null) return;

            ProgramName.Text = System.IO.Path.GetFileNameWithoutExtension(fileName);
            OutputTextBlock.Text = $"Saved program: {fileName}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not save program: " + error.Message; }
    }

    private void LoadRandomExercise(object? sender, RoutedEventArgs e)
    {
        // load random exercise in application
        _application.LoadRandomExercise();
        UpdateGridSizeDisplay();
        DrawGrid();
    }

    private async void LoadExerciseFromFile(object? sender, RoutedEventArgs e)
    {
        try
        {
            var file = await _fileDialogs.OpenText("Load exercise");
            if (file is null) return;

            _application.LoadExercise(file.Text);
            _timer.Stop();
            UpdateGridSizeDisplay();
            DrawGrid();
            OutputTextBlock.Text = $"Loaded exercise: {file.FileName}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not load exercise: " + error.Message; }
    }

    private void GridSize_ValueChanged(object? sender, RoutedEventArgs e)
    {
        if (updatingGridSize) return;
        // change the gridsize in application if the value is not null
        if (GridSize?.Value != null)
            _application.ChangeGridSize((int)GridSize.Value);
        if (ExerciseGrid != null) DrawGrid();
    }

    private void UpdateGridSizeDisplay()
    {
        // Reflect the loaded grid without replacing it through ValueChanged.
        updatingGridSize = true;
        try { GridSize.Value = _application.Grid.GetCells().GetLength(0); }
        finally { updatingGridSize = false; }
    }
}
