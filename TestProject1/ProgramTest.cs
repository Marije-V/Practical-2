
using Practical2;

namespace TestProject1;

public class ProgramTest
{
    [Fact]
    public void ToStringWithChildren()
    {
        Program program = new Program("Test", new List<ICommand>()
                {
                    new Repeat(1, new List<ICommand>() {
                        new Move(2), new Turn(TurnDirection.Left)})

                });

        string expected = "Repeat 1\n\tMove 2\n\tTurn Left\n";
        Assert.Equal(expected, program.ToString());
    }
    [Fact]
    public void ToStringWithNestedChildren()
    {
        Program program = new Program("Test1", new List<ICommand>()
        {
            new Repeat(1, new List<ICommand>()
            {
                new Repeat(1, new List<ICommand>()
                {
                    new Move(2)
                })
            })
        });
        string expected = "Repeat 1\n\tRepeat 1\n\t\tMove 2\n";
        Assert.Equal(expected, program.ToString());
    }
}