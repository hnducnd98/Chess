using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private ECellColor _color;
    private ECellState _state;
    private Transform cellSelectedObj;

    //Vị trị tọa độ trên bàn cờ 8x8
    public CLocation location { get; set; }
    public void SetLocation(CLocation location)
    {
        this.location = location;
    }

    public BasePiece _currentPiece { get; private set; }

    public ECellState State
    {
        get { return _state; }
        private set
        {
            _state = value;
            //Xử lý thay đổi trạng thái
            switch (_state)
            {
                case ECellState.NORMAL:
                    cellSelectedObj.gameObject.SetActive(false);

                    break;
                case ECellState.SELECTED:
                    cellSelectedObj.gameObject.SetActive(true);

                    _currentPiece.BeSelected();

                    break;
                case ECellState.TARGETED:
                    cellSelectedObj.gameObject.SetActive(true);

                    break;
                default:
                    cellSelectedObj.gameObject.SetActive(false);
                    break;
            }

        }
    }


    public ECellColor Color
    {
        get { return _color; }
        set
        {
            _color = value;

            switch (_color)
            {
                case ECellColor.BLACK:
                    GetComponent<Renderer>().material = ResourceCTL.Instance.BlackCellMaterial;
                    break;
                case ECellColor.WHITE:
                    GetComponent<Renderer>().material = ResourceCTL.Instance.WhiteCellMaterial;
                    break;
                default:
                    break;
            }

        }
    }

    public float SIZE
    {
        get
        {
            return GetComponent<Renderer>().bounds.size.x;
        }
    }

    void Awake()
    {
        cellSelectedObj = this.transform.GetChild(1);
    }

    protected void Start()
    {
        SetCellState(ECellState.NORMAL);
    }

    /// <summary>
    /// Chỉ có thể thay đổi đc cell state thông qua hàm SetCellState
    /// </summary>
    /// <param name="cellState"></param>
    public void SetCellState(ECellState cellState)
    {
        this.State = cellState;
    }

    /// <summary>
    /// Truyền vào null nếu như quân cờ di chuyển từ ô này đi ra
    /// </summary>
    /// <param name="piece"></param>
    public void SetPiece(BasePiece piece)
    {
        this._currentPiece = piece;
    }

    /// <summary>
    /// Thực hiện di chuyển quân cờ ở ô hiện tại
    /// </summary>
    public void MakeAMove(Cell targetedCell)
    {
        _currentPiece.Move(targetedCell);
        State = ECellState.NORMAL;
        _currentPiece = null;

        //foreach (var item in ChessBoard.Current.blackLifeCells)
        //{
        //    item.targetedCells.Clear();
        //    item.piece.BeSelected();
        //    var list = new List<Cell>();
        //    foreach (var item2 in item.piece._targetedCells)
        //    {
        //        list.Add(item2);
        //    }
        //    item.SetTargetedCells(list);
        //    item.piece.BeUnSelected();
        //}

        BaseGameCTL.Current.SwitchTurnAsync();
    }

}
