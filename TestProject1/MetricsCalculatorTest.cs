
using Practical2;

namespace TestProject1;

public class MetricsCalculatorTest
{
    public List<Program> programs;
    public MetricsCalculatorTest()
    {
        programs = new List<Program>(){
            new Program(
                "TestProgram1",
                new List<ICommand>()
                {
                    new Repeat(5, new List<ICommand>() {
                        new Move(4), new Turn(TurnDirection.Left)}),
                    new Repeat(5, new List<ICommand>() {
                        new Repeat(6, new List<ICommand>(){
                            new Move(5)
                        }), new Turn(TurnDirection.Left)}),

                }
            )
        };
    }
    [Fact]
    public void TestCalculate()
    {
        Metrics metrics = MetricsCalculator.Calculate(programs[0]);
        Metrics expectedMetrics = new Metrics(7, 3, 2);
        Assert.Equal(expectedMetrics.CommandCount, metrics.CommandCount);
        Assert.Equal(expectedMetrics.RepeatCount, metrics.RepeatCount);
        Assert.Equal(expectedMetrics.MaxNestingLevel, metrics.MaxNestingLevel);
    }
}