public class Metrics
{
    public int CommandCount, RepeatCount, MaxNestingLevel;

    public Metrics(int com, int rep, int max)
    {
        CommandCount = com;
        RepeatCount = rep;
        MaxNestingLevel = max;
    }
}