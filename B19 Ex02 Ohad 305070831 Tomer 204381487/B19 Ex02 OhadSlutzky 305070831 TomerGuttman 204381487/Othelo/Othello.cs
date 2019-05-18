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

            UI.Console.PrintIntro();
            UI.Console.ClearScreen();

            Game_Data.Player[] players = new Game_Data.Player[2];
            int boardSize = UI.Console.RecieveInputFromUser(players);
            UI.Console.ClearScreen();
            
            while (anotherGame == true)
            {
                Board board = new Board(boardSize);
                consecutiveNumberOfTurnsWithoutValidMoves = 0;
                UI.Console.PrintInputPointFormat(board);
                UI.Console.PrintBoard(board);

                while (consecutiveNumberOfTurnsWithoutValidMoves != 2)
                {
                    TurnManager.OthelloTurnManager(board, players[turnIndicator % 2], ref consecutiveNumberOfTurnsWithoutValidMoves);
                    turnIndicator += 1;
                }

                board.CountNumberOfDiscsForBothPlayers(ref player1NumberOfDiscs, ref player2NumberOfDiscs);
                System.Threading.Thread.Sleep(1500);
                UI.Console.PrintFinalScore(players[0], players[1], player1NumberOfDiscs, player2NumberOfDiscs);
                anotherGame = UI.Console.AskIfPlayAgain();
                System.Threading.Thread.Sleep(2000);
                UI.Console.ClearScreen();
            }

            UI.Console.ExitSequence();
        }
    }
}