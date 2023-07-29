public class Carriage 
{
    //singleton
    private static Carriage instance;
    
    public static Carriage Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Carriage();
            }
            return instance;
        }
    }

    //coordinates
    public int X { get; set; }
    public int Y { get; set; }

    //constructor
    private Carriage()
    {
        X = 0;
        Y = 0;
    }

    //set cell under carriage to opposite state
    public void ToggleCellState(Game game)
    {
        game.SetCellState(X, Y, !game.GetCellState(X, Y));
    }

     public void Move(int deltaX, int deltaY, Game game)
    {
        // Get the grid boundaries
        int rows = game.GetGridSize().Rows;
        int columns = game.GetGridSize().Columns;
        
        // Calculate new coordinates after moving
        int newX = X + deltaX;
        int newY = Y + deltaY;

        // Check if the new coordinates are within the grid boundaries
        if (newX >= 0 && newX < columns && newY >= 0 && newY < rows)
        {
            // Move the carriage to the new valid coordinates
            X = newX;
            Y = newY;
        }
        // If the new coordinates are outside the grid boundaries, don't move the carriage

        //render the game
        game.Render();
    }

}