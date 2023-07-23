public class Cell
{
    public int X { get; } // X-coordinate of the cell
    public int Y { get; } // Y-coordinate of the cell

    private bool isAlive;
    private bool isAliveNext;
    private List<Cell> neighbors;

    public Cell(int x, int y, bool isAlive)
    {
        X = x;
        Y = y;
        this.isAlive = isAlive;
        isAliveNext = false;
        neighbors = new List<Cell>();
    }

    public void AddNeighbor(Cell neighbor)
    {
        neighbors.Add(neighbor);
    }

      public void PrepareNextState()
    {
        // Calculate the next state of the cell based on its current state and neighbors
        int livingNeighbors = neighbors.Count(neighbor => neighbor.IsAlive);

        if (isAlive)
        {
            // Cell is currently alive
            if (livingNeighbors < 2 || livingNeighbors > 3)
            {
                // Cell dies due to underpopulation or overpopulation
                isAliveNext = false;
            }
            else
            {
                // Cell survives to the next generation
                isAliveNext = true;
            }
        }
        else
        {
            // Cell is currently dead
            if (livingNeighbors == 3)
            {
                // Cell becomes alive due to reproduction
                isAliveNext = true;
            }
            else
            {
                // Cell remains dead in the next generation
                isAliveNext = false;
            }
        }
    }

    public void UpdateState()
    {
        // Update the cell's state (isAlive) with the next state (isAliveNext).
        isAlive = isAliveNext;
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }
}
