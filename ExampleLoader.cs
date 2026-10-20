public class ExampleLoader
{
    List<Program> programs;
    public ExampleLoader()
    {
        // TODO fill these with example programs (hardcoded)
        programs = new List<Program> {
            new Program(),
            new Program(),
            new Program()};
    }

    public Program GetRandomExample()
    {
        // TODO implement random logic
        return programs[0];
    }
}