namespace Practical2;

public class Program
{
    private string name;
    private List<ICommand> commands;
    public Program(string name, List<ICommand> commands)
    {
        this.name = name;
        this.commands = commands;
    }

    public Character Execute()
    {
        Character c = new Character();
        foreach (ICommand command in commands)
        {
            command.Execute(c);
        }
        return c;
    }

    public IReadOnlyList<ICommand> GetCommands()
    {
        return commands;
    }
}