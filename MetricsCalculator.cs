namespace Practical2;

public static class MetricsCalculator
{
    public static Metrics Calculate(Program program)
    {
        int commands = 0;
        int repeats = 0;
        int nesting = 0;
        Metrics temp;
        foreach (ICommand c in program.GetCommands())
        {
            commands++;
            if (c is IHasChildren hasChildren)
            {
                repeats++;
                temp = Traverse(hasChildren.GetChildren(), 1);
                commands += temp.CommandCount;
                repeats += temp.RepeatCount;
                if (nesting < temp.MaxNestingLevel) nesting = temp.MaxNestingLevel;
            }
        }
        return new Metrics(commands, repeats, nesting);
    }

    private static Metrics Traverse(List<ICommand> commands, int depth)
    {
        int c = 0;
        int r = 0;
        int n = depth;
        Metrics temp;
        foreach (ICommand command in commands)
        {
            c++;
            if (command is IHasChildren hasChildren)
            {
                r++;
                temp = Traverse(hasChildren.GetChildren(), depth + 1);
                c += temp.CommandCount;
                r += temp.RepeatCount;
                if (n < temp.MaxNestingLevel) n = temp.MaxNestingLevel;
            }
        }
        return new Metrics(c, r, n);
    }
}