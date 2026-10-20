namespace Practical2;

public class TxtParser : IProgramParser
{
    public TxtParser()
    {

    }

    public bool CanParse(string filepath)
    {
        string extension = filepath.Split('.').Last();
        return extension == "txt";
    }

    public Program Parse(string content)
    {
        return new Program(new List<ICommand>());
    }
}