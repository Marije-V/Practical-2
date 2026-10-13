namespace Practical2;

public static class Parser
{
    public static Program Parse(string filepath)
    {
        // if filepath.suffix == .txt then TextParse(fileContents)
        // if filepath.suffix == .json then JsonParse(fileContents)
        // else
        Console.Write("Sorry, we don't accept that filetype. Try .txt or .json.");
        return new Program();
    }

    private static Program TextParse(string contents)
    {
        //parse logic for .txt
        return new Program();
    }

    private static Program JsonParse(string contents)
    {
        //parse logic for .json
        return new Program();
    }
}