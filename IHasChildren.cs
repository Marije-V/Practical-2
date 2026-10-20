namespace Practical2;

public interface IHasChildren
{
    public IReadOnlyList<ICommand> GetChildren();
}