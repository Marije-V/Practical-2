
using System.IO.Enumeration;
using Practical2;

namespace TestProject1;

public class NonExistingFileParser : IProgramParser
{
    public bool CanParse(string extesnion)
    {
        return false;
    }
    public List<ICommand> Parse(StreamReader content)
    {
        return new List<ICommand>();
    }
}
public class FileImporterTest
{
    private string path;
    public FileImporterTest()
    {
        path = Path.Combine(AppContext.BaseDirectory, "Test.txt");
    }
    [Fact]
    public void ParserExistsAndCorrectFile()
    {
        FileImporter importer = new FileImporter();
        var result = importer.Import(path);
        string expectedName = "Test";
        Assert.IsType<Program>(result);
        Assert.Equal(expectedName, result.name);
    }
    [Fact]
    public void TestFileNotExists()
    {
        string filepath = "nonexistingfile.txt";
        FileImporter importer = new FileImporter();
        var exception = Assert.Throws<Exception>(() => importer.Import(filepath));
        Assert.Equal("Filepath does not exist", exception.Message);
    }
    [Fact]
    public void TestProgramWithoutRightParser()
    {
        FileImporter importer = new FileImporter(new List<IProgramParser>()
        {
            new NonExistingFileParser()
        });
        var exception = Assert.Throws<Exception>(() => importer.Import(path));

        Assert.Equal("We cannot parse this filetype", exception.Message);
    }
}