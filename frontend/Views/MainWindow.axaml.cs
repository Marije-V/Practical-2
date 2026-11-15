using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Practical2;
using Avalonia.Threading;
using Cody.Services;

namespace Cody.Views;

public partial class MainWindow : Window
{
    private readonly FileDialogService fileDialogs;
    private Application application = new Application();
    private DispatcherTimer timer = new DispatcherTimer();
    private int step;
    private string runResult = "";
    private bool updatingGridSize;
    private GridView? ExerciseGrid => this.FindControl<GridView>("GridDisplay");
    public MainWindow()
    {
        fileDialogs = new FileDialogService(StorageProvider,
            System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, "../../../../Examples")));
        InitializeComponent();
        application.ChangeGridSize(5);
        DrawGrid();
        timer.Interval = TimeSpan.FromMilliseconds(250);
        timer.Tick += DrawNextStep;
    }

    // TODO? make the program code (text) have different colors for functions, numbers, direction and condition

    private void Run(object? sender, RoutedEventArgs e)
    {
        timer.Stop();
        try
        {
            application.LoadProgram(ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            try
            {
                runResult = application.Run();
            }
            catch (Exception error)
            {
                runResult = "Runtime error: " + error.Message;
            }
            DrawGrid();
            step = 0;
            timer.Start();
            OutputTextBlock.Text = "Running...";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not run program: " + error.Message; }
    }

    private void DrawNextStep(object? sender, EventArgs e)
    {
        var trace = application.grid.GetGridTrace();
        if (step >= trace.Count) { timer.Stop(); OutputTextBlock.Text = runResult; return; }
        var (x, y, facing) = trace[step++];
        ExerciseGrid?.DisplayCharacter(x, y, facing);
    }

    private void DrawGrid()
    {
        timer.Stop();
        ExerciseGrid?.DisplayGrid(application.grid);
    }

    private void CalculateMetrics(object? sender, RoutedEventArgs e)
    {
        try
        {
            application.LoadProgram(ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            OutputTextBlock.Text = application.CalculateMetrics();
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not calculate metrics: " + error.Message; }
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
            var file = await fileDialogs.OpenText("Load program");
            if (file is null) return;

            application.LoadProgram(file.Name, file.Text);

            // load the correct info everywhere and reset the timer
            timer.Stop();
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
            var fileName = await fileDialogs.SaveText("Save program", ProgramName.Text ?? "", ProgramTextBox.Text ?? "");
            if (fileName is null) return;

            ProgramName.Text = System.IO.Path.GetFileNameWithoutExtension(fileName);
            OutputTextBlock.Text = $"Saved program: {fileName}";
        }
        catch (Exception error) { OutputTextBlock.Text = "Could not save program: " + error.Message; }
    }

    private void LoadRandomExercise(object? sender, RoutedEventArgs e)
    {
        // load random exercise in application
        application.LoadRandomExercise();
        UpdateGridSizeDisplay();
        DrawGrid();

        // TODO display on screen
    }

    private async void LoadExerciseFromFile(object? sender, RoutedEventArgs e)
    {
        try
        {
            var file = await fileDialogs.OpenText("Load exercise");
            if (file is null) return;

            application.LoadExercise(file.Text);
            timer.Stop();
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
            application.ChangeGridSize((int)GridSize.Value);
        if (ExerciseGrid != null) DrawGrid();
    }

    private void UpdateGridSizeDisplay()
    {
        // Reflect the loaded grid without replacing it through ValueChanged.
        updatingGridSize = true;
        try { GridSize.Value = application.grid.GetCells().GetLength(0); }
        finally { updatingGridSize = false; }
    }
}
