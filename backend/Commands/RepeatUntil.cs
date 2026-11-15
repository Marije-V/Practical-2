namespace Practical2;

public class RepeatUntil : ICommand, IHasChildren
{
    private readonly Condition _condition;
    private readonly List<ICommand> _commands;

    public RepeatUntil(Condition condition, List<ICommand> commands)
    {
        _condition = condition;
        _commands = commands;
    }

    // Execute the commands inside of the repeat function
    public void Execute(Grid grid)
    {
        int iterations = 0;
        while (!grid.IsTrue(_condition))
        {
            if (++iterations > 10000) throw new InvalidOperationException("RepeatUntil stopped after 10,000 iterations.");
            foreach (ICommand command in _commands)
            {
                command.Execute(grid);
            }
        }
    }

    public List<ICommand> GetChildren()
        => _commands;

    public override string ToString()
        => $"RepeatUntil {_condition}";

}
