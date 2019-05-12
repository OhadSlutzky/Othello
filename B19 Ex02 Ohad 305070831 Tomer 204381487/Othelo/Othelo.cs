using Game_Logic_and_Data;

namespace Othelo
{
    public class Othelo
    {
        public static void Run()
        {
            Player_Data.Player[] players = new Player_Data.Player[2];
            int boardSize = UI.Console.RecieveInputFromUser(ref players);
            Board board = new Game_Logic_and_Data.Board(boardSize);
            Board.PrintBoard(ref board);
            UI.Console.RecievePointFromPlayer()
        }
    }
}
