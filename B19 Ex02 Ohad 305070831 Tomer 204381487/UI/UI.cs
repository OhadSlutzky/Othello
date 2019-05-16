using System.Collections.Generic;

namespace UI
{
    public class Console
    {
        public static int RecieveInputFromUser(Game_Data.Player[] io_players)
        {
            int boardSize = 0;
            int numOfPlayers = 0;
            string strNumOfPlayers = null;
            string strBoardSize = null;
            string userInput = null;

            io_players[0] = new Game_Data.Player();
            io_players[1] = new Game_Data.Player();

            System.Console.WriteLine("AT ANY POINT IN THE GAME, ENTER 'Q' TO EXIT\n");
            System.Console.WriteLine("Please enter your name: ");
            userInput = System.Console.ReadLine();

            if (userInput == "Q")
            {
                ExitSequence();
            }

            io_players[0].M_PlayerName = userInput;
            io_players[0].M_Color = Game_Data.Player.k_Black;

            strNumOfPlayers = RecieveNumOfPlayers();

            bool parseSucceed = int.TryParse(strNumOfPlayers, out numOfPlayers);

            if (parseSucceed == false)
            {
                ///means that the string was "Q"
                ExitSequence();
            }
            else
            {
                if (numOfPlayers == 2)
                {
                    System.Console.WriteLine("Please enter the second player's name: ");
                    userInput = System.Console.ReadLine();

                    if (userInput == "Q")
                    {
                        ExitSequence();
                    }

                    io_players[1].M_PlayerName = userInput;
                }
                else
                {
                    io_players[1].M_PlayerName = "PC";
                }

                io_players[1].M_Color = Game_Data.Player.k_White;
            }

            strBoardSize = RecieveBoardSize();
            parseSucceed = int.TryParse(strBoardSize, out boardSize);

            if (parseSucceed == false)
            {
                ///means that the string was "Q"
                ExitSequence();
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

        public static Game_Data.Board.Point RecievePointFromPlayer(Game_Data.Board i_otheloBoard, Game_Data.Player i_Player, List<Game_Data.Board.Point> i_validPointsToChooseFrom)
        {
            Game_Data.Board.Point o_PlayerChosenPoint = null;
            string userInput = null;
            bool availablePoint = false;

            string message = string.Format("It is {0}'s turn ({1})!", i_Player.M_PlayerName, i_Player.M_Color);
            System.Console.WriteLine(message);

            if (i_Player.M_PlayerName != "PC")
            {
                while (availablePoint == false && userInput != "Q")
                {
                    System.Console.WriteLine("Please choose a point to place your disc in from the following options: ");
                    UI.Console.PrintValidPointsForPlayer(i_validPointsToChooseFrom);
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
                            System.Console.WriteLine("--------------------------------------------------\nYou can't place a disc there!");
                            System.Console.WriteLine("Please place your disc by choosing a cell in such a way that there is at least one straight\n(horizontal, vertical, or diagonal) occupied line between the new disc and another one of your discs.");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid Input!");
                    }
                }

                if (userInput == "Q")
                {
                    ExitSequence();
                }
            }
            else   ///It's PC's turn
            {
                System.Random rand = new System.Random();
                o_PlayerChosenPoint = i_validPointsToChooseFrom[rand.Next(i_validPointsToChooseFrom.Count)];
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

        public static void PrintFinalScore(Game_Data.Player i_player1, Game_Data.Player i_player2, int i_player1NumberOfDiscs, int i_player2NumberOfDiscs)
        {
            string winnerMessage = null;
            string finalScoreMessage = string.Format(
@"  ______ _____ _   _          _             _____  _____ ____  _____  ______ 
 |  ____|_   _| \ | |   /\   | |           / ____|/ ____/ __ \|  __ \|  ____|
 | |__    | | |  \| |  /  \  | |  ______  | (___ | |   | |  | | |__) | |__   
 |  __|   | | | . ` | / /\ \ | | |______|  \___ \| |   | |  | |  _  /|  __|  
 | |     _| |_| |\  |/ ____ \| |____       ____) | |___| |__| | | \ \| |____ 
 |_|    |_____|_| \_/_/    \_\______|     |_____/ \_____\____/|_|  \_\______|



{0} ({1}) : {2}  Vs  {3} ({4}) : {5}

",
i_player1.M_PlayerName,
i_player1.M_Color,
i_player1NumberOfDiscs,
i_player2.M_PlayerName,
i_player2.M_Color,
i_player2NumberOfDiscs);

            System.Console.WriteLine(finalScoreMessage);

            if (i_player1NumberOfDiscs > i_player2NumberOfDiscs)
            {
                winnerMessage = string.Format("{0} IS THE WINNER!!", i_player1.M_PlayerName);
            }
            else if (i_player1NumberOfDiscs < i_player2NumberOfDiscs)
            {
                winnerMessage = string.Format("{0} IS THE WINNER!!", i_player2.M_PlayerName);
            }
            else
            {
                winnerMessage = "IT IS A TIE!!\nUNBELIVABLE!!";
            }

            System.Console.WriteLine(winnerMessage);
        }

        public static void PrintValidPointsForPlayer(List<Game_Data.Board.Point> validPointsToChooseFrom)
        {
            System.Text.StringBuilder o_stringValidPoints = new System.Text.StringBuilder(7);
            string message = null;

            foreach(Game_Data.Board.Point currentPoint in validPointsToChooseFrom)
            {
                message = string.Format("{0},{1}   ", currentPoint.M_Longtitude, currentPoint.M_Latitude);
                o_stringValidPoints.Append(message);
            }

            System.Console.WriteLine(o_stringValidPoints);
        }

        public static void ExitSequence()
        {
            System.Console.WriteLine("Thank you for playing our game!!! Goodbye!");
            System.Threading.Thread.Sleep(2000);
            System.Environment.Exit(0);
        }

        public static void PrintIntro()
        {
            string intro = string.Format(
@"


██████╗ ████████╗██╗  ██╗███████╗██╗     ██╗      ██████╗ 
██╔═══██╗╚══██╔══╝██║  ██║██╔════╝██║     ██║     ██╔═══██╗
██║   ██║   ██║   ███████║█████╗  ██║     ██║     ██║   ██║
██║   ██║   ██║   ██╔══██║██╔══╝  ██║     ██║     ██║   ██║
╚██████╔╝   ██║   ██║  ██║███████╗███████╗███████╗╚██████╔╝
 ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝ ╚═════╝ ");

            System.Console.WriteLine(intro);
            System.Threading.Thread.Sleep(2000);
        }

        public static void PrintInputPointFormat(Game_Data.Board i_othelloBoard)
        {
            string inputPointFormat = string.Format("\nPlease enter the point in the following form - n,c where n is an iteger between 1 - {0} and c is a character between A - {1}\n", i_othelloBoard.M_BoardSize, (char)('A' + i_othelloBoard.M_BoardSize - 1));
            System.Console.WriteLine(inputPointFormat);
        }
    }
}
