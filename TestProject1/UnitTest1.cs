using Practical2;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        TxtParser parse = new TxtParser();

        bool succesful = parse.CanParse("hello.txt");

        Assert.True(succesful);
    }
}
