using System;
using Game_Data;

namespace Game_Logic
{
    public class TurnManager
    {
        private const int k_Increase = 1;
        private const int k_Decrease = -1;
        private const int k_DontMove = 0;


        public enum eDirection {Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }


        public static void OtheloTurnManager(ref Game_Data.Board io_otheloBoard, Player_Data.Player i_player)
        {
            Board tempOtheloBoard = new Board(ref io_otheloBoard);
            Board.Point playersPointChoice;

            if (UpdateValidCells(ref tempOtheloBoard, i_player.M_Color) > 0)
            {
                playersPointChoice = UI.Console.RecievePointFromPlayer(tempOtheloBoard, i_player);
                io_otheloBoard.M_OtheloBoard[playersPointChoice.M_Longtitude - 1, (int)playersPointChoice.M_Latitude - 'A'].M_CellValue = i_player.M_Color;
                UI.Console.ClearScreen();
                Game_Data.Board.PrintBoard(ref io_otheloBoard);//not here.
            }
        }

        public static int UpdateValidCells(ref Board io_otheloBoard, char i_playerColor)
        {
            int o_numberOfValidPlacementPoints = 0;

            foreach (Board.Point currentPoint in io_otheloBoard.M_OtheloBoard )
            {
                if (currentPoint.M_CellValue == i_playerColor)
                {
                    for (eDirection direction = eDirection.Up; direction <= eDirection.UpLeft; direction++ )
                    {
                        SingleCellValidityCheck(ref io_otheloBoard, currentPoint, direction, ref o_numberOfValidPlacementPoints);
                    }
                }
            }

            return o_numberOfValidPlacementPoints;
        }

        public static void SingleCellValidityCheck(ref Board io_otheloBoard, Board.Point i_currentPoint, eDirection i_direction, ref int io_numberOfValidPlacementPoints)
        {
            switch(i_direction)
            {
                case eDirection.Up:
                    UpdateCellsValidity(ref io_otheloBoard,i_currentPoint, k_Decrease, k_DontMove , ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.UpRight:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Increase, ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.Right:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Increase, ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.DownRight:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_Increase, ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.Down:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_DontMove, ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.DownLeft:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_Decrease, ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.Left:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Decrease, ref io_numberOfValidPlacementPoints);
                    break;

                case eDirection.UpLeft:
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Decrease, ref io_numberOfValidPlacementPoints);
                    break;
            }
        }

        public static void UpdateCellsValidity(ref Board io_otheloBoard, Board.Point i_currentPoint, int i_longtitudeValue, int i_latitudeValue, ref int io_numberOfValidPlacementPoints)
        {
            int latitude = (i_currentPoint.M_Latitude - 'A') + i_latitudeValue;
            int longtitude = i_currentPoint.M_Longtitude - 1 + i_longtitudeValue;
            bool isPotentialValidPoint = false;

            while (IsPointOnBoardAndRivalDisc(io_otheloBoard, longtitude, latitude, i_currentPoint.M_CellValue) == true)
            {
                longtitude += i_longtitudeValue;
                latitude += i_latitudeValue;
                isPotentialValidPoint = true;
            }

            if (latitude >= 0 && latitude < io_otheloBoard.M_BoardSize && longtitude >= 0 && longtitude < io_otheloBoard.M_BoardSize && isPotentialValidPoint == true)
            {
                io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
                io_numberOfValidPlacementPoints += 1;
            }
        }

        public static bool IsPointOnBoardAndRivalDisc(Board i_otheloBoard, int i_longtitude, int i_latitude, char i_playerColor)
        {
            return (i_latitude >= 0 && i_latitude < i_otheloBoard.M_BoardSize && i_longtitude >= 0 && i_longtitude < i_otheloBoard.M_BoardSize && i_otheloBoard.M_OtheloBoard[i_longtitude, i_latitude].M_CellValue != i_playerColor && i_otheloBoard.M_OtheloBoard[i_longtitude, i_latitude].M_CellValue != Board.Point.k_Empty);
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
