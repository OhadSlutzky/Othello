using Game_Logic_and_Data;

namespace Othelo
{
    public class Othelo
    {
        public static void Run()
        {
            Player_Data.PlayersData players = new Player_Data.PlayersData();
            UI.Console.RecieveInputFromUser(players);
            Board board = new Game_Logic_and_Data.Board(8);
            Board.PrintBoard(ref board);
        }
    }
}
