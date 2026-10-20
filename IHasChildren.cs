interface IHasChildren
{
    IReadOnlyList<ICommand> GetChildren();
}