using Game_Data;
using Game_Logic;
namespace Othelo
{
    public class Othelo
    {
        public static void Run()
        {
            int turnIndicator = 0;
            int consecutiveNumberOfTurnsWithoutValidMoves = 0;
            int player1NumberOfDiscs = 0;
            int player2NumberOfDiscs = 0;
            bool anotherGame = true;
            Player_Data.Player[] players = new Player_Data.Player[2];
            int boardSize = UI.Console.RecieveInputFromUser(ref players);

            while (anotherGame == true)
            {
                Board board = new Board(boardSize);

                UI.Console.PrintBoard(board);

                while (consecutiveNumberOfTurnsWithoutValidMoves != 2)
                {
                    TurnManager.OtheloTurnManager(ref board, players[turnIndicator % 2], ref consecutiveNumberOfTurnsWithoutValidMoves);
                    turnIndicator += 1;
                }

                board.CountNumberOfDiscsForBothPlayers(ref player1NumberOfDiscs, ref player2NumberOfDiscs);
                UI.Console.PrintFinalScore(players[0], players[1], player1NumberOfDiscs, player2NumberOfDiscs);
                anotherGame = UI.Console.AskIfPlayAgain();
            }
        }
    }
}
