using System.Windows.Input;

namespace Practical2;

public class Program
{
    private List<ICommand> commands;
    private string name;
    private Character character;

    public Program()
    {
        name = "Untitled Program";
        character = new Character("Johannes", new Vector2(0, 0), Direction.East);
        commands = new List<ICommand>();
    }

    public void executeCommands()
    {
        foreach (ICommand command in commands)
        {
            command.executeCommand(character);
        }
        Console.WriteLine(ToString());
        Console.WriteLine($"End state: ({character.position.X}, {character.position.Y}) facing {character.direction}");
    }

    public string ToString()
    {
        String result = "";
        foreach (ICommand command in commands)
        {
            result += command.ToString() + ", ";
        }
        // fixen dat de laatste ", " een "." wordt. somehow...
        return result;
    }

    public void calculateMetrics()
    {
        // calculate metrics
        Console.WriteLine("Calculating metrics...");
    }
}