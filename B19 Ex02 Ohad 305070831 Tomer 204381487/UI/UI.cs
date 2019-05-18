using System.Collections.Generic;

namespace UI
{
    public class Console
    {
        public static int RecieveInputFromUser(Game_Data.Player[] io_Players)
        {
            int boardSize = 0;
            int numOfPlayers = 0;
            string strNumOfPlayers = null;
            string strBoardSize = null;
            string userInput = null;

            io_Players[0] = new Game_Data.Player();
            io_Players[1] = new Game_Data.Player();

            System.Console.WriteLine("Welcome to Othello!\nAT ANY POINT IN THE GAME, ENTER 'Q' TO EXIT\n");
            System.Console.WriteLine("Please enter your name: ");
            userInput = System.Console.ReadLine();

            if (userInput == "Q")
            {
                ExitSequence();
            }

            io_Players[0].M_PlayerName = userInput;
            io_Players[0].M_Color = Game_Data.Player.k_Black;

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

                    io_Players[1].M_PlayerName = userInput;
                }
                else
                {
                    io_Players[1].M_PlayerName = "PC";
                }

                io_Players[1].M_Color = Game_Data.Player.k_White;
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

        public static Game_Data.Board.Point RecievePointFromPlayer(Game_Data.Board i_OthelloBoard, Game_Data.Player i_Player, List<Game_Data.Board.Point> i_ValidPointsToChooseFrom)
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
                    UI.Console.PrintValidPointsForPlayer(i_ValidPointsToChooseFrom);
                    userInput = System.Console.ReadLine();

                    if (IsValidInput(userInput, i_OthelloBoard.M_BoardSize))
                    {
                        if (Game_Data.Board.IsValidDiscPlacement(i_OthelloBoard, int.Parse(userInput[0].ToString()), userInput[2], i_Player))
                        {
                            o_PlayerChosenPoint = new Game_Data.Board.Point(int.Parse(userInput[0].ToString()), userInput[2], i_Player.M_Color);
                            availablePoint = true;
                        }
                        else
                        {
                            System.Console.WriteLine("--------------------------------------------------\nYou can't place a disc there!");
                            System.Console.WriteLine("Please place your disc on the board by choosing a cell, in such a way that there is at least one straight (horizontal, vertical, or diagonal) occupied line between the new disc and another one of your discs, with one or more contiguous rival pieces between them.");
                        }
                    }
                    else if(userInput != "Q")
                    {
                        System.Console.WriteLine("Invalid Input!");
                    }
                }

                if (userInput == "Q")
                {
                    ExitSequence();
                }
            }
            else 
            {
                /// It's PC's turn
                System.Random rand = new System.Random();
                o_PlayerChosenPoint = i_ValidPointsToChooseFrom[rand.Next(i_ValidPointsToChooseFrom.Count)];
            }

            return o_PlayerChosenPoint;
        }
        
        internal static bool IsValidInput(string i_UserInput, int i_BoardSize)
        {
            return i_UserInput.Length == 3 && i_UserInput[1] == ',' && i_UserInput[0] >= '0' && i_UserInput[0] <= (char)(i_BoardSize + '0') && i_UserInput[2] >= 'A' && i_UserInput[2] <= (char)(i_BoardSize + 'A');
        }

        public static void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static void PrintBoard(Game_Data.Board i_OthelloBoard)
        {
            Game_Data.Board.PrintBoard(i_OthelloBoard);
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

        public static void PrintFinalScore(Game_Data.Player i_Player1, Game_Data.Player i_Player2, int i_Player1NumberOfDiscs, int i_Player2NumberOfDiscs)
        {
            string winnerMessage = null;
            string finalScoreMessage = string.Format(
@"  ______ _____ _   _          _             _____  _____ ____  _____  ______ 
 |  ____|_   _| \ | |   /\   | |           / ____|/ ____/ __ \|  __ \|  ____|
 | |__    | | |  \| |  /  \  | |  ______  | (___ | |   | |  | | |__) | |__   
 |  __|   | | | . ` | / /\ \ | | |______|  \___ \| |   | |  | |  _  /|  __|  
 | |     _| |_| |\  |/ ____ \| |____       ____) | |___| |__| | | \ \| |____ 
 |_|    |_____|_| \_/_/    \_\______|     |_____/ \_____\____/|_|  \_\______|



{0} ('{1}') : {2}  Vs  {3} ('{4}') : {5}

",
i_Player1.M_PlayerName,
i_Player1.M_Color,
i_Player1NumberOfDiscs,
i_Player2.M_PlayerName,
i_Player2.M_Color,
i_Player2NumberOfDiscs);

            System.Console.WriteLine(finalScoreMessage);

            if (i_Player1NumberOfDiscs > i_Player2NumberOfDiscs)
            {
                winnerMessage = string.Format("{0} IS THE WINNER!!", i_Player1.M_PlayerName);
            }
            else if (i_Player1NumberOfDiscs < i_Player2NumberOfDiscs)
            {
                winnerMessage = string.Format("{0} IS THE WINNER!!", i_Player2.M_PlayerName);
            }
            else
            {
                winnerMessage = "IT IS A TIE!!\nUNBELIVABLE!!";
            }

            System.Console.WriteLine(winnerMessage);
        }

        public static void PrintValidPointsForPlayer(List<Game_Data.Board.Point> i_ValidPointsToChooseFrom)
        {
            System.Text.StringBuilder o_stringValidPoints = new System.Text.StringBuilder(7);
            string message = null;

            foreach(Game_Data.Board.Point currentPoint in i_ValidPointsToChooseFrom)
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

        public static void PrintInputPointFormat(Game_Data.Board i_OthelloBoard)
        {
            string inputPointFormat = string.Format("\nPlease enter the point in the following form - n,c where n is an iteger between 1 - {0} and c is a character between A - {1}\n", i_OthelloBoard.M_BoardSize, (char)('A' + i_OthelloBoard.M_BoardSize - 1));
            System.Console.WriteLine(inputPointFormat);
            System.Console.WriteLine("For your knowledge:\nplace your disc on the board by choosing a cell, in such a way that there is at least one straight (horizontal, vertical, or diagonal)\noccupied line between the new disc and another one of your discs, with one or more contiguous rival pieces between them.\n");
        }
    }
}
