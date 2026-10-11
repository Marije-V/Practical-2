using System.Windows.Input;

namespace Practical2;

class Repeat : ICommand
{
    private int repeat;
    private List<ICommand> commands;

    public Repeat(int repeat)
    {
        this.repeat = repeat;
        commands = new List<ICommand>();
    }
    public Repeat(int repeat, List<ICommand> commands)
    {
        this.repeat = repeat;
        this.commands = commands;
    }
    public String ToString()
    {
        String result = "";
        for (int n = 0; n < repeat; n++)
        {
            foreach (ICommand command in commands)
            {
                result += command.ToString() + ", ";
            }
        }
        return result;
    }
    public void executeCommand(Character character)
    {
        foreach (ICommand command in commands)
        {
            command.executeCommand(character);
        }
    }
}