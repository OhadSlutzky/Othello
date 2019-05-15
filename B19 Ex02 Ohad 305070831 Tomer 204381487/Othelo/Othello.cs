using Game_Data;
using Game_Logic;

namespace Othello
{
    public class Othello
    {
        public static void Run()
        {
            int turnIndicator = 0;
            int consecutiveNumberOfTurnsWithoutValidMoves = 0;
            int player1NumberOfDiscs = 0;
            int player2NumberOfDiscs = 0;
            bool anotherGame = true;
            Game_Data.Player[] players = new Game_Data.Player[2];
            int boardSize = UI.Console.RecieveInputFromUser(ref players);

            while (anotherGame == true)
            {
                Board board = new Board(boardSize);

                UI.Console.PrintBoard(board);
                string message = string.Format("Please enter the point in the following form - n,c where n is an iteger between 1 - {0} and c is a character between A - {1}.", board.M_BoardSize, (char)('A' + board.M_BoardSize - 1));
                System.Console.WriteLine(message);

                while (consecutiveNumberOfTurnsWithoutValidMoves != 2)
                {
                    TurnManager.OtheloTurnManager(ref board, players[turnIndicator % 2], ref consecutiveNumberOfTurnsWithoutValidMoves);
                    turnIndicator += 1;
                }

                board.CountNumberOfDiscsForBothPlayers(ref player1NumberOfDiscs, ref player2NumberOfDiscs);
                UI.Console.PrintFinalScore(players[0], players[1], player1NumberOfDiscs, player2NumberOfDiscs);
                anotherGame = UI.Console.AskIfPlayAgain();
                UI.Console.ClearScreen();
            }
        }
    }
}
