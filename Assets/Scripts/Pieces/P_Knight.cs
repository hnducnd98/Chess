using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Knight : BasePiece
{
    public override void BeSelected()
    {
        //Xác định 8 vị trí mà mã có thể đi
        //1. Nếu trống => TARGETED
        //2. Nếu không trống + là quân địch => TARGETED
        _targetedCells = getTargetTableLocation();

        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }

    public override void BeSelectedSameColor()
    {
        _targetedCells = getTargetTableLocation();

        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }
    }

    private List<Cell> getTargetTableLocationSameColor()
    {
        List<Cell> list = new List<Cell>();

        //1
        CLocation c = new CLocation(this.location.X + 2, this.location.Y - 1);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //2
        c = new CLocation(this.location.X + 2, this.location.Y + 1);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //3
        c = new CLocation(this.location.X - 2, this.location.Y - 1);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //4
        c = new CLocation(this.location.X - 2, this.location.Y + 1);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //5
        c = new CLocation(this.location.X + 1, this.location.Y - 2);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //6
        c = new CLocation(this.location.X + 1, this.location.Y + 2);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //7
        c = new CLocation(this.location.X - 1, this.location.Y - 2);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //8
        c = new CLocation(this.location.X - 1, this.location.Y + 2);
        if (CHelper.checkLocationSameColor(c))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        return list;
    }

    private List<Cell> getTargetTableLocation()
    {
        List<Cell> list = new List<Cell>();

        //1
        CLocation c = new CLocation(this.location.X + 2, this.location.Y - 1);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //2
        c = new CLocation(this.location.X + 2, this.location.Y + 1);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //3
        c = new CLocation(this.location.X - 2, this.location.Y - 1);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //4
        c = new CLocation(this.location.X - 2, this.location.Y + 1);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //5
        c = new CLocation(this.location.X + 1, this.location.Y - 2);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //6
        c = new CLocation(this.location.X + 1, this.location.Y + 2);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //7
        c = new CLocation(this.location.X - 1, this.location.Y - 2);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        //8
        c = new CLocation(this.location.X - 1, this.location.Y + 2);
        if (CHelper.checkLocation(c,this))
            list.Add(ChessBoard.Current.Cells[c.X][c.Y]);

        return list;
    }
}
