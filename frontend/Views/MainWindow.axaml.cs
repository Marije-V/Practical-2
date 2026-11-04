using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using Practical2;

namespace Cody.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        // Get a random example program
        ExampleLoader ex = new ExampleLoader();
        Practical2.Program program = ex.GetRandomExample();
        Console.WriteLine($"Random selection gave you program: {program.name}.");
    }
}