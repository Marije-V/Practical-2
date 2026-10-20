namespace Practical2;

public class TxtParser : IProgramParser
{
    public TxtParser()
    {

    }

    public bool CanParse(string filepath)
    {
        return false;
    }

    public Program Parse(string content)
    {
        return new Program(new List<ICommand>());
    }
}