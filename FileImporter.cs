namespace Practical2;

public class FileImporter
{
    private List<IProgramParser> parsers;

    public FileImporter()
    {
        parsers = [new TxtParser()];
    }
    public FileImporter(List<IProgramParser> parsers)
    {
        this.parsers = parsers;
    }
    public Program Import(string path)
    {
        //check whether the file exists
        if (!File.Exists(path))
        {
            Console.Error.WriteLine("Filepath does not exist");
        }

        foreach (IProgramParser parser in parsers)
        {
            string extension = path.Split('.').Last();

            if (parser.CanParse(extension) == false)
            {
                continue;
            }

            StreamReader fileReader = new StreamReader(path);

            string nameWithSuffix = path.Split('/').Last();
            return new Program(nameWithSuffix.Split('.')[0], parser.Parse(fileReader));
        }

        throw new Exception("We cannot parse this filetype");

    }

}