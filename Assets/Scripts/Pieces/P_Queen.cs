using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Queen : BasePiece
{
    public override void BeSelected()
    {
        HuongDiTuong();
        HuongDiXe();
    }

    public override void BeSelectedSameColor()
    {
        HuongDiTuongSameColor();
        HuongDiXeSameColor();
    }

    public void HuongDiTuongSameColor()
    {
        //Thực hiện kiểm tra 4 đường chéo, cho đến khi gặp điểm dừng
        //1. Phải trên : x++, y++
        CLocation c = new CLocation(this.location.X + 1, this.location.Y + 1);
        while (CHelper.checkLocationSameColor(c))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece != null)
                break;
            c = new CLocation(c.X + 1, c.Y + 1);
        }

        //2. Phải dưới : x++, y--
        c = new CLocation(this.location.X + 1, this.location.Y - 1);
        while (CHelper.checkLocationSameColor(c))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece != null)
                break;
            c = new CLocation(c.X + 1, c.Y - 1);
        }

        //3. Trái trên : x--, y++
        c = new CLocation(this.location.X - 1, this.location.Y + 1);
        while (CHelper.checkLocationSameColor(c))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece != null)
                break;
            c = new CLocation(c.X - 1, c.Y + 1);
        }

        //4. Trái dưới : x--, y--
        c = new CLocation(this.location.X - 1, this.location.Y - 1);
        while (CHelper.checkLocationSameColor(c))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.Cells[c.X][c.Y]._currentPiece != null)
                break;
            c = new CLocation(c.X - 1, c.Y - 1);
        }


        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }

    public void HuongDiXeSameColor()
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

    public void HuongDiTuong()
    {
        //Thực hiện kiểm tra 4 đường chéo, cho đến khi gặp điểm dừng
        //1. Phải trên : x++, y++
        CLocation c = new CLocation(this.location.X + 1, this.location.Y + 1);
        while (CHelper.checkLocation(c, this))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] != 0)
                break;
            c = new CLocation(c.X + 1, c.Y + 1);
        }

        //2. Phải dưới : x++, y--
        c = new CLocation(this.location.X + 1, this.location.Y - 1);
        while (CHelper.checkLocation(c, this))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] != 0)
                break;
            c = new CLocation(c.X + 1, c.Y - 1);
        }

        //3. Trái trên : x--, y++
        c = new CLocation(this.location.X - 1, this.location.Y + 1);
        while (CHelper.checkLocation(c, this))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] != 0)
                break;
            c = new CLocation(c.X - 1, c.Y + 1);
        }

        //4. Trái dưới : x--, y--
        c = new CLocation(this.location.X - 1, this.location.Y - 1);
        while (CHelper.checkLocation(c, this))
        {
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);
            if (ChessBoard.Current.arrayLocationFuture[c.X, c.Y] != 0)
                break;
            c = new CLocation(c.X - 1, c.Y - 1);
        }


        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }

    public void HuongDiXe()
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
}
