using Microsoft.VisualBasic.FileIO;

namespace Practical2;

public class FileImporter
{
    private readonly List<IProgramParser> _parsers;

    // Create a fileimporter with only a textparser
    public FileImporter()
    {
        _parsers = [new TxtParser()];
    }
    // Create a fileimporter with multiple types of parsers
    public FileImporter(List<IProgramParser> parsers)
    {
        _parsers = parsers;
    }
    public Program Import(string path)
    {
        //check whether the file exists
        if (!File.Exists(path))
        {
            throw new FormatException("Filepath does not exist");
        }

        // Loop through all parsers 
        foreach (IProgramParser parser in _parsers)
        {
            string[] splittedFilepath = path.Split('.');
            string extension = splittedFilepath[splittedFilepath.Count() - 1];

            // If the parser does not have the same extension as the file continue
            if (!parser.CanParse(extension))
            {
                continue;
            }

            // Make a streamreader so you can read the file line by line
            StreamReader fileReader = new StreamReader(path);

            // Get the file name and return the parsed program
            string name = Path.GetFileNameWithoutExtension(path);
            return new Program(name, parser.ParseWithStreamReader(fileReader));
        }

        // Throw exception if we cannot parse the file
        throw new FormatException("We cannot parse this filetype");
    }

    public static Program TryParseText(string name, string filecontent)
    {
        TxtParser textParser = new TxtParser();
        if (filecontent == null) filecontent = "";
        return new Program(name, textParser.ParseFromText(filecontent.Split("\n")));
    }

}