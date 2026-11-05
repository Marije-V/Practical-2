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

    private void Run(object? sender, RoutedEventArgs e)
    {
        // get the text from the textbox
        string text = ProgramTextBox.Text;

        // update the mainProgram by trying to parse the text from the textblock
        application.TryParse(text);

        // show textual trace in output-textblock
        OutputTextBlock.Text = application.Run();

        // TODO show visual trace in grid
    }

    private void CalculateMetrics(object? sender, RoutedEventArgs e)
    {
        // get the text from the textbox
        string text = ProgramTextBox.Text;

        // update the mainProgram by trying to parse the text from the textblock
        application.TryParse(text);

        // calculate the metrics so we can display them in the textblock output
        OutputTextBlock.Text = application.CalculateMetrics();
    }

    private void LoadBasicExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        ProgramTextBox.Text = application.LoadBasicExampleProgram();
    }

    private void LoadAdvancedExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        ProgramTextBox.Text = application.LoadAdvancedExampleProgram();
    }

    private void LoadExpertExampleProgram(object? sender, RoutedEventArgs e)
    {
        // change mainProgram in application and display the program in the textblock on screen.
        ProgramTextBox.Text = application.LoadExpertExampleProgram();
    }

    private void LoadProgramFromFile(object? sender, RoutedEventArgs e)
    {
        // TODO open file dialog

        // TODO give filepath as argument to function below
        ProgramTextBox.Text = application.LoadProgramFromFile();
    }
}