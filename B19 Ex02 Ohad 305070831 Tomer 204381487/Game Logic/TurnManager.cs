using System;
using Game_Data;
using System.Collections.Generic;

namespace Game_Logic
{
    public class TurnManager
    {
        private const int k_Increase = 1;
        private const int k_Decrease = -1;
        private const int k_DontMove = 0;
        private const string k_UpdateValidity = "UpdateCellValidity";
        private const string k_ChangeRivalDiscsCellsColor = "ChangeDiscsColor";

        public enum eDirection
        {
            Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft
        }

        public static void OtheloTurnManager(ref Game_Data.Board io_otheloBoard, Player i_player, ref int io_consecutiveNumberOfTurnsWithoutValidMoves)
        {
            Board tempOtheloBoard = new Board(ref io_otheloBoard);
            Board.Point playersPointChoice;
            List<Board.Point> validPointsToChooseFrom = new List<Board.Point>();
            //io_otheloBoard.ResetPointsValidityToFalse();

            if (UpdateValidCells(ref tempOtheloBoard, i_player.M_Color, ref validPointsToChooseFrom) > 0)
            {
                playersPointChoice = UI.Console.RecievePointFromPlayer(tempOtheloBoard, i_player, validPointsToChooseFrom);
                playersPointChoice.M_CellValue = i_player.M_Color;
                validPointsToChooseFrom.Clear();
                io_otheloBoard.M_OtheloBoard[playersPointChoice.M_Longtitude - 1, (int)playersPointChoice.M_Latitude - 'A'].M_CellValue = i_player.M_Color;
                io_otheloBoard.M_OtheloBoard[playersPointChoice.M_Longtitude - 1, (int)playersPointChoice.M_Latitude - 'A'].M_IsAvailableCell = false;
                UpdateBoardAfterDiscPlacement(io_otheloBoard, playersPointChoice);

                if (i_player.M_PlayerName.Equals("PC") == true)
                {
                    System.Threading.Thread.Sleep(2000);
                }

                UI.Console.ClearScreen();
                UI.Console.PrintBoard(io_otheloBoard);
                io_consecutiveNumberOfTurnsWithoutValidMoves = 0;
            }
            else
            {
                string message = string.Format("{0} HAS NO MOVES!", i_player.M_PlayerName);
                System.Console.WriteLine(message);
                io_consecutiveNumberOfTurnsWithoutValidMoves += 1;
            }
        }

        public static int UpdateValidCells(ref Board io_otheloBoard, char i_playerColor, ref List<Board.Point> io_validPointsToChooseFrom)
        {
            int noValueInt = 0;
            foreach (Board.Point currentPoint in io_otheloBoard.M_OtheloBoard )
            {
                if (currentPoint.M_CellValue == i_playerColor)
                {
                    for (eDirection direction = eDirection.Up; direction <= eDirection.UpLeft; direction++ )
                    {
                        EightWayCellsCheckAndUpdateValidityOrChangeCellsColor(ref io_otheloBoard, currentPoint, direction, k_UpdateValidity, ref io_validPointsToChooseFrom, ref noValueInt);
                    }
                }
            }

            return io_validPointsToChooseFrom.Count;
        }

        public static void EightWayCellsCheckAndUpdateValidityOrChangeCellsColor(ref Board io_otheloBoard, Board.Point i_currentPoint, eDirection i_direction, string UpdateValidityOrChangeColor, ref List<Board.Point> io_validPointsToChooseFrom, ref int io_NumberOfRivalDiscsToChangeIfNeeded)
        {
            switch(i_direction)
            {
                case eDirection.Up:
                    if (UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                        UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_DontMove, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_Decrease, k_DontMove, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_Decrease, k_DontMove, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.UpRight:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Increase, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Increase, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Increase, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.Right:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Increase, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Increase, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Increase, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.DownRight:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                        UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_Increase, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_Increase, k_Increase, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_Increase, k_Increase, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.Down:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_DontMove, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_Increase, k_DontMove, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_Increase, k_DontMove, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.DownLeft:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Increase, k_Decrease, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_Increase, k_Decrease, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint,  k_Increase, k_Decrease, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.Left:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Decrease, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Decrease, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_DontMove, k_Decrease, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;

                case eDirection.UpLeft:
                    if(UpdateValidityOrChangeColor == k_UpdateValidity)
                    {
                    UpdateCellsValidity(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Decrease, ref io_validPointsToChooseFrom);
                    }
                    else
                    {
                        if (IsRivalDiscChangeNeeded(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Decrease, ref io_NumberOfRivalDiscsToChangeIfNeeded))
                        {
                            ChangeDiscsColor(ref io_otheloBoard, i_currentPoint, k_Decrease, k_Decrease, io_NumberOfRivalDiscsToChangeIfNeeded);
                        }
                    }

                    break;
            }
        }

        public static void UpdateCellsValidity(ref Board io_otheloBoard, Board.Point i_currentPoint, int i_longtitudeValue, int i_latitudeValue, ref List<Board.Point> io_validPointsToChooseFrom)
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

            if (latitude >= 0 && latitude < io_otheloBoard.M_BoardSize && longtitude >= 0 && longtitude < io_otheloBoard.M_BoardSize && isPotentialValidPoint == true && io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue == Board.Point.k_Empty)
            {
                if(io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell == false)
                {
                io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_IsAvailableCell = true;
                    io_validPointsToChooseFrom.Add(io_otheloBoard.M_OtheloBoard[longtitude, latitude]);
                }
            }
        }

