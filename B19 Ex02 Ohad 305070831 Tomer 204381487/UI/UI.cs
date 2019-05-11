namespace UI
{
    public class Console
    {
        public static void RecieveInputFromUser(Player_Data.PlayersData i_players)
        {
            int numOfPlayers = 0;
            System.Console.WriteLine("Please enter your name: /n");
            string player1Name = System.Console.ReadLine();

            System.Console.WriteLine("How many players are playing? (enter '1' or '2')");
            bool parseResult = int.TryParse(System.Console.ReadLine(), out numOfPlayers);

            if (parseResult == false)
            {
                System.Console.WriteLine("There was an Error!\n");
            }
            else
            {
                if (numOfPlayers == 2)
                {
                    System.Console.WriteLine("Please enter the second player's name: ");
                    string player2Name = System.Console.ReadLine();
                    i_players.SetPlayersNames(player1Name, player2Name);
                }
                else
                {
                    i_players.SetPlayersNames(player1Name);
                }
            }
        }
    }
}
