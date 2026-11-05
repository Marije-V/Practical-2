using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Practical2;
using System.ComponentModel;

namespace Cody.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private Application application = new Application();

    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public string OutputText { get; set; } = "output will appear here...";
    public string ProgramText { get; set; } = "";

    public void Run()
    {
        string toBeParsed = ProgramText;
        // update the mainProgram by trying to parse the text from the textblock

        application.Run(); // will return the textual trace?

        OutputText = "Tried running the program";
    }

    public void CalculateMetrics()
    {
        // get the text from the textblock
        // update the mainProgram by trying to parse the text from the textblock
        application.CalculateMetrics(); //make this function just return the textual metrics
        OutputText = "Tried calculating metrics";
    }

    public void LoadBasicExampleProgram()
    {
        application.LoadBasicExampleProgram(); // changes mainProgram in application
        ProgramText = "Tried loading a basic example program.";
    }

    public void LoadAdvancedExampleProgram()
    {
        application.LoadAdvancedExampleProgram(); // changes mainProgram in application
        ProgramText = "Tried loading an advanced example program.";
    }

    public void LoadExpertExampleProgram()
    {
        application.LoadExpertExampleProgram(); // changes mainProgram in application
        ProgramText = "Tried loading an expert example program.";
    }

    public void LoadProgramFromFile()
    {
        // open file dialog
        // give filepath as argument to function below
        //application.LoadProgramFromFile();
        ProgramText = "Tried loading a program from file.";
    }
}
