using System.Text;

namespace Practical2;

public class Program
{
    public string Name;
    private readonly List<ICommand> _commands;
    private readonly Grid _grid;
    public Program(string name, List<ICommand> commands)
    {
        Name = name;
        _commands = commands;
        _grid = new Grid(new Cell[20, 20]);
    }

    // Loop through all the commands and execute it with a fresh character
    public Grid Execute()
    {
        _grid.ClearTraces();
        foreach (ICommand command in _commands)
        {
            command.Execute(_grid);
        }
        return _grid;
    }

    // Return the list of commands
    public IReadOnlyList<ICommand> GetCommands()
        => _commands;


    public override string ToString()
    {
        StringBuilder build = new();
        foreach (ICommand c in _commands)
        {
            build.Append(c.ToString() + "\n");
            if (c is IHasChildren hasChildren)
            {
                build.Append(ToStringHelper(1, hasChildren.GetChildren()));
            }
        }
        return build.ToString();
    }

    private static string ToStringHelper(int indentation, List<ICommand> children)
    {
        StringBuilder build = new();
        foreach (ICommand c in children)
        {
            build.Append(new string('\t', indentation) + c.ToString() + "\n");
            if (c is IHasChildren hasChildren)
            {
                build.Append(ToStringHelper(indentation + 1, hasChildren.GetChildren()));
            }
        }
        return build.ToString();
    }
}
