namespace Practical2;

public interface IProgramParser
{
    public bool CanParse(string extension);
    public Program Parse(string content);
}