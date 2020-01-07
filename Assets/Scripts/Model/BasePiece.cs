using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePiece : MonoBehaviour
{
    public List<Cell> _targetedCells = new List<Cell>();

    public Cell _currentCell;

    [SerializeField]
    private Vector2 offsetPosition;

    public PieceInfor Info { get; private set; }

    [SerializeField]
    protected EPlayer _ePlayer;

    public EPlayer player { get { return _ePlayer; } protected set { _ePlayer = value; } }

    public CLocation location { get; set; }

    public float currentLocationX { get; set; }
    public float currentLocationY { get; set; }

    public void setInfor(PieceInfor info, Cell newCell)
    {
        Info = info;
        SetNewLocation(newCell);
    }

    private int setPosCount = 0;
    protected void SetNewLocation(Cell newCell)
    {
        var me = this;

        ChessBoard.Current.arrayLocationFuture[this.location.X, this.location.Y] = 0;
        ChessBoard.Current.arrayLocationFuture[newCell.location.X, newCell.location.Y] = this.Info.Score;

        setPosCount++;
        if (setPosCount > 1)
            this._currentCell.SetPiece(null);

        this._currentCell = newCell;
        newCell.SetPiece(this);
        this.location = newCell.location;

        currentLocationX = newCell.location.X;
        currentLocationY = newCell.location.Y;

        //this.transform.position = offsetPosition + new Vector2(location.X * ChessBoard.Current.CELL_SIZE,
        //    location.Y * ChessBoard.Current.CELL_SIZE);

        this.transform.DOMove(offsetPosition + new Vector2(location.X * ChessBoard.Current.CELL_SIZE,
            location.Y * ChessBoard.Current.CELL_SIZE), 0.75f);





        if ((this.Info.Score == 10 && this.location.Y == 7) || (this.Info.Score == -10 && this.location.Y == 0))
        {
            var pieceInfor = new PieceInfor();
            if (this.location.Y == 7)
            {
                pieceInfor.Name = "W_QUEEN_" + ChessBoard.Current.indexQueenWhite;
                pieceInfor.Path = "Pieces/W_QUEEN";
                pieceInfor.Score = 80;
                pieceInfor.X = this.location.X;
                pieceInfor.Y = this.location.Y;
                ChessBoard.Current.indexQueenWhite++;
            }
            else
            {
                pieceInfor.Name = "B_QUEEN_" + ChessBoard.Current.indexQueenBlack;
                pieceInfor.Path = "Pieces/B_QUEEN";
                pieceInfor.Score = 80;
                pieceInfor.X = this.location.X;
                pieceInfor.Y = this.location.Y;
                ChessBoard.Current.indexQueenBlack++;
            }

            GameObject.Destroy(this.gameObject);

            GameObject GO = GameObject.Instantiate<GameObject>(ResourceCTL.Instance.GetGameObject(pieceInfor.Path));
            GO.transform.parent = ChessBoard.Current.transform.GetChild(1);
            GO.name = pieceInfor.Name;
            BasePiece p = GO.GetComponent<BasePiece>();
            //p.setInfor(pieceInfor, _cells[item.X][item.Y]);
            p.setInfor(pieceInfor, ChessBoard.Current.Cells[pieceInfor.X][pieceInfor.Y]);

            _currentCell.SetPiece(p);


            newCell._currentPiece.BeSelected();
            var list = new List<Cell>();
            foreach (var item in newCell._currentPiece._targetedCells)
            {
                list.Add(item);
            }
            var _pieceInfor = new EnemyInfor(newCell._currentPiece, list);
            if (this.Info.Score == 10)
            {
                ChessBoard.Current.whiteLifeCells.Add(_pieceInfor);
            }
            else
            {
                ChessBoard.Current.blackLifeCells.Add(_pieceInfor);
            }
            newCell._currentPiece.BeUnSelected();
        }
    }

    /// <summary>
    /// Di chuyển tới ô targered
    /// </summary>
    /// <param name="targetedCell"></param>
    public virtual void Move(Cell targetedCell)
    {
        //1. Di chuyển đơn thuần
        this.SetNewLocation(targetedCell);

        //2. Ăn

        BeUnSelected();
    }

    /// <summary>
    /// Chuyển ô hiện tại về trạng thái selected và tìm các ô targerted
    /// </summary>
    public abstract void BeSelected();

    /// <summary>
    /// Chuyển ô hiện tại về trạng thái selected và tìm các ô targerted
    /// </summary>
    public abstract void BeSelectedSameColor();

    /// <summary>
    /// Thực hiện tấn công vào quân cờ ở trong ô targetedCell
    /// </summary>
    /// <param name="targetedCell"></param>
    public virtual void Attack(Cell targetedCell)
    {
        //Với quân địch
        //- Đánh dấu là đã chết
        targetedCell._currentPiece.BeAttackedBy(this);

        //Với quân mình
        //- Di chuyển đến vị trí mới
        _currentCell.SetCellState(ECellState.NORMAL);
        this.SetNewLocation(targetedCell);
        BeUnSelected();

        //foreach (var item in ChessBoard.Current.blackLifeCells)
        //{
        //    item.targetedCells.Clear();
        //    item.piece.BeSelected();
        //    var list = new List<Cell>();
        //    foreach (var item2 in _targetedCells)
        //    {
        //        list.Add(item2);
        //    }
        //    item.SetTargetedCells(list);
        //    BeUnSelected();
        //}

        BaseGameCTL.Current.SwitchTurnAsync();
    }

    /// <summary>
    /// Thực hiện việc quân này bị ăn
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void BeAttackedBy(BasePiece enemy)
    {
        if (this.player == EPlayer.BLACK)
        {
            var d = -1;
            for (int i = 0; i < ChessBoard.Current.blackLifeCells.Count; i++)
            {
                if (ChessBoard.Current.blackLifeCells[i].piece.Info.Name == this.Info.Name)
                {
                    d = i;
                }
            }

            if (ChessBoard.Current.blackLifeCells[d].piece != null)
            {
                ChessBoard.Current.blackDefeateds.Add(ChessBoard.Current.blackLifeCells[d].piece);
                ChessBoard.Current.blackLifeCells.RemoveAt(d);
            }
        }
        else
        {
            var d = -1;
            for (int i = 0; i < ChessBoard.Current.whiteLifeCells.Count; i++)
            {
                if (ChessBoard.Current.whiteLifeCells[i].piece.Info.Name == this.Info.Name)
                {
                    d = i;
                }
            }

            if (ChessBoard.Current.whiteLifeCells[d].piece != null)
            {
                ChessBoard.Current.whiteDefeateds.Add(ChessBoard.Current.whiteLifeCells[d].piece);
                ChessBoard.Current.whiteLifeCells.RemoveAt(d);
            }
        }

        GameObject.Destroy(this.gameObject);
        _currentCell.SetPiece(null);
    }

    /// <summary>
    /// Loại bỏ trạng thái các quân cờ đang ở trạng thái selected và targerted
    /// </summary>
    public void BeUnSelected()
    {

        foreach (var item in _targetedCells)
        {
            item.SetCellState(ECellState.NORMAL);
        }
        _targetedCells.Clear();
    }
}
