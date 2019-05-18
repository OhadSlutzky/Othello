using System;

namespace Game_Data
{
    public class Board
    {
        private int m_BoardSize;
        private Point[,] m_OthelloBoard;

        public static void PrintBoard(Board i_OthelloBoard)
        {
            for (int i = 0; i < i_OthelloBoard.m_BoardSize; i++)
            {
                if (i == 0)
                {
                    if (i_OthelloBoard.m_BoardSize == 8)
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

                for (char c = 'A'; c < (char)('A' + i_OthelloBoard.m_BoardSize); c++)
                {
                    if (c == 'A')
                    {
                        string outputMessage = string.Format("{0} | {1} |", i + 1, i_OthelloBoard.m_OthelloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                    else
                    {
                        string outputMessage = string.Format(" {0} |", i_OthelloBoard.m_OthelloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                }

                if (i_OthelloBoard.m_BoardSize == 8)
                {
                    Console.WriteLine("\n  =================================");
                }
                else
                {
                    Console.WriteLine("\n  =========================");
                }
            }

            Console.WriteLine();
        }

        public static bool IsValidDiscPlacement(Board i_OthelloBoard, int i_Longtitude, char i_Latitude, Game_Data.Player i_Player)
        {
            return i_OthelloBoard.M_OthelloBoard[i_Longtitude - 1, i_Latitude - 'A'].M_IsAvailableCell == true;
        }

        public Board(Board i_Board)
        {
            m_BoardSize = i_Board.M_BoardSize;
            m_OthelloBoard = new Point[m_BoardSize, m_BoardSize];

            char maxBoardLatitude = (char)('A' + m_BoardSize);

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (char c = 'A'; c < maxBoardLatitude; c++)
                {
                    m_OthelloBoard[i, (int)(c - 'A')] = new Point(i + 1, c, i_Board.m_OthelloBoard[i, (int)(c - 'A')].M_CellValue);
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

        public Point[,] M_OthelloBoard
        {
            get
            {
                return m_OthelloBoard;
            }

            set
            {
                m_OthelloBoard = value;
            }
        }

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_OthelloBoard = new Point[m_BoardSize, m_BoardSize];

            char maxBoardLatitude = (char)('A' + m_BoardSize);

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (char c = 'A'; c < maxBoardLatitude; c++)
                {
                    m_OthelloBoard[i, (c - 'A')] = new Point(i + 1, c, Point.k_Empty);
                }
            }

            InitializeBoard();
        }

        public void InitializeBoard()
        {
            if (m_BoardSize == 8)
            {
                m_OthelloBoard[3, 3].M_CellValue = Point.k_White;
                m_OthelloBoard[3, 4].M_CellValue = Point.k_Black;
                m_OthelloBoard[4, 3].M_CellValue = Point.k_Black;
                m_OthelloBoard[4, 4].M_CellValue = Point.k_White;

                m_OthelloBoard[3, 3].M_IsAvailableCell = false;
                m_OthelloBoard[3, 4].M_IsAvailableCell = false;
                m_OthelloBoard[4, 3].M_IsAvailableCell = false;
                m_OthelloBoard[4, 4].M_IsAvailableCell = false;
            }
            else if (m_BoardSize == 6)
            {
                m_OthelloBoard[2, 2].M_CellValue = Point.k_White;
                m_OthelloBoard[2, 3].M_CellValue = Point.k_Black;
                m_OthelloBoard[3, 2].M_CellValue = Point.k_Black;
                m_OthelloBoard[3, 3].M_CellValue = Point.k_White;

                m_OthelloBoard[2, 2].M_IsAvailableCell = false;
                m_OthelloBoard[2, 3].M_IsAvailableCell = false;
                m_OthelloBoard[3, 2].M_IsAvailableCell = false;
                m_OthelloBoard[3, 3].M_IsAvailableCell = false;
            }
        }

        public void CountNumberOfDiscsForBothPlayers(ref int io_Player1NumberOfDiscs, ref int io_Player2NumberOfDiscs)
        {
            foreach (Board.Point currentPoint in m_OthelloBoard)
            {
                if (currentPoint.M_CellValue == Point.k_Black)
                {
                    io_Player1NumberOfDiscs += 1;
                }
                else if (currentPoint.M_CellValue == Point.k_White)
                {
                    io_Player2NumberOfDiscs += 1;
                }
            }
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

            public Point(int i_Longtitude, char i_Latitude, char i_CellValue)
            {
                m_Longtitude = i_Longtitude;
                m_Latitude = i_Latitude;
                m_CellValue = i_CellValue;
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