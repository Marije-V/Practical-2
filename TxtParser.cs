using System.Collections;

namespace Practical2;

public class TxtParser : IProgramParser
{
    private StreamReader reader;
    private string currentLine;
    public TxtParser()
    {

    }

    public bool CanParse(string filepath)
    {
        string extension = filepath.Split('.').Last();
        return extension == "txt";
    }

    public List<ICommand> Parse(StreamReader content)
    {
        reader = content;
        currentLine = reader.ReadLine();
        return ParseBlock(0);
    }

    private List<ICommand> ParseBlock(int indent)
    {
        List<ICommand> commands = new List<ICommand>();

        while (currentLine != null)
        {
            // Skip White spaces
            if (string.IsNullOrWhiteSpace(currentLine))
            {
                continue;
            }

            // calculate the current indention
            int level = currentLine.TakeWhile(char.IsWhiteSpace).Count();

            if (level % 4 != 0)
            {
                throw new Exception("The indentation in this file should be 4 spaces.");
            }

            level /= 4;

            if (level < indent)
            {
                return commands;
            }


            string[] words = currentLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            currentLine = reader.ReadLine();

            switch (words[0])
            {
                case "Move":
                    commands.Add(new Move(int.Parse(words[1])));
                    break;
                case "Turn":
                    commands.Add(new Turn(TurnDirection.Left));
                    break;
                case "Repeat":
                    commands.Add(
                        new Repeat(int.Parse(words[1]),
                        ParseBlock(indent++))
                    );
                    break;
                default:
                    break;
            }
        }
        return commands;
    }

}