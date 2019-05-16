using System;

namespace Game_Data
{
    public class Board
    {
        private int m_BoardSize;
        private Point[,] m_OtheloBoard;

        public static void PrintBoard(Board i_otheloBoard)
        {
            for (int i = 0; i < i_otheloBoard.m_BoardSize; i++)
            {
                if (i == 0)
                {
                    if (i_otheloBoard.m_BoardSize == 8)
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

                for (char c = 'A'; c < (char)('A' + i_otheloBoard.m_BoardSize); c++)
                {
                    if (c == 'A')
                    {
                        string outputMessage = string.Format("{0} | {1} |", i + 1, i_otheloBoard.m_OtheloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                    else
                    {
                        string outputMessage = string.Format(" {0} |", i_otheloBoard.m_OtheloBoard[i, c - 'A'].M_CellValue);
                        Console.Write(outputMessage);
                    }
                }

                if (i_otheloBoard.m_BoardSize == 8)
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

        public static bool IsValidDiscPlacement(Board i_otheloBoard, int i_Longtitude, char i_Latitude, Game_Data.Player i_Player)
        {
            return i_otheloBoard.M_OtheloBoard[i_Longtitude - 1, i_Latitude - 'A'].M_IsAvailableCell == true; ///styleCop took down the (int) cast! !
        }

        public Board(Board i_Board)
        {
            m_BoardSize = i_Board.M_BoardSize;
            m_OtheloBoard = new Point[m_BoardSize, m_BoardSize];
            char maxBoardLatitude = (char)('A' + m_BoardSize);
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (char c = 'A'; c < maxBoardLatitude; c++)
                {
                    m_OtheloBoard[i, (int)(c - 'A')] = new Point(i + 1, c, i_Board.m_OtheloBoard[i, (int)(c - 'A')].M_CellValue);
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

                m_OtheloBoard[3, 3].M_IsAvailableCell = false;
                m_OtheloBoard[3, 4].M_IsAvailableCell = false;
                m_OtheloBoard[4, 3].M_IsAvailableCell = false;
                m_OtheloBoard[4, 4].M_IsAvailableCell = false;
            }
            else if (m_BoardSize == 6)
            {
                m_OtheloBoard[2, 2].M_CellValue = Point.k_White;
                m_OtheloBoard[2, 3].M_CellValue = Point.k_Black;
                m_OtheloBoard[3, 2].M_CellValue = Point.k_Black;
                m_OtheloBoard[3, 3].M_CellValue = Point.k_White;

                m_OtheloBoard[2, 2].M_IsAvailableCell = false;
                m_OtheloBoard[2, 3].M_IsAvailableCell = false;
                m_OtheloBoard[3, 2].M_IsAvailableCell = false;
                m_OtheloBoard[3, 3].M_IsAvailableCell = false;
            }
        }

        public void CountNumberOfDiscsForBothPlayers(ref int player1NumberOfDiscs, ref int player2NumberOfDiscs)
        {
            foreach (Board.Point currentPoint in m_OtheloBoard)
            {
                if (currentPoint.M_CellValue == Point.k_Black)
                {
                    player1NumberOfDiscs += 1;
                }
                else if(currentPoint.M_CellValue == Point.k_White)
                {
                    player2NumberOfDiscs += 1;
                }
            }
        }

        public void ResetPointsValidityToFalse()
        {
            foreach(Point currentPoint in m_OtheloBoard)
            {
                currentPoint.M_IsAvailableCell = false;
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
