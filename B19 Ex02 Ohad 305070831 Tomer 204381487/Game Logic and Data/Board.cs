using System;


namespace Game_Data
{
    public class Board
    {
        private int m_BoardSize;
        private Point[,] m_OtheloBoard;

        public Board(ref Board i_Board)
        {
            m_BoardSize = i_Board.M_BoardSize;
            m_OtheloBoard = new Point[m_BoardSize, m_BoardSize];

            char maxBoardLatitude = (char)('A' + m_BoardSize);

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (char c = 'A'; c < maxBoardLatitude; c++)
                {
                    m_OtheloBoard[i, (int)(c - 'A')] = new Point(i + 1, c, i_Board.m_OtheloBoard[i,(int)(c-'A')].M_CellValue);
                }
            }
        }

        public int M_BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }

        public Point[,] M_OtheloBoard
        {
            get
            {
                return m_OtheloBoard;
            }
            set
            {
                m_OtheloBoard = value;
            }
        }

        public Board(int i_boardSize)
        {
            m_BoardSize = i_boardSize;
            m_OtheloBoard = new Point[m_BoardSize, m_BoardSize];

            char maxBoardLatitude = (char)('A' + m_BoardSize);

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (char c = 'A'; c < maxBoardLatitude; c++)
                {
                    m_OtheloBoard[i, (c - 'A')] = new Point(i + 1, c, Point.k_Empty);
                }
            }

            InitializeBoard();
        }

        public void InitializeBoard()
        {
            if (m_BoardSize == 8)
            {
                m_OtheloBoard[3, 3].M_CellValue = Point.k_White;
                m_OtheloBoard[3, 4].M_CellValue = Point.k_Black;
                m_OtheloBoard[4, 3].M_CellValue = Point.k_Black;
                m_OtheloBoard[4, 4].M_CellValue = Point.k_White;
            }
            else if (m_BoardSize == 6)
            {
                m_OtheloBoard[2, 2].M_CellValue = Point.k_White;
                m_OtheloBoard[2, 3].M_CellValue = Point.k_Black;
                m_OtheloBoard[3, 2].M_CellValue = Point.k_Black;
                m_OtheloBoard[3, 3].M_CellValue = Point.k_White;
            }
        }

        public static void PrintBoard(ref Board io_otheloBoard)
        {
            for (int i = 0; i < io_otheloBoard.m_BoardSize; i++)
            {
                if (i == 0)
                {
                    if (io_otheloBoard.m_BoardSize == 8)
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

                for (char c = 'A'; c < (char)('A' + io_otheloBoard.m_BoardSize); c++)
                {
                    if (c == 'A')
                    {
                        string outputMessage = string.Format("{0} | {1} |", i + 1, io_otheloBoard.m_OtheloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                    else
                    {
                        string outputMessage = string.Format(" {0} |",io_otheloBoard.m_OtheloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                }

                if (io_otheloBoard.m_BoardSize == 8)
                {
                    Console.WriteLine("\n  =================================");
                }
                else
                {
                    Console.WriteLine("\n  =========================");
                }
            }
        }

        public static bool IsValidDiscPlacement(Board i_otheloBoard, int i_Longtitude, char i_Latitude, Player_Data.Player i_Player)
        {
            return (i_otheloBoard.M_OtheloBoard[i_Longtitude - 1, (int)i_Latitude - 'A'].M_IsAvailableCell == true);
        }

        public class Point
        {
            private bool m_IsAvailableCell = false;
            private int m_Longtitude;
            private char m_Latitude;
            private char m_CellValue;
            public const char k_Black = 'X';
            public const char k_White = 'O';
            public const char k_Empty = ' ';

            public Point(int i_longtitude, char i_latitude, char i_cellValue)
            {
                m_Longtitude = i_longtitude;
                m_Latitude = i_latitude;
                m_CellValue = i_cellValue;
            }

            public int M_Longtitude
            {
                get
                {
                    return m_Longtitude;
                }
                set
                {
                    m_Longtitude = value;
                }
            }

            public char M_Latitude
            {
                get
                {
                    return m_Latitude;
                }
                set
                {
                    m_Latitude = value;
                }
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

            public bool M_IsAvailableCell
            {
                get
                {
                    return m_IsAvailableCell;
                }
                set
                {
                    m_IsAvailableCell = value;
                }
            }

        }
    }
}
