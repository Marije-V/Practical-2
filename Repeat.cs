namespace Practical2;

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
        for (int t = 0; t < times; t++)
        {
            foreach (ICommand command in commands)
            {
                command.Execute(character);
            }
        }
    }

    public IReadOnlyList<ICommand> GetChildren()
    {
        return commands;
    }
}