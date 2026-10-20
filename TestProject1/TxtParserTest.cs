using System.Text;
using Practical2;

namespace TestProject1;

public class TxtParserTest
{
    [Theory]
    [InlineData("hello.txt", true)]
    [InlineData("hello.cs", false)]
    [InlineData("hello.TXT", false)]
    public void CanParseTestCases(string filepath, bool expected)
    {
        var parser = new TxtParser();

        Assert.Equal(expected, parser.CanParse(filepath));
    }

    [Fact]
    public void ReadsTheInputCorrectly()
    {
        const string input = """

            Move 10
            Turn left
            Turn right
            Repeat 2 times
                Move 3
                Repeat 2 times
                    Turn left
            Move 5
            Unknown command

            """;

        var commands = Parse(input);

        Assert.Equal(5, commands.Count);
        Assert.IsType<Move>(commands[0]);
        Assert.IsType<Turn>(commands[1]);
        Assert.IsType<Turn>(commands[2]);

        var repeat = Assert.IsType<Repeat>(commands[3]);
        var repeatCommands = repeat.GetChildren();
        Assert.Equal(2, repeatCommands.Count);
        Assert.IsType<Move>(repeatCommands[0]);

        var nestedRepeat = Assert.IsType<Repeat>(repeatCommands[1]);
        Assert.IsType<Turn>(Assert.Single(nestedRepeat.GetChildren()));

        Assert.IsType<Move>(commands[4]);
    }

    [Fact]
    public void EmptyInput()
    {
        Assert.Empty(Parse(string.Empty));
    }

    [Fact]
    public void IndentationNotEqualToFour()
    {
        const string input = """
            Move 10
              Move 5
            """;

        var exception = Assert.Throws<Exception>(() => Parse(input));

        Assert.Equal("The indentation in this file should be 4 spaces.", exception.Message);
    }

    private static List<ICommand> Parse(string input)
    {
        using var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(input)));
        return new TxtParser().Parse(reader);
    }
}
