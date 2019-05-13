using Game_Data;
using Game_Logic;
namespace Othelo
{
    public class Othelo
    {
        public static void Run()
        {
            Player_Data.Player[] players = new Player_Data.Player[2];
            int boardSize = UI.Console.RecieveInputFromUser(ref players);
            Board board = new Board(boardSize);
            Board.PrintBoard(ref board);
            TurnManager.OtheloTurnManager(ref board, players[1]);

            //UI.Console.RecievePointFromPlayer();
        }
    }
}
