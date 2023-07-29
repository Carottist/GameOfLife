using System.Threading;

public class Game
{
    private Grid grid;
    private GameMode mode;
    private int generationsToSimulate;

    //get grid size
    public (int Rows, int Columns) GetGridSize()
{
    // Assuming the Grid class has Rows and Columns properties
    int rows = grid.Rows;
    int columns = grid.Columns;

    // Return the size as a tuple
    return (rows, columns);
}


    public Game(int rows, int columns, NeighborMode neighborMode, int generationsToSimulate)
    {
        grid = new Grid(rows, columns, neighborMode);
        mode = GameMode.Paused;
        this.generationsToSimulate = generationsToSimulate;
    }

    public void Start()
    {
        if (GameMode.Running == mode)
        {
            Console.WriteLine("The game is already running!");
            return;
        }

        mode = GameMode.Running;

        for (int generation = 1; generation <= generationsToSimulate; generation++)
        {
            Console.WriteLine($"Generation: {generation}");
            grid.Render(gameMode: mode);
            grid.PrepareNextGeneration();
            grid.UpdateGeneration();

            // Adjust the speed of the simulation if needed
            Thread.Sleep(1000);

            // To pause the simulation at any generation, you can add a condition and handle user input.
            Console.ReadKey();
        }

        mode = GameMode.Carriage;
    }

    public void SetCellState(int x, int y, bool isAlive)
    {
        // Set the state of the cell at the specified coordinates (x, y)
        // Note: Ensure that the coordinates are within the grid boundaries
        grid.SetCellState(x, y, isAlive);
        grid.Render(gameMode: mode);
    }

    //get the state of the cell at the specified coordinates (x, y)
    public bool GetCellState(int x, int y)
    {
        return grid.GetCellState(x, y);
    }

    public void NextIteration()
    {
        /* // Run the simulation for the next iteration (generation)
        if (GameMode.Paused == mode || GameMode.Carriage == mode)
        {
            Console.WriteLine("The game is already running. Wait for the current simulation to complete.");
            //log current mode
            Console.WriteLine($"Current mode: {mode}");
            return;
        } */

        grid.PrepareNextGeneration();
        grid.UpdateGeneration();
        grid.Render(gameMode: mode);
    }

    //change game mode
    public void ChangeMode(GameMode mode)
    {
        this.mode = mode;
        grid.Render(gameMode: mode);
        //log current mode
        Console.WriteLine($"Current mode: {mode}");
    }

    //render the grid
    public void Render()
    {
        grid.Render(gameMode: mode);
    }
}

public enum GameMode
{
    Running,
    Paused,
    Carriage
}
