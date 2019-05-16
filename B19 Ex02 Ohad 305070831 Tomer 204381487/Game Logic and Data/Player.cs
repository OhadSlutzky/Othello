using System;
using System.Collections.Generic;

namespace Game_Data
{
    public class Player
    {
        private string m_PlayerName;
        private char m_Color;
        public const char k_Black = 'X';
        public const char k_White = 'O';

        public Player()
        {
            m_PlayerName = null;
        }

        public string M_PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public char M_Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        public void PrintPlayerInfo()
        {
            string playerInfo = string.Format("{0} {1}", m_PlayerName, m_Color);
            System.Console.WriteLine(playerInfo);
        }
    }
}
