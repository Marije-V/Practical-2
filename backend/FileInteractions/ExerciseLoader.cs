namespace Practical2;

public class ExerciseLoader
{
    private List<Grid> _exercises;
    public ExerciseLoader()
    {
        _exercises = new List<Grid>
        {
            Exercise1(),
            Exercise2()
        };
    }

    private static Grid Exercise1()
    {
        Cell[,] cells = new Cell[,]{
            {Cell.Empty, Cell.Empty, Cell.Blocked, Cell.Blocked, Cell.Blocked},
            {Cell.Blocked, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Blocked},
            {Cell.Blocked, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Blocked},
            {Cell.Blocked, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty},
            {Cell.Blocked, Cell.Blocked, Cell.Empty, Cell.Empty, Cell.Goal}};

        return new Grid(cells);
    }
    private static Grid Exercise2()
    {
        Cell[,] cells = new Cell[,]{
            {Cell.Empty, Cell.Empty, Cell.Blocked, Cell.Blocked, Cell.Blocked},
            {Cell.Blocked, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Blocked},
            {Cell.Blocked, Cell.Blocked, Cell.Empty, Cell.Empty, Cell.Blocked},
            {Cell.Blocked, Cell.Blocked, Cell.Blocked, Cell.Empty, Cell.Empty},
            {Cell.Blocked, Cell.Blocked, Cell.Blocked, Cell.Empty, Cell.Goal}};

        return new Grid(cells);
    }

    public Grid GetRandomExercise()
    {
        var rand = new Random();
        return _exercises[rand.Next(0, _exercises.Count)];
    }
}