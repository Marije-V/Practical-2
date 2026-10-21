using Practical2;

namespace TestProject1;

public class ApplicationTest
{
    [Theory]
    [InlineData("e", "e", "Random selection gave")]
    [InlineData("path", "e", "Type 'e'")]
    [InlineData("path", "e", "End state")]
    [InlineData("path", "m", "Metrics:  |Amount:")]
    [InlineData("path", "f", "Congratulations.")]

    public void TestInput(string firstInput, string secondInput, string expectedOutput)
    {
        var originalIn = Console.In;
        var originalOut = Console.Out;

        try
        {
            string input = firstInput == "path" ? Path.Combine(AppContext.BaseDirectory, "Test.txt") : firstInput;
            Console.SetIn(new StringReader($"{input}\n{secondInput}"));
            using var outputCapture = new StringWriter();
            Console.SetOut(outputCapture);

            Application.Main(Array.Empty<string>());

            string output = outputCapture.ToString();
            Assert.Contains(expectedOutput, output);
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }

}