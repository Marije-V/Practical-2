namespace Practical2;

public class Main
{
    private Program program;
    private Program exampleProgram1 = new Program();
    private Program exampleProgram2 = new Program();
    private Program exampleProgram3 = new Program();

    public void main()
    {
        Console.Write("Type '1', '2' or '3' for an example program, or upload your own file.");
        // cannot be char in c#
        string input = Console.ReadLine();
        Console.WriteLine(input);
        if (input == "1")
        {
            program = exampleProgram1;
        }
        if (input == "2")
        {
            program = exampleProgram2;
        }
        if (input == "3")
        {
            program = exampleProgram3;
        }
        else
        {
            try
            {
                program = Parser.Parse(input);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input, program now broken. beep boop morp zeep.");
            }
        }
    }
}