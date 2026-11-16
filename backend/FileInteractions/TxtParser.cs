using System.Collections;

namespace Practical2;

public class TxtParser : IProgramParser
{
    // for reading from just text:
    private string[] _textInput;
    private string _currentTextLine;
    private int _currentLineIndex;

    public TxtParser()
    {

    }

    // checks if the parser can parse the file in this filepath
    public bool CanParse(string filepath)
    {
        string[] splittedFilepath = filepath.Split('.');
        string extension = splittedFilepath[splittedFilepath.Count() - 1];
        return extension == "txt";
    }

    public List<ICommand> ParseFromText(string[] content)
    {
        _textInput = content;
        _currentLineIndex = 0;
        _currentTextLine = _textInput.Length == 0 ? null : _textInput[_currentLineIndex];
        return ParseTextBlock(0);
    }
    public List<ICommand> ParseWithStreamReader(StreamReader content)
    {
        string text = content.ReadToEnd();

        return ParseFromText(text.Replace("\r", "").Split('\n'));
    }

    // parses a block from the parser. So like all commands within a repeat. if repeat in repeat it uses recursion
    private List<ICommand> ParseTextBlock(int indent)
    {
        List<ICommand> commands = new List<ICommand>();

        // Keep going until there is no new line in the textfile
        while (_currentTextLine != null)
        {
            // Skip White spaces
            if (string.IsNullOrWhiteSpace(_currentTextLine))
            {
                AdvanceLine();
                continue;
            }

            int level = GetIndentationLevel(_currentTextLine);

            // If the indention level is smaller it should stop, with the current block
            if (level < indent)
            {
                break;
            }

            // Now we know that the indention is on the same level and we start the splitting
#nullable enable
            string[] words = _currentTextLine.TrimStart().Split((Char[]?)null, StringSplitOptions.RemoveEmptyEntries);

            // Now we read the new line while having the old data. This way if we call a repeat function it still goes to the next line instead of staying on the old one
            AdvanceLine();

            switch (words[0].ToLowerInvariant())
            {
                case "move":
                    // Parse the amount of steps and make a new move commando
                    commands.Add(new Move(int.Parse(words[1])));
                    break;
                case "turn":
                    // Make the turn all lowercase so left = left and the rest we just read as right
                    TurnDirection turn = words[1].ToLowerInvariant() switch
                    {
                        "left" => TurnDirection.Left,
                        "right" => TurnDirection.Right,
                        _ => throw new FormatException("Expected Turn left or Turn right.")
                    };
                    commands.Add(new Turn(turn));
                    break;
                case "repeatuntil":
                    commands.Add(new RepeatUntil(Enum.Parse<Condition>(words[1]), ParseTextBlock(indent + 1)));
                    break;
                case "repeat":
                    // recursion on parseblock(indent + 1) so the children are the list of the repeat.
                    commands.Add(
                        new Repeat(int.Parse(words[1]),
                        ParseTextBlock(indent + 1))
                    );
                    break;
                default:
                    break;
            }
        }
        return commands;
    }
    private void AdvanceLine()
    {
        _currentLineIndex++;

        _currentTextLine = _currentLineIndex < _textInput.Length
            ? _textInput[_currentLineIndex]
            : null;
    }
    private static int GetIndentationLevel(string line)
    {
        const int spacesPerLevel = 4;

        // calculate the current indention
        int spaces = line.TakeWhile(char.IsWhiteSpace).Sum(character => character == '\t' ? spacesPerLevel : 1);

        // currently indention is based on 4 spaces
        if (spaces % spacesPerLevel != 0)
        {
            throw new FormatException("The indentation in this file should be 4 spaces.");
        }

        // Divide it by spacesPerLevel to get the actual indention level
        return spaces / spacesPerLevel;
    }
}
