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
        int iterations = 0;
        while (!grid.isTrue(condition))
        {
            if (++iterations > 10000) throw new InvalidOperationException("RepeatUntil stopped after 10,000 iterations.");
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
