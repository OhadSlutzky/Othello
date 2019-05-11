using System;


namespace Game_Logic_and_Data
{
    class Board
    {
        private int m_boardSize;
        private Point[,] m_otheloBoard;

        public Board(int i_boardSize)
        {
            m_boardSize = i_boardSize;
            m_otheloBoard = new Point[m_boardSize, m_boardSize];
            char maxBoardLatitude = (char)('A' + m_boardSize);
            for (int i = 0; i < m_boardSize; i++)
            {
                for (char c = 'A'; c < maxBoardLatitude; c++)
                {
                    m_otheloBoard[i, (c - 'A')] = new Point(i + 1, c, Point.k_empty);
                }
            }

            InitializeBoard();
        }

        public void InitializeBoard()
        {
            if (m_boardSize == 8)
            {
                m_otheloBoard[3, 3].SetCellValue(Point.k_white);
                m_otheloBoard[3, 4].SetCellValue(Point.k_black);
                m_otheloBoard[4, 3].SetCellValue(Point.k_black);
                m_otheloBoard[4, 4].SetCellValue(Point.k_white);
            }
            else if (m_boardSize == 6)
            {
                m_otheloBoard[2, 2].SetCellValue(Point.k_white);
                m_otheloBoard[2, 3].SetCellValue(Point.k_black);
                m_otheloBoard[3, 2].SetCellValue(Point.k_black);
                m_otheloBoard[3, 3].SetCellValue(Point.k_white);
            }
        }

        public static void PrintBoard(ref Board io_otheloBoard)
        {
            
        }

        class Point
        {
            private bool m_isAvailableCell = false;
            private int m_longtitude;
            private char m_latitude;
            private int m_cellValue;
            internal const int k_black = 1;
            internal const int k_white = 0;
            internal const int k_empty = -1;


            public Point(int i_longtitude, char i_latitude, int i_cellValue)
            {
                m_longtitude = i_longtitude;
                m_latitude = i_latitude;
                m_cellValue = i_cellValue;
            }

            public void SetCellValue(int i_newCellValue)
            {
                m_cellValue = i_newCellValue;
            }

        }
    }
}
