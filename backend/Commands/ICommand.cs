namespace Practical2;

public interface ICommand
{
    public void Execute(Grid grid);
    public string ToString();
}