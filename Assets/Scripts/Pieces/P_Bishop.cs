using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Bishop : BasePiece
{
    public override void BeSelected()
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

    public override void BeSelectedSameColor()
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
}
