using System;

namespace Game_Logic_and_Data
{
    class Point
    {
        private bool m_isAvailableCell = false;
        private int m_longtitude;
        private char m_latitude;
        private int m_cellValue;
        private const int k_black = 1;
        private const int k_white = 0;
        private const int k_empty = -1;


        public Point(int i_longtitude, char i_latitude, int i_cellValue)
        {
            m_longtitude = i_longtitude;
            m_latitude = i_latitude;
            m_cellValue = i_cellValue;
        }
    }
}
