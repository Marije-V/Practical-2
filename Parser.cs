namespace Practical2;

public static class Parser
{
    static Program Parse(string filepath)
    {
        // if filepath.suffix == .txt then TextParse(fileContents)
        // if filepath.suffix == .json then JsonParse(fileContents)
        // else
        Console.Write("Sorry, we don't accept that filetype. Try .txt or .json.");
    }

    static Program TextParse(string contents)
    {
        //parse logic for .txt
        return new Program();
    }

    static Program JsonParse(string contents)
    {
        //parse logic for .json
        return new Program();
    }
}