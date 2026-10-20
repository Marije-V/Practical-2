public class Program
{
    private List<ICommand> commands;
    public Program(List<ICommand> commands)
    {
        this.commands = commands;
    }

    public void Execute()
    {
        foreach (ICommand command in commands)
        {
            command.executeCommand(character);
        }
    }

    public IReadOnlyList<ICommand> GetCommands()
    {
        return commands;
    }

}