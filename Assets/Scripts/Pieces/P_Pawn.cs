using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Pawn : BasePiece
{
    private bool isFirstMoved = true;

    public override void BeSelected()
    {
        switch (this._ePlayer)
        {
            case EPlayer.BLACK:
                BeSelectedBlack();
                break;
            case EPlayer.WHITE:
                BeSelectedWhite();
                break;
            default:
                break;
        }
    }

    private void BeSelectedWhite()
    {
        //Hiển thị các nước đi có thể
        if (isFirstMoved)
        {
            //2. Có khả năng đi 2 bước về trước
            if (ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y + 2] == 0 &&
                ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y + 1] == 0)
                _targetedCells.Add(ChessBoard.Current.Cells[this.location.X][this.location.Y + 2]);
        }

        //1. Có khả năng đi 1 bước về trước
        if (ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y + 1] == 0)
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X][this.location.Y + 1]);

        //3. Xác định 2 ô chéo phía trước có quân ăn hay không
        //Ăn : Nếu là quân địch , tức là quân đen
        if (this.location.X > 0 && ChessBoard.Current.arrayLocationFuture[this.location.X - 1, this.location.Y + 1] != 0 &&
            ChessBoard.Current.arrayLocationFuture[this.location.X - 1, this.location.Y + 1] / this.Info.Score < 0)
        {
            //Phía trên bên trái
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X - 1][this.location.Y + 1]);
        }

        if (this.location.X < 7 && ChessBoard.Current.arrayLocationFuture[this.location.X + 1, this.location.Y + 1] != 0 &&
            ChessBoard.Current.arrayLocationFuture[this.location.X + 1, this.location.Y + 1] / this.Info.Score < 0)
        {
            //Phía trên bên phải
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X + 1][this.location.Y + 1]);
        }

        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }

        //4. Xác định trường hợp bắt tốt qua đường
    }

    private void BeSelectedBlack()
    {
        //Hiển thị các nước đi có thể
        if (isFirstMoved)
        {
            //2. Có khả năng đi 2 bước về trước
            if (ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y - 2] == 0 &&
                ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y - 1] == 0)
                _targetedCells.Add(ChessBoard.Current.Cells[this.location.X][this.location.Y - 2]);
        }

        //1. Có khả năng đi 1 bước về trước
        if (ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y - 1] == 0)
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X][this.location.Y - 1]);

        //3. Xác định 2 ô chéo phía trước có quân ăn hay không
        //Ăn : Nếu là quân địch , tức là quân đen
        if (this.location.X > 0 && ChessBoard.Current.arrayLocationFuture[this.location.X - 1, this.location.Y - 1] != 0 &&
            ChessBoard.Current.arrayLocationFuture[this.location.X - 1, this.location.Y - 1] / this.Info.Score < 0)
        {
            //Phía trên bên trái
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X - 1][this.location.Y - 1]);
        }

        if (this.location.X < 7 && ChessBoard.Current.arrayLocationFuture[this.location.X + 1, this.location.Y - 1] != 0 &&
            ChessBoard.Current.arrayLocationFuture[this.location.X + 1, this.location.Y - 1] / this.Info.Score < 0)
        {
            //Phía trên bên phải
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X + 1][this.location.Y - 1]);
        }

        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }

        //4. Xác định trường hợp bắt tốt qua đường
    }

    public override void Move(Cell targetedCell)
    {
        isFirstMoved = false;

        base.Move(targetedCell);
    }

    public override void BeSelectedSameColor()
    {
        //Hiển thị các nước đi có thể
        if (isFirstMoved)
        {
            //2. Có khả năng đi 2 bước về trước
            if (ChessBoard.Current.Cells[this.location.X][this.location.Y + 2]._currentPiece == null &&
                ChessBoard.Current.Cells[this.location.X][this.location.Y + 1]._currentPiece == null)
                _targetedCells.Add(ChessBoard.Current.Cells[this.location.X][this.location.Y + 2]);
        }

        //1. Có khả năng đi 1 bước về trước
        if (ChessBoard.Current.Cells[this.location.X][this.location.Y + 1]._currentPiece == null)
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X][this.location.Y + 1]);

        //3. Xác định 2 ô chéo phía trước có quân ăn hay không
        //Ăn : Nếu là quân địch , tức là quân đen
        if (this.location.X > 0 && ChessBoard.Current.Cells[this.location.X - 1][this.location.Y + 1]._currentPiece != null &&
            ChessBoard.Current.Cells[this.location.X - 1][this.location.Y + 1]._currentPiece.player == EPlayer.WHITE)
        {
            //Phía trên bên trái
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X - 1][this.location.Y + 1]);
        }

        if (this.location.X < 7 && ChessBoard.Current.Cells[this.location.X + 1][this.location.Y + 1]._currentPiece != null &&
            ChessBoard.Current.Cells[this.location.X + 1][this.location.Y + 1]._currentPiece.player == EPlayer.WHITE)
        {
            //Phía trên bên phải
            _targetedCells.Add(ChessBoard.Current.Cells[this.location.X + 1][this.location.Y + 1]);
        }

        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.TARGETED);
        }

        //4. Xác định trường hợp bắt tốt qua đường
    }
}
