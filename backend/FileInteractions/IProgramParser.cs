namespace Practical2;

public interface IProgramParser
{
    public bool CanParse(string filepath);
    public List<ICommand> ParseWithStreamReader(StreamReader content);
}