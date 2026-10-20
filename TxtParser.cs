using System.Collections;

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

    public Program Parse(StreamReader context)
    {
        int currentWhiteSpaceLevel = 0;
        string? currentLine = context.ReadLine();
        string[] words = currentLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        int whiteSpaces = currentLine.TakeWhile(char.IsWhiteSpace).Count();

        do
        {
            switch (words[0])
            {
                case "Move":

                    break;
                case "Turn":
                    break;
                case "Repeat":
                    break;
            }
            currentLine = context.ReadLine();
        } while (currentLine.IsWhiteSpace());

        List<ICommand> commandList = new List<ICommand>();


        return new Program(new List<ICommand>());
    }

}