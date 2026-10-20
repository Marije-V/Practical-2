public class FileImporter
{
    List<IProgramParser> parsers;

    public FileImporter()
    {
        parsers = new List<IProgramParser>();
        parsers.Add(new TxtParser());
    }
    public FileImporter(List<IProgramParser> parsers)
    {
        this.parsers = parsers;
    }


}