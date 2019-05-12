using System;


namespace Game_Logic_and_Data
{
    public class Board
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
                    m_otheloBoard[i, (c - 'A')] = new Point(i + 1, c, Point.k_Empty);
                }
            }

            InitializeBoard();
        }

        public void InitializeBoard()
        {
            if (m_boardSize == 8)
            {
                m_otheloBoard[3, 3].M_CellValue = Point.k_White;
                m_otheloBoard[3, 4].M_CellValue = Point.k_Black;
                m_otheloBoard[4, 3].M_CellValue = Point.k_Black;
                m_otheloBoard[4, 4].M_CellValue = Point.k_White;
            }
            else if (m_boardSize == 6)
            {
                m_otheloBoard[2, 2].M_CellValue = Point.k_White;
                m_otheloBoard[2, 3].M_CellValue = Point.k_Black;
                m_otheloBoard[3, 2].M_CellValue = Point.k_Black;
                m_otheloBoard[3, 3].M_CellValue = Point.k_White;
            }
        }

        public static void PrintBoard(ref Board io_otheloBoard)
        {
            for (int i = 0; i < io_otheloBoard.m_boardSize; i++)
            {
                if (i == 0)
                {
                    if (io_otheloBoard.m_boardSize == 8)
                    {
                        Console.WriteLine("    A   B   C   D   E   F   G   H");
                        Console.WriteLine("  =================================");
                    }
                    else
                    {
                        Console.WriteLine("    A   B   C   D   E   F");
                        Console.WriteLine("  =========================");
                    }
                }

                for (char c = 'A'; c < (char)('A' + io_otheloBoard.m_boardSize); c++)
                {
                    if (c == 'A')
                    {
                        string outputMessage = string.Format("{0} | {1} |", i + 1, io_otheloBoard.m_otheloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                    else
                    {
                        string outputMessage = string.Format(" {0} |",io_otheloBoard.m_otheloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                }

                if (io_otheloBoard.m_boardSize == 8)
                {
                    Console.WriteLine("\n  =================================");
                }
                else
                {
                    Console.WriteLine("\n  =========================");
                }
            }
        }

        class Point
        {
            private bool m_IsAvailableCell = false;
            private int m_Longtitude;
            private char m_Latitude;
            private char m_CellValue;
            internal const char k_Black = 'X';
            internal const char k_White = 'O';
            internal const char k_Empty = ' ';


            public Point(int i_longtitude, char i_latitude, char i_cellValue)
            {
                m_Longtitude = i_longtitude;
                m_Latitude = i_latitude;
                m_CellValue = i_cellValue;
            }

            public char M_CellValue
            {
                get
                {
                    return m_CellValue;
                }
                set
                {
                    m_CellValue = value;
                }
            }
            
        }
    }


}
