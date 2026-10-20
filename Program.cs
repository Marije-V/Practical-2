namespace Practical2;

public class Program
{
    private string name;
    private List<ICommand> commands;
    public Program(List<ICommand> commands)
    {
        this.commands = commands;
    }

    public void Execute()
    {
        Character c = new Character();
        foreach (ICommand command in commands)
        {
            command.Execute(c);
        }
    }

    public IReadOnlyList<ICommand> GetCommands()
    {
        return commands;
    }

}