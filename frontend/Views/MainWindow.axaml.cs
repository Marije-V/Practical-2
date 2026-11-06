using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using Practical2;
using System.Collections.Generic;

namespace Cody.Views;

public partial class MainWindow : Window
{
    private Application application = new Application();
    public MainWindow()
    {
        InitializeComponent();
    }

    // TODO? make the program code (text) have different colors for functions, numbers, direction and condition

    private void Run(object? sender, RoutedEventArgs e)
    {
        // update the mainProgram by trying to parse the text from the textblock
        application.TryParseText(ProgramName.Text, ProgramTextBox.Text);

        // show textual trace in output-textblock
        OutputTextBlock.Text = application.Run();

        // TODO show visual trace in grid
    }

    private void CalculateMetrics(object? sender, RoutedEventArgs e)
    {
        // update the mainProgram by trying to parse the text from the textblock
        application.TryParseText(ProgramName.Text, ProgramTextBox.Text);

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

        // TODO display on screen
    }

    private void LoadExerciseFromFile(object? sender, RoutedEventArgs e)
    {
        // TODO open file dialog
        // https://docs.avaloniaui.net/docs/how-to/dialogs-how-to

        // TODO display on screen
    }
}