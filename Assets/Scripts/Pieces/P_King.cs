using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_King : BasePiece
{
    private bool isFirstMoved = true;
    public override void BeSelected()
    {
        //Nhập thành
        if (isFirstMoved)
        {
            if (ChessBoard.Current.arrayLocationFuture[this.location.X + 3, this.location.Y] != 0 &&
                ChessBoard.Current.arrayLocationFuture[this.location.X + 3, this.location.Y] == Math.Abs(50) &&
                ChessBoard.Current.arrayLocationFuture[this.location.X + 3, this.location.Y] / this.Info.Score > 0)
            {
                if (ChessBoard.Current.arrayLocationFuture[this.location.X + 1, this.location.Y] == 0 &&
                   ChessBoard.Current.arrayLocationFuture[this.location.X + 2, this.location.Y] == 0)
                {
                    _targetedCells.Add(ChessBoard.Current.Cells[this.location.X + 2][this.location.Y]);
                }
            }

            if (ChessBoard.Current.arrayLocationFuture[this.location.X - 4, this.location.Y] != 0 &&
               ChessBoard.Current.arrayLocationFuture[this.location.X - 4, this.location.Y] == Math.Abs(50) &&
               ChessBoard.Current.arrayLocationFuture[this.location.X - 4, this.location.Y] / this.Info.Score > 0)
            {
                if (ChessBoard.Current.arrayLocationFuture[this.location.X - 1, this.location.Y] == 0 &&
                   ChessBoard.Current.arrayLocationFuture[this.location.X - 2, this.location.Y] == 0 &&
                   ChessBoard.Current.arrayLocationFuture[this.location.X - 3, this.location.Y] == 0)
                {
                    _targetedCells.Add(ChessBoard.Current.Cells[this.location.X - 2][this.location.Y]);
                }
            }
        }

        //8 ô xung quanh nó
        //1. x++, y++
        CLocation c = new CLocation(this.location.X + 1, this.location.Y + 1);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //2. x++, y
        c = new CLocation(this.location.X + 1, this.location.Y);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //3. x++, y--
        c = new CLocation(this.location.X + 1, this.location.Y - 1);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //4. x, y--
        c = new CLocation(this.location.X, this.location.Y - 1);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //5. x--, y--
        c = new CLocation(this.location.X - 1, this.location.Y - 1);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //6. x--, y
        c = new CLocation(this.location.X - 1, this.location.Y);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //7. x--, y++
        c = new CLocation(this.location.X - 1, this.location.Y + 1);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //8. x, y++
        c = new CLocation(this.location.X, this.location.Y + 1);
        if (CHelper.checkLocation(c, this))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);



        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }

    public override void Move(Cell targetedCell)
    {
        if (isFirstMoved == true)
        {
            if (targetedCell.location.X - this.location.X == 2)
            {
                ChessBoard.Current.Cells[location.X + 3][location.Y]._currentPiece.Move(ChessBoard.Current.Cells[location.X + 1][location.Y]);
            }
            else
            {
                if (targetedCell.location.X - this.location.X == -2)
                {
                    ChessBoard.Current.Cells[location.X - 4][location.Y]._currentPiece.Move(ChessBoard.Current.Cells[location.X - 1][location.Y]);
                }
            }
        }
        isFirstMoved = false;
        base.Move(targetedCell);
    }

    public override void BeAttackedBy(BasePiece enemy)
    {
        base.BeAttackedBy(enemy);
        BaseGameCTL.Current.GameOver(enemy.player);
    }

    public override void BeSelectedSameColor()
    {
        //8 ô xung quanh nó
        //1. x++, y++
        CLocation c = new CLocation(this.location.X + 1, this.location.Y + 1);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //2. x++, y
        c = new CLocation(this.location.X + 1, this.location.Y);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //3. x++, y--
        c = new CLocation(this.location.X + 1, this.location.Y - 1);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //4. x, y--
        c = new CLocation(this.location.X, this.location.Y - 1);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //5. x--, y--
        c = new CLocation(this.location.X - 1, this.location.Y - 1);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //6. x--, y
        c = new CLocation(this.location.X - 1, this.location.Y);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //7. x--, y++
        c = new CLocation(this.location.X - 1, this.location.Y + 1);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //8. x, y++
        c = new CLocation(this.location.X, this.location.Y + 1);
        if (CHelper.checkLocationSameColor(c))
            _targetedCells.Add(ChessBoard.Current.Cells[c.X][c.Y]);



        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }
}
