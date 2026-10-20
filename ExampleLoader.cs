namespace Practical2;

public class ExampleLoader
{
    List<Program> programs;
    public ExampleLoader()
    {
        // TODO fill these with example programs (hardcoded)
        programs = new List<Program> {
            new Program(new List<ICommand> ()),
            new Program(new List<ICommand> ()),
            new Program(new List<ICommand> ())};
    }

    public Program GetRandomExample()
    {
        // TODO implement random logic
        return programs[0];
    }
}