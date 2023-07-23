public class Game
{
    private Grid grid;
    private bool isRunning;
    private int generationsToSimulate;

    public Game(int rows, int columns, NeighborMode neighborMode, int generationsToSimulate)
    {
        grid = new Grid(rows, columns, neighborMode);
        isRunning = false;
        this.generationsToSimulate = generationsToSimulate;
    }

    public void Start()
    {
        if (isRunning)
        {
            Console.WriteLine("The game is already running!");
            return;
        }

        isRunning = true;

        for (int generation = 1; generation <= generationsToSimulate; generation++)
        {
            Console.Clear();
            Console.WriteLine($"Generation: {generation}");
            grid.Render();
            grid.PrepareNextGeneration();
            grid.UpdateGeneration();

            // Adjust the speed of the simulation if needed
            // For example, you can use Thread.Sleep to introduce a delay between generations.

            // To pause the simulation at any generation, you can add a condition and handle user input.
            // For example, you can use Console.ReadKey to wait for a key press before proceeding to the next generation.
        }

        isRunning = false;
    }

    public void SetCellState(int x, int y, bool isAlive)
    {
        // Set the state of the cell at the specified coordinates (x, y)
        // Note: Ensure that the coordinates are within the grid boundaries
        grid.SetCellState(x, y, isAlive);

        Console.Clear();
        grid.Render();
    }

    public void NextIteration()
    {
        // Run the simulation for the next iteration (generation)
        if (isRunning)
        {
            Console.WriteLine("The game is already running. Wait for the current simulation to complete.");
            return;
        }

        grid.PrepareNextGeneration();
        grid.UpdateGeneration();

        Console.Clear();
        grid.Render();
    }
}
