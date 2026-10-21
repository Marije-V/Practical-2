namespace Practical2;

public class Program
{
    public string name;
    private List<ICommand> commands;
    public Program(string name, List<ICommand> commands)
    {
        this.name = name;
        this.commands = commands;
    }

    // Loop through all the commands and execute it with a fresh character
    public Character Execute()
    {
        Character c = new Character();
        foreach (ICommand command in commands)
        {
            command.Execute(c);
        }
        return c;
    }

    // Return the list of commands
    public IReadOnlyList<ICommand> GetCommands()
    {
        return commands;
    }
}