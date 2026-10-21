namespace Practical2;

public static class MetricsCalculator
{
    public static Metrics Calculate(Program program)
    {
        int commands = 0;
        int repeats = 0;
        int nesting = 0;
        Metrics temp;

        // Move through the list of ICommands in program
        foreach (ICommand c in program.GetCommands())
        {
            // you are moving through 1 command so commands++
            commands++;

            if (c is IHasChildren hasChildren)
            {
                // The command has children, so in this case it is also a repeat, as we don't have if statements or while loops
                repeats++;
                // loop through the indented children and give their indention level
                temp = Traverse(hasChildren.GetChildren(), 1);

                // add the commands and repeats of the travers
                commands += temp.CommandCount;
                repeats += temp.RepeatCount;

                // if the nesting of the traverse is higher than the current nesting, replace the nesting
                if (nesting < temp.MaxNestingLevel) nesting = temp.MaxNestingLevel;
            }
        }
        // Return the total metrics
        return new Metrics(commands, repeats, nesting);
    }

    // This function is the calculate but with a depth indicater and a list of commands instead 
    private static Metrics Traverse(List<ICommand> commands, int depth)
    {
        int com = 0;
        int repeats = 0;
        int nesting = depth;
        Metrics temp;
        foreach (ICommand command in commands)
        {
            com++;
            if (command is IHasChildren hasChildren)
            {
                repeats++;
                temp = Traverse(hasChildren.GetChildren(), depth + 1);
                com += temp.CommandCount;
                repeats += temp.RepeatCount;
                if (nesting < temp.MaxNestingLevel) nesting = temp.MaxNestingLevel;
            }
        }
        return new Metrics(com, repeats, nesting);
    }
}