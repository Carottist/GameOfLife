public class Grid
{
    private Cell[,] cells;
    private int rows;
    private int columns;
    private NeighborMode neighborMode;

    //getters for rows and columns
    public int Rows { get { return rows; } }
    public int Columns { get { return columns; } }

    public Grid(int rows, int columns, NeighborMode neighborMode)
    {
        this.rows = rows;
        this.columns = columns;
        this.neighborMode = neighborMode;
        cells = new Cell[rows, columns];
        InitializeCells();
        CalculateNeighbors();
    }


    

    private void InitializeCells()
    {
        // Create and initialize each cell in the grid
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                cells[y, x] = new Cell(x, y, isAlive: false); // Initialize all cells as dead
            }
        }
    }

    public void AddCell(Cell cell)
    {
        // Add a cell to the grid and connect it with its neighbors
        int x = cell.X;
        int y = cell.Y;
    }

    private void CalculateNeighbors()
    {
        // Calculate the neighbors for each cell based on the neighbor mode
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {

                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0)
                            continue; // Skip the cell itself

                        int nx = x + dx;
                        int ny = y + dy;

                        if (neighborMode == NeighborMode.Classic)
                        {
                            if (nx >= 0 && nx < columns && ny >= 0 && ny < rows)
                            {
                                cells[y, x].AddNeighbor(cells[ny, nx]);
                            }
                        }
                        else if (neighborMode == NeighborMode.Wraparound)
                        {
                            nx = (nx + columns) % columns;
                            ny = (ny + rows) % rows;
                            cells[y, x].AddNeighbor(cells[ny, nx]);
                        }
                    }
                }
            }
        }
    
    }

    public void PrepareNextGeneration()
    {
        // Calculate the next state of each cell based on the rules of the Game of Life
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                cells[y, x].PrepareNextState();
            }
        }
    }

    public void UpdateGeneration()
    {
        // Update the state of each cell for the next generation
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                cells[y, x].UpdateState();
            }
        }
    }

    public void Render(GameMode gameMode)
    {
        // Clear the console
        Console.Clear();

        if (gameMode == GameMode.Running || gameMode == GameMode.Paused)
        {
            // Render the grid to the console
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Console.Write(cells[y, x].IsAlive ? "*" : " ");
                }
                Console.WriteLine();
            }
        }

        //if game mode is carriage, render the carriage as well
        if (gameMode == GameMode.Carriage)
        {
            // Render the grid to the console
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (Carriage.Instance.X == x && Carriage.Instance.Y == y)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(cells[y, x].IsAlive ? "o" : " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
    
    public void SetCellState(int x, int y, bool isAlive)
    {
        // Set the state (alive or dead) of a cell at specific coordinates (x, y)
        // Note: Ensure that the coordinates are within the grid boundaries
        if (x >= 0 && x < columns && y >= 0 && y < rows)
        {
            cells[y, x].IsAlive = isAlive;
        }
    }

    public bool GetCellState(int x, int y)
    {
        // Get the state (alive or dead) of a cell at specific coordinates (x, y)
        // Note: Ensure that the coordinates are within the grid boundaries
        if (x >= 0 && x < columns && y >= 0 && y < rows)
        {
            return cells[y, x].IsAlive;
        }
        return false;
    }

   
}
 public enum NeighborMode
    {
        Classic,
        Wraparound
    }
