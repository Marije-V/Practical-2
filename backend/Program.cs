namespace Practical2;

public class Program
{
    public string name;
    private List<ICommand> commands;
    private Grid grid;
    public Program(string name, List<ICommand> commands)
    {
        this.name = name;
        this.commands = commands;
        grid = new Grid(new Cell[20, 20]);
    }

    // Loop through all the commands and execute it with a fresh character
    public Grid Execute()
    {
        grid.ClearTraces();
        foreach (ICommand command in commands)
        {
            command.Execute(grid);
        }
        return grid;
    }

    // Return the list of commands
    public IReadOnlyList<ICommand> GetCommands()
    {
        return commands;
    }

    public override string ToString()
    {
        string result = "";
        foreach (ICommand c in commands)
        {
            if (c is IHasChildren hasChildren)
            {
                result += c.ToString() + "\n";
                result += ToStringHelper(1, hasChildren.GetChildren());
            }
            else
            {
                result += c.ToString() + "\n";
            }
        }
        return result;
    }

    private string ToStringHelper(int indentation, List<ICommand> children)
    {
        string result = "";
        foreach (ICommand c in children)
        {
            if (c is IHasChildren hasChildren)
            {
                result += new string(' ', 4 * indentation) + c.ToString() + "\n";
                result += ToStringHelper(indentation + 1, hasChildren.GetChildren());
            }
            else
            {
                result += new string(' ', 4 * indentation) + c.ToString() + "\n";
            }
        }
        return result;
    }
}