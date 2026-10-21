namespace Practical2;

public class FileImporter
{
    private List<IProgramParser> parsers;

    // Create a fileimporter with only a textparser
    public FileImporter()
    {
        parsers = [new TxtParser()];
    }
    // Create a fileimporter with multiple types of parsers
    public FileImporter(List<IProgramParser> parsers)
    {
        this.parsers = parsers;
    }
    public Program Import(string path)
    {
        //check whether the file exists
        if (!File.Exists(path))
        {
            throw new Exception("Filepath does not exist");
        }

        // Loop through all parsers 
        foreach (IProgramParser parser in parsers)
        {
            string extension = path.Split('.').Last();

            // If the parser does not have the same extension as the file continue
            if (parser.CanParse(extension) == false)
            {
                continue;
            }

            // Make a streamreader so you can read the file line by line
            StreamReader fileReader = new StreamReader(path);

            // Get the file name and return the parsed program
            string name = Path.GetFileNameWithoutExtension(path);
            return new Program(name, parser.Parse(fileReader));
        }

        // Throw exception if we cannot parse the file
        throw new Exception("We cannot parse this filetype");
    }

}