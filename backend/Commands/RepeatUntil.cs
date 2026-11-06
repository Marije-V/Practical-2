namespace Practical2;

public class RepeatUntil : ICommand, IHasChildren
{
    private Condition condition;
    private List<ICommand> commands;

    public RepeatUntil(Condition condition, List<ICommand> commands)
    {
        this.condition = condition;
        this.commands = commands;
    }

    // Execute the commands inside of the repeat function
    public void Execute(Grid grid)
    {
        while (grid.isTrue(condition))
        {
            foreach (ICommand command in commands)
            {
                command.Execute(grid);
            }
        }
    }

    public List<ICommand> GetChildren()
    {
        return commands;
    }

    public override string ToString()
    {
        return $"RepeatUntil {condition}";
    }
}