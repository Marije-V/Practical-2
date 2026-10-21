using System.Collections;

namespace Practical2;

public class TxtParser : IProgramParser
{
    private StreamReader reader;
    private string currentLine;
    public TxtParser()
    {

    }

    // checks if the parser can parse the file in this filepath
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

    // parses a block from the parser. So like all commands within a repeat. if repeat in repeat it uses recursion
    private List<ICommand> ParseBlock(int indent)
    {
        List<ICommand> commands = new List<ICommand>();

        // Keep going until there is no new line in the textfile
        while (currentLine != null)
        {
            // Skip White spaces
            if (string.IsNullOrWhiteSpace(currentLine))
            {
                currentLine = reader.ReadLine();
                continue;
            }

            // calculate the current indention
            int level = currentLine.TakeWhile(char.IsWhiteSpace).Count();

            // currently indention is based on 4 spaces
            if (level % 4 != 0)
            {
                throw new Exception("The indentation in this file should be 4 spaces.");
            }

            // Divide it by 4 to get the actual indention level
            level /= 4;

            // If the indention level is smaller it should stop, with the current block
            if (level < indent)
            {
                break;
            }

            // Now we know that the indention is on the same level and we start the splitting
            string[] words = currentLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // Now we read the new line while having the old data. This way if we call a repeat function it still goes to the next line instead of staying on the old one
            currentLine = reader.ReadLine();

            switch (words[0])
            {
                case "Move":
                    // Parse the amount of steps and make a new move commando
                    commands.Add(new Move(int.Parse(words[1])));
                    break;
                case "Turn":
                    // Make the turn all lowercase so left = left and the rest we just read as right
                    TurnDirection turn = words[1].ToLower() == "left" ? TurnDirection.Left : TurnDirection.Right;
                    commands.Add(new Turn(turn));
                    break;
                case "Repeat":
                    // recursion on parseblock(indent + 1) so the children are the list of the repeat.
                    commands.Add(
                        new Repeat(int.Parse(words[1]),
                        ParseBlock(indent + 1))
                    );
                    break;
                default:
                    break;
            }
        }
        return commands;
    }

}