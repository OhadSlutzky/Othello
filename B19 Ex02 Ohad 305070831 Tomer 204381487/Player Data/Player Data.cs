using System;

namespace Player_Data
{
    public class PlayersData
    {
        private string m_Player1Name;
        private string m_Player2Name;

        public PlayersData() { m_Player1Name = null;  m_Player2Name = null; }

        public string M_Player1Name
        {
            get
            {
                return m_Player1Name;
            }
            set
            {
                m_Player1Name = value;
            }
        }

        public string M_Player2Name
        {
            get
            {
                return m_Player2Name;
            }
            set
            {
                m_Player2Name = value;
            }
        }

        public void SetPlayersNames(string i_Player1Name, string i_Player2Name = null)
        {
            m_Player1Name = i_Player1Name;

            if (i_Player2Name != null)
            {
                m_Player2Name = i_Player2Name;
            }
        }
    }
}
