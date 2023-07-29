public static class Control
{
    public static void MainControl(Game game)
    {
        //wait for input
        Console.WriteLine("Enter a command: ");
        Console.WriteLine("1. setCell- set a cell to true or false");
        Console.WriteLine("2. next-next iteration");
        Console.WriteLine("3. next50- next 50 iterations");
        Console.WriteLine("4.default- automatically builds a glider");
        Console.WriteLine("5. arow- automatically builds an arow");
        Console.WriteLine("6. change game mode to carriage");
        string input = Console.ReadLine();
        //switch case for input

        switch(input)
        {
            case "setcell" or "1":
                SetCell(game);
                break;
            case "default" or "4":
                game.SetCellState(4, 3, true);
                game.SetCellState(5, 4, true);
                game.SetCellState(5, 5, true);
                game.SetCellState(4, 5, true);
                game.SetCellState(3, 5, true);
                break; 
            case "arow" or "5":
                game.SetCellState(4, 3, true);
                game.SetCellState(3, 4, true);
                game.SetCellState(2, 5, true);
                game.SetCellState(3, 6, true);
                game.SetCellState(4, 7, true);
                game.SetCellState(5, 4, true);
                game.SetCellState(5, 5, true);
                game.SetCellState(5, 6, true);
                game.SetCellState(6, 2, true);
                game.SetCellState(6, 3, true);
                game.SetCellState(6, 7, true);
                game.SetCellState(6, 8, true);
                break;       
            case "next" or "2":
                game.NextIteration();
                break;
             case "next50" or "3":
             for (int i = 0; i < 50; i++)
             {
                Thread.Sleep(150);
                game.NextIteration();
             }
                break;
            case "change game mode to carriage" or "6":
                GameModeToCarriage(game);
                break;
        }

        //call main control again
        MainControl(game);
    
    }

    private static void SetCell(Game game)
    {
        Console.WriteLine("Enter the x coordinate: ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the y coordinate: ");
        int y = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the value (true/false): ");
        bool value;

        if (!bool.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Invalid input. The cell state will be set to false.");
            value = false;
        }

        game.SetCellState(x, y, value);
    }

    //game mode to carriage
    public static void GameModeToCarriage(Game game)
    {
        while (true)
        {
            // Clear the console and render the grid with caret
            game.ChangeMode(GameMode.Carriage);
            Console.WriteLine("Use W, A, S, D keys to move the caret. Press Spacebar to toggle cell state.");

            // Wait for input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // Handle keyboard input
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    Carriage.Instance.Move(0,1,game); // Move caret up
                    break;
                case ConsoleKey.A:
                    Carriage.Instance.Move(-1,0,game); // Move caret left
                    break;
                case ConsoleKey.S:
                    Carriage.Instance.Move(0,-1,game); // Move caret down
                    break;
                case ConsoleKey.D:
                    Carriage.Instance.Move(1,0,game); // Move caret right
                    break;
                case ConsoleKey.Spacebar:
                    // Toggle cell state at the caret position
                    Carriage.Instance.ToggleCellState(game);
                    break;
                case ConsoleKey.Escape:
                    game.ChangeMode(GameMode.Running);
                    return; // Exit the loop and stop the program when the user presses Escape key
                default:
                    break;
            }

            // Add a short delay to make the caret movement smoother
            Thread.Sleep(100);
        }
    }

}