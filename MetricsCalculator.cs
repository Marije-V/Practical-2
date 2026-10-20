namespace Practical2;

public static class MetricsCalculator
{
    public static Metrics Calculate(Program program)
    {
        // TODO figure this shit out (specifacillay the GetChildren())
        int commands = 0;
        int repeats = 0;
        int nesting = 0;
        Metrics temp;
        foreach (ICommand c in program.GetCommands())
        {
            if (c is IHasChildren hasChildren)
            {
                repeats++;
                nesting++;
                temp = Traverse(hasChildren.GetChildren(), 1);
                commands += temp.CommandCount;
                repeats += temp.RepeatCount;
                nesting = temp.MaxNestingLevel + 1; // TODO???
            }
            commands++;
        }
        return new Metrics(commands, repeats, nesting);
    }

    private static Metrics Traverse(List<ICommand> commands, int depth)
    {
        int c = 0;
        int r = 0;
        // TODO figure this shit out (with the depth and maximum shit and idk)
        int n = depth;
        foreach (ICommand command in commands)
        {
            if (command.GetType() == typeof(IHasChildren))
            {
                r++;
                n++;
                Traverse((IHasChildren)command.GetChildren(), depth + 1);
            }
            c++;
        }
        return new Metrics(c, r, n);
    }
}