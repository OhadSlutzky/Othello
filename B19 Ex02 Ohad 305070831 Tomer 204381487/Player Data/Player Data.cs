using System;

namespace Player_Data
{
    public class PlayersData
    {
        private string m_Player1Name;
        private string m_Player2Name;

        public PlayersData() { m_Player1Name = null;  m_Player2Name = null; }

        public void SetPlayer1Name(string i_Player1Name)
        {
            m_Player1Name = i_Player1Name;
        }

        public void SetPlayer2Name(string i_Player2Name)
        {
            m_Player2Name = i_Player2Name;
        }

        public void SetPlayersName(string i_Player1Name, string i_Player2Name = null)
        {
            SetPlayer1Name(i_Player1Name);

            if (i_Player2Name != null)
            {
                SetPlayer2Name(i_Player2Name);
            }
        }

        public string GetPlayer1Name()
        {
            return m_Player1Name;
        }

        public string GetPlayer2Name()
        {
            return m_Player2Name;
        }

        public void ReciveInputFromUser()
        {
            int numOfPlayers = 0;
            Console.WriteLine("Please enter your name: /n");
            string player1Name = Console.ReadLine();

            Console.WriteLine("How many players are playing? (enter '1' or '2')");
            bool parseResult = int.TryParse(Console.ReadLine(), out numOfPlayers);

            if (parseResult == false)
            {
                Console.WriteLine("There was an Error!\n");
            }
            else
            {
                if (numOfPlayers == 2)
                {
                    Console.WriteLine("Please enter the second player's name: \n");
                    string player2Name = Console.ReadLine();
                    SetPlayersName(player1Name, player2Name);
                }
                else
                {
                    SetPlayersName(player1Name);
                }
            }
        }
    }
}
