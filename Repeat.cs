using System.Windows.Input;

public class Repeat : ICommand, IHasChildren
{
    private int times;
    private List<ICommand> commands;

    public Repeat(int times, List<ICommand> commands)
    {
        this.times = times;
        this.commands = commands;
    }

    public void Execute(Character character)
    {
        foreach (ICommand command in commands)
        {
            command.execute(character);
        }
    }

    public IReadOnlyList<ICommand> GetChildren()
    {
        return commands;
    }
}