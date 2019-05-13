using System;


namespace Game_Logic_and_Data
{
    public class TurnManager
    {
        private const int k_Increase = 1;
        private const int k_Decrease = -1;
        private const int k_DontMove = 0;


        public enum eDirection {Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }


        public static void OtheloTurnManager(ref Board io_otheloBoard, Player_Data.Player i_player)
        {
            Board tempOtheloBoard = new Board(ref io_otheloBoard);
            Board.Point playersPointChoice;
            UpdateValidCells(ref tempOtheloBoard, i_player.M_Color);
            //UI - get the point.
            //check if valid - if not while to get a new point.
            //put in , and update acordingly.
            


        }

        public static void UpdateValidCells(ref Board io_otheloBoard, char i_playerColor)
        {
            foreach(Board.Point currentPoint in io_otheloBoard.M_OtheloBoard )
            {
                if (currentPoint.M_CellValue == i_playerColor)
                {
                    for (eDirection direction = eDirection.Up; direction <= eDirection.UpLeft; direction++ )
                    {
                        SingleCellValidityCheck(ref io_otheloBoard, currentPoint, direction);
                    }
                }
            }
        }

        public static void SingleCellValidityCheck(ref Board io_otheloBoard, Board.Point i_currentPoint, eDirection i_direction)
        {
            switch(i_direction)
            {
                case eDirection.Up:
                    UpdateCellsValidity(ref io_otheloBoard,i_currentPoint, k_Decrease, k_DontMove);
                    break;

                case eDirection.UpRight:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Increase);
                    break;

                case eDirection.Right:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Increase);
                    break;

                case eDirection.DownRight:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_Increase);
                    break;

                case eDirection.Down:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_DontMove);
                    break;

                case eDirection.DownLeft:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_Decrease);
                    break;

                case eDirection.Left:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Decrease); break;

                case eDirection.UpLeft:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Decrease); break;
            }
        }

        public static void UpdateCellsValidity(ref Board io_otheloBoard, Board.Point i_currentPoint , int i_longtitudeValue , int i_latitudeValue)
        {
            int latitude = (i_currentPoint.M_Latitude - 'A') + i_latitudeValue;
            int longtitude = i_currentPoint.M_Longtitude + i_longtitudeValue;
            while (latitude >= 0 && latitude < io_otheloBoard.M_BoardSize && longtitude >= 0 && longtitude < io_otheloBoard.M_BoardSize && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
            {
                longtitude += i_longtitudeValue;
                latitude += i_latitudeValue;
            }

            if (latitude >= 0 && latitude < io_otheloBoard.M_BoardSize && longtitude >= 0 && longtitude < io_otheloBoard.M_BoardSize)
            {
                io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
            }
        }
    }
}


/*
public static void CheckValidityUp(ref Board io_otheloBoard, Board.Point i_currentPoint)
{
    int latitude = i_currentPoint.M_Latitude - 'A';
    int longtitude = i_currentPoint.M_Longtitude - 1;
    while (longtitude >= 0 && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
    {
        longtitude -= 1;
    }

    if (longtitude >= 0)
    {
        io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
    }
}

public static void CheckValidityUpRight(ref Board io_otheloBoard, Board.Point i_currentPoint)
{
    int latitude = (i_currentPoint.M_Latitude - 'A') + 1;
    int longtitude = i_currentPoint.M_Longtitude - 1;
    while (latitude < io_otheloBoard.M_BoardSize && longtitude >= 0 && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
    {
        longtitude -= 1;
        latitude += 1;
    }

    if (longtitude >= 0 && latitude <= io_otheloBoard.M_BoardSize)
    {
        io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
    }
}

public static void CheckValidityRight(ref Board io_otheloBoard, Board.Point i_currentPoint)
{
    int latitude = i_currentPoint.M_Latitude - 'A' + 1;
    int longtitude = i_currentPoint.M_Longtitude;
    while (longtitude >= 0 && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
    {
        latitude += 1;
    }

    if (latitude < io_otheloBoard.M_BoardSize) 
    {
        io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
    }
}

public static void CheckValidityDownRight(ref Board io_otheloBoard, Board.Point i_currentPoint)
{
    int latitude = (i_currentPoint.M_Latitude - 'A') + 1;
    int longtitude = i_currentPoint.M_Longtitude + 1;
    while (latitude < io_otheloBoard.M_BoardSize && longtitude < io_otheloBoard.M_BoardSize && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
    {
        longtitude += 1;
        latitude += 1;
    }

    if (latitude < io_otheloBoard.M_BoardSize && longtitude < io_otheloBoard.M_BoardSize)
    {
        io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
    }
}

public static void CheckValidityDown(ref Board io_otheloBoard, Board.Point i_currentPoint)
{
    int latitude = i_currentPoint.M_Latitude - 'A';
    int longtitude = i_currentPoint.M_Longtitude + 1;
    while (longtitude < io_otheloBoard.M_BoardSize && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
    {
        longtitude += 1;
    }

    if (longtitude < io_otheloBoard.M_BoardSize)
    {
        io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
    }
}

public static void CheckValidityDownLeft(ref Board io_otheloBoard, Board.Point i_currentPoint)
{
    int latitude = (i_currentPoint.M_Latitude - 'A') - 1;
    int longtitude = i_currentPoint.M_Longtitude + 1;
    while (latitude >= 0 && longtitude < io_otheloBoard.M_BoardSize && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue) && (io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != Board.Point.k_Empty))
    {
        longtitude += 1;
        latitude -= 1;
    }

    if (latitude >= 0 && longtitude < io_otheloBoard.M_BoardSize)
    {
        io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
    }
}
*/
