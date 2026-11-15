namespace Practical2;

public class Repeat : ICommand, IHasChildren
{
    private readonly int _times;
    private readonly List<ICommand> _commands;

    public Repeat(int times, List<ICommand> commands)
    {
        this._times = times;
        this._commands = commands;
    }

    // Execute the commands inside of the repeat function
    public void Execute(Grid grid)
    {
        for (int t = 0; t < _times; t++)
        {
            foreach (ICommand command in _commands)
            {
                command.Execute(grid);
            }
        }
    }

    public List<ICommand> GetChildren()
        => _commands;

    public override string ToString()
        => $"Repeat {_times}";

}