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

            if (parseSucceed == false)
            {
                ///means that the string was "Q"
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
                else
                {
                    io_players[1].M_PlayerName = "PC";
                }

                io_players[1].M_Color = Player_Data.Player.k_Black;
            }

            strBoardSize = RecieveBoardSize();
            parseSucceed = int.TryParse(strBoardSize, out boardSize);

            if (parseSucceed == false)
            {
                ///means that the string was "Q"
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

        public static Game_Data.Board.Point RecievePointFromPlayer(Game_Data.Board i_otheloBoard, Player_Data.Player i_Player)
        {
            Game_Data.Board.Point o_PlayerChosenPoint = null;
            string userInput = null; ///MIGHT CRASH BECAUSE USERINPUT[1] AND USERINPUT[3] DON'T EXIST
            string message = null;
            bool availablePoint = false;

            message = string.Format("Please enter the point in the following form - int,char when int is between 1 - {0} and char between A - {1}", i_otheloBoard.M_BoardSize, (char)('A' + i_otheloBoard.M_BoardSize));
            System.Console.WriteLine(message);
            
            while (availablePoint == false && userInput != "Q")
            {
                System.Console.WriteLine("Please place your disc by choosing a cell\nin such a way that there is at least one straight (horizontal, vertical, or diagonal) occupied line between the new disc and another one of your discs.");
                userInput = System.Console.ReadLine();

                if (IsValidInput(userInput, i_otheloBoard.M_BoardSize))
                {
                    if (Game_Data.Board.IsValidDiscPlacement(i_otheloBoard, int.Parse(userInput[0].ToString()), userInput[2], i_Player))
                    {
                        o_PlayerChosenPoint = new Game_Data.Board.Point(int.Parse(userInput[0].ToString()), userInput[2], i_Player.M_Color);
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
                System.Console.WriteLine("\nGOODBYE!!");
                System.Environment.Exit(0);
            }

            return o_PlayerChosenPoint;
        }
        
        internal static bool IsValidInput(string i_userInput, int i_boardSize)
        {
            return i_userInput.Length == 3 && i_userInput[1] == ',' && i_userInput[0] >= '0' && i_userInput[0] <= (char)(i_boardSize + '0') && i_userInput[2] >= 'A' && i_userInput[2] <= (char)(i_boardSize + 'A');
        }

        public static void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static void PrintBoard(Game_Data.Board i_otheloBoard)
        {
            Game_Data.Board.PrintBoard(i_otheloBoard);
        }

        public static bool AskIfPlayAgain()
        {
            string userInput = null;
            bool anotherGame = false;   
            System.Console.WriteLine("Would you like to have another game?");

            while (userInput != "yes" && userInput != "no" && userInput != "Q")
            {
                System.Console.WriteLine("Please enter 'yes' or 'no'");
                userInput = System.Console.ReadLine();

                if (userInput.Equals("yes"))
                {
                    anotherGame = true;
                }
                else if (userInput.Equals("no") || userInput.Equals("Q"))
                {
                    anotherGame = false;
                }
            }

            return anotherGame;
        }

        public static void PrintFinalScore(Player_Data.Player i_player1, Player_Data.Player i_player2, int i_player1NumberOfDiscs, int i_player2NumberOfDiscs)
        {
            string message = string.Format(
@"  ______ _____ _   _          _             _____  _____ ____  _____  ______ 
 |  ____|_   _| \ | |   /\   | |           / ____|/ ____/ __ \|  __ \|  ____|
 | |__    | | |  \| |  /  \  | |  ______  | (___ | |   | |  | | |__) | |__   
 |  __|   | | | . ` | / /\ \ | | |______|  \___ \| |   | |  | |  _  /|  __|  
 | |     _| |_| |\  |/ ____ \| |____       ____) | |___| |__| | | \ \| |____ 
 |_|    |_____|_| \_/_/    \_\______|     |_____/ \_____\____/|_|  \_\______|



            
          {0}          {1}
            {2}             {3}

",
i_player1.M_PlayerName,
i_player2.M_PlayerName,
i_player1NumberOfDiscs,
i_player2NumberOfDiscs);
            System.Console.WriteLine(message);

            if (i_player1NumberOfDiscs > i_player2NumberOfDiscs)
            {
                string winner = string.Format("{0} IS THE WINNER!!", i_player1.M_PlayerName);
            }
            else if (i_player1NumberOfDiscs < i_player2NumberOfDiscs)
            {
                string winner = string.Format("{0} IS THE WINNER!!", i_player2.M_PlayerName);
            }
            else
            {
                System.Console.WriteLine("IT IS A TIE!!\nUNBELIVABLE!!");
            }
        }
    }
}
