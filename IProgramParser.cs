namespace Practical2;

public interface IProgramParser
{
    public bool CanParse(string extension);
    public List<ICommand> Parse(StreamReader content);
}