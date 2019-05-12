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
            io_players[0].M_Color = Player_Data.Player.k_Black;

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
                io_players[1].M_Color = Player_Data.Player.k_White;
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
    }

}
