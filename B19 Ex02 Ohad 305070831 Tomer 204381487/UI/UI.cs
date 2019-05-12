namespace UI
{
    public class Console
    {
        public static int RecieveInputFromUser(ref Player_Data.Player[] io_players)
        {
            int boardSize = 0;
            int numOfPlayers = 0;
            string strNumOfPlayers = null;
            string strBoardSize = null;

            io_players[0] = new Player_Data.Player();
            io_players[1] = new Player_Data.Player();

            System.Console.WriteLine("AT ANY POINT IN THE GAME, ENTER 'Q' TO EXIT\n");
            System.Console.WriteLine("Please enter your name: ");

            io_players[0].M_PlayerName = System.Console.ReadLine();
            io_players[0].M_Color = Player_Data.Player.k_White;

            strNumOfPlayers = RecieveNumOfPlayers();

            bool parseSucceed = int.TryParse(strNumOfPlayers, out numOfPlayers);

            if (parseSucceed == false)//means that the string was "Q"
            {
                System.Console.WriteLine("GOODBYE!!!");
                System.Environment.Exit(0);
            }

            else
            {
                if (numOfPlayers == 2)
                {
                    System.Console.WriteLine("Please enter the second player's name: ");
                    io_players[1].M_PlayerName = System.Console.ReadLine();
                }
                io_players[1].M_PlayerName = "PC";
                io_players[1].M_Color = Player_Data.Player.k_Black;
            }

            strBoardSize = RecieveBoardSize();
            parseSucceed = int.TryParse(strBoardSize, out boardSize);

            if (parseSucceed == false)//means that the string was "Q"
            {
                System.Console.WriteLine("GOODBYE!!!");
                System.Environment.Exit(0);
            }

            return boardSize;
        }

        internal static string RecieveNumOfPlayers()
        {
            System.Console.WriteLine("How many players are playing?");

            string userInput = null;

            while (userInput != "1" && userInput != "2" && userInput != "Q")
            {
                System.Console.WriteLine("Please enter '1' or '2': ");
                userInput = System.Console.ReadLine();
            }

            return userInput;
        }

        internal static string RecieveBoardSize()
        {
            string userInput = null;

            System.Console.WriteLine("What size of board would you like? (6x6 or 8x8)");

            while (userInput != "6" && userInput != "8" && userInput != "Q")
            {
                System.Console.WriteLine("Please enter '6' or '8': ");
                userInput = System.Console.ReadLine();
            }

            return userInput;
        }

        public static Game_Logic_and_Data.Board.Point RecievePointFromPlayer(Game_Logic_and_Data.Board i_otheloBoard, Player_Data.Player i_Player)
        {
            Game_Logic_and_Data.Board.Point o_PlayerChosenPoint = null;
            string userInput = null; //MIGHT CRASH BECAUSE USERINPUT[1] AND USERINPUT[3] DON'T EXIST
            string message = null;
            bool availablePoint = false;

            message = string.Format("Please enter the point in the following form - (int,char) when int is between 1 - {0} and char between A - {1}", i_otheloBoard.M_BoardSize, (char)('A' + i_otheloBoard.M_BoardSize));
            System.Console.WriteLine(message);
            
            while (availablePoint == false && userInput != "Q")
            {
                System.Console.WriteLine("Please place your disc by choosing a cell\nin such a way that there is at least one straight (horizontal, vertical, or diagonal) occupied line between the new disc and another one of your discs.");
                userInput = System.Console.ReadLine();

                while (IsValidInput(userInput, i_otheloBoard.M_BoardSize) == true && availablePoint == false)
                {
                    if (i_otheloBoard.M_OtheloBoard[userInput[1], userInput[3]].M_IsAvailableCell == true)
                    {
                        o_PlayerChosenPoint = new Game_Logic_and_Data.Board.Point(userInput[1], userInput[3], i_Player.M_Color);
                        availablePoint = true;
                    }
                    else
                    {
                        System.Console.WriteLine("You can't place a disc there!");
                    }
                }
            }

            if (userInput == "Q")
            {
                System.Environment.Exit(0);
            }

            return o_PlayerChosenPoint;
        }
        
        internal static bool IsValidInput(string i_userInput, int i_boardSize)
        {
            return (i_userInput.Length == 5 && i_userInput[0] == '(' && i_userInput[4] == ')' && i_userInput[2] == ',' && i_userInput[1] >= '0' && i_userInput[1] <= (char)(i_boardSize + '0') && i_userInput[3] >= 'A' && i_userInput[3] <= (char)(i_boardSize + 'A'));
        }      
    }
}