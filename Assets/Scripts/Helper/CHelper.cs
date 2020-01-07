using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHelper
{
    /// <summary>
    /// Kiểm tra xem tọa độ có thỏa mãn không
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool checkLocation(CLocation c, BasePiece piece)
    {
        if (c.X < 0 || c.X > 7 || c.Y < 0 || c.Y > 7)
            return false;
        else
        {
            //if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece != null)
            if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] != 0)
            {
                if (piece.player == EPlayer.BLACK)
                {
                    if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] < 0)
                    {
                        return false;
                    }
                }
                if (piece.player == EPlayer.WHITE)
                {
                    if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public static bool checkLocationSameColor(CLocation c)
    {
        if (c.X < 0 || c.X > 7 || c.Y < 0 || c.Y > 7)
            return false;
        else
        {
            if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece != null)
            {
                if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece.player == EPlayer.BLACK)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