        public static bool IsPointOnBoardAndRivalDisc(Board i_otheloBoard, int i_longtitude, int i_latitude, char i_playerColor)
        {
            return i_latitude >= 0 && i_latitude < i_otheloBoard.M_BoardSize && i_longtitude >= 0 && i_longtitude < i_otheloBoard.M_BoardSize && i_otheloBoard.M_OtheloBoard[i_longtitude, i_latitude].M_CellValue != i_playerColor && i_otheloBoard.M_OtheloBoard[i_longtitude, i_latitude].M_CellValue != Board.Point.k_Empty;
        }

        public static bool IsRivalDiscChangeNeeded(ref Board io_otheloBoard, Board.Point i_currentPoint, int i_longtitudeValue, int i_latitudeValue, ref int io_numberOfRivalDiscsToChange)
        {
            int latitude = (i_currentPoint.M_Latitude - 'A') + i_latitudeValue;
            int longtitude = i_currentPoint.M_Longtitude - 1 + i_longtitudeValue;
            bool isChangeOfDiscsNeeded = false;

            while (IsPointOnBoardAndRivalDisc(io_otheloBoard, longtitude, latitude, i_currentPoint.M_CellValue) == true)
            {
                longtitude += i_longtitudeValue;
                latitude += i_latitudeValue;
                io_numberOfRivalDiscsToChange += 1;
                isChangeOfDiscsNeeded = true;
            }

            if (latitude < 0 || latitude >= io_otheloBoard.M_BoardSize || longtitude < 0 || longtitude >= io_otheloBoard.M_BoardSize || isChangeOfDiscsNeeded == false || io_otheloBoard.M_OtheloBoard[longtitude, latitude].M_CellValue != i_currentPoint.M_CellValue)
            {
                isChangeOfDiscsNeeded = false;
                io_numberOfRivalDiscsToChange = 0;
            }

            return isChangeOfDiscsNeeded;
        }

        internal static void ChangeDiscsColor(ref Board io_otheloBoard, Board.Point i_currentPoint, int i_longtitudeVal, int i_latitudeVal, int i_numberOfRivalDiscsToChange)
        {
            int startLongtitude = i_currentPoint.M_Longtitude - 1;
            int startLatitude = i_currentPoint.M_Latitude - 'A';

            for (int i = 1; i <= i_numberOfRivalDiscsToChange; i++)
            {
                ///if ()
                io_otheloBoard.M_OtheloBoard[startLongtitude + (i * i_longtitudeVal), startLatitude + (i * i_latitudeVal)].M_CellValue = i_currentPoint.M_CellValue;
            }
        }

        internal static void UpdateBoardAfterDiscPlacement(Board io_otheloBoard, Board.Point i_userPointChosen)
        {
            int numberOfDiscsToChange = 0;
            List<Board.Point> emptyList = new List<Board.Point>();
            for (eDirection direction = eDirection.Up; direction <= eDirection.UpLeft; direction++)
            {
                EightWayCellsCheckAndUpdateValidityOrChangeCellsColor(ref io_otheloBoard, i_userPointChosen, direction, k_ChangeRivalDiscsCellsColor, ref emptyList, ref numberOfDiscsToChange);
                numberOfDiscsToChange = 0; //might fix
            }
        }
    }
}