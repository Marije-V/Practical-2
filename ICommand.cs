namespace Practical2;

interface ICommand
{
    string ToString();
    void executeCommand(Character character);
}