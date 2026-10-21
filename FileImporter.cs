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
            throw new Exception("Filepath does not exist");
        }

        foreach (IProgramParser parser in parsers)
        {
            string extension = path.Split('.').Last();

            if (parser.CanParse(extension) == false)
            {
                continue;
            }

            StreamReader fileReader = new StreamReader(path);

            string name = Path.GetFileNameWithoutExtension(path);
            return new Program(name, parser.Parse(fileReader));
        }

        throw new Exception("We cannot parse this filetype");

    }

}