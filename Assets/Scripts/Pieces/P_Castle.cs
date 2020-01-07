using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Castle : BasePiece
{
    /// <summary>
    /// Tìm các ô có khả năng di chuyển
    /// 1. Trống
    /// 2. Quân địch
    /// 3. Chạy ngang dọc cho đến khi quân bị cản
    /// </summary>
    public override void BeSelected()
    {
        //Di chuyển ngang : Tọa độ Y giữ nguyên

        for (int x = this.location.X + 1; x < 8; x++)
        {
            Cell c = ChessBoard.Current.Cells[x][this.location.Y];
            if (ChessBoard.Current.arrayLocationFuture[x, this.location.Y] == 0)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (ChessBoard.Current.arrayLocationFuture[c.location.X, c.location.Y] / this.Info.Score < 0)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }

        for (int x = this.location.X - 1; x >= 0; x--)
        {
            Cell c = ChessBoard.Current.Cells[x][this.location.Y];
            if (ChessBoard.Current.arrayLocationFuture[x, this.location.Y] == 0)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (ChessBoard.Current.arrayLocationFuture[c.location.X, c.location.Y] / this.Info.Score < 0)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }

        //Di chuyển dọc : Tọa độ X giữ nguyên
        for (int y = this.location.Y + 1; y < 8; y++)
        {
            Cell c = ChessBoard.Current.Cells[this.location.X][y];
            if (ChessBoard.Current.arrayLocationFuture[this.location.X, y] == 0)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (ChessBoard.Current.arrayLocationFuture[c.location.X, c.location.Y] / this.Info.Score < 0)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }

        for (int y = this.location.Y - 1; y >= 0; y--)
        {
            Cell c = ChessBoard.Current.Cells[this.location.X][y];
            if (ChessBoard.Current.arrayLocationFuture[this.location.X, y] == 0)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (ChessBoard.Current.arrayLocationFuture[c.location.X, c.location.Y] / this.Info.Score < 0)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }


        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }

    public override void BeSelectedSameColor()
    {
        //Di chuyển ngang : Tọa độ Y giữ nguyên

        for (int x = this.location.X + 1; x < 8; x++)
        {
            Cell c = ChessBoard.Current.Cells[x][this.location.Y];
            if (ChessBoard.Current.Cells[x][this.location.Y]._currentPiece == null)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (c._currentPiece.player == EPlayer.WHITE)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }

        for (int x = this.location.X - 1; x >= 0; x--)
        {
            Cell c = ChessBoard.Current.Cells[x][this.location.Y];
            if (ChessBoard.Current.Cells[x][this.location.Y]._currentPiece == null)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (c._currentPiece.player == EPlayer.WHITE)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }

        //Di chuyển dọc : Tọa độ X giữ nguyên
        for (int y = this.location.Y + 1; y < 8; y++)
        {
            Cell c = ChessBoard.Current.Cells[this.location.X][y];
            if (ChessBoard.Current.Cells[this.location.X][y]._currentPiece == null)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (c._currentPiece.player == EPlayer.WHITE)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }

        for (int y = this.location.Y - 1; y >= 0; y--)
        {
            Cell c = ChessBoard.Current.Cells[this.location.X][y];
            if (ChessBoard.Current.Cells[this.location.X][y]._currentPiece == null)
            {
                //Nếu trống
                //TARGETED
                _targetedCells.Add(c);
            }
            else
            {
                //Nếu không trống
                if (c._currentPiece.player == EPlayer.WHITE)
                {
                    //TARGETED
                    _targetedCells.Add(c);
                }
                break;
            }
        }


        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }
}
