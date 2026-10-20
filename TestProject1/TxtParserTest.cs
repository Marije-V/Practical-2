using Practical2;

namespace TestProject1;

public class TxtParserTest
{
    public TxtParserTest()
    {

    }
    [Fact]
    public void Test1()
    {
        TxtParser parse = new TxtParser();

        bool succesful = parse.CanParse("hello.txt");

        Assert.True(succesful);
    }

    [Fact]
    public void Test2()
    {
        TxtParser parse = new TxtParser();

        string path = Path.GetTempFileName();
        try
        {
            File.WriteAllLines(path, new[]
            {
                "Repeat 4 times",
                "    Move 10",
                "    Turn right"
            });

            List<ICommand> actual;
            using (var reader = File.OpenText(path))
            {
                actual = parse.Parse(reader);
            }

            var repeat = Assert.IsType<Repeat>(Assert.Single(actual));
            var commands = repeat.GetChildren();
            Assert.Equal(2, commands.Count);
            Assert.IsType<Move>(commands[0]);
            Assert.IsType<Turn>(commands[1]);
        }
        finally
        {
            File.Delete(path);
        }
    }
}