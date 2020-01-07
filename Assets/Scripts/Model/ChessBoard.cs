using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard Current;

    public GameObject cellPrefab;
    public LayerMask CellLayerMask = 0;
    private float cell_size = -1;
    public Cell currentSelectedCell = null;


    public List<Cell> blackCells = new List<Cell>();
    public List<Cell> whiteCells = new List<Cell>();

    public List<EnemyInfor> blackLifeCells = new List<EnemyInfor>();
    public List<EnemyInfor> whiteLifeCells = new List<EnemyInfor>();

    public List<BasePiece> whiteDefeateds = new List<BasePiece>();
    public List<BasePiece> blackDefeateds = new List<BasePiece>();

    public float CELL_SIZE
    {
        get
        {
            if (cell_size < 0)
            {
                cell_size = cellPrefab.GetComponent<Cell>().SIZE;
            }
            return cell_size;
        }
    }

    private Vector2 basePosition = Vector2.zero;
    private Cell[][] _cells;
    public Cell[][] Cells
    {
        get
        {
            return _cells;
        }
    }
    private List<BasePiece> pieces = new List<BasePiece>();

    void Awake()
    {
        Current = this;
    }

    void Start()
    {
        InitChessBoard();
        InitChessPieces();
    }

    void Update()
    {
        if (BaseGameCTL.Current.GameState == EGameState.PLAYING)
        {
            CheckUserInput();
        }
    }

    private void CheckUserInput()
    {
        //Đường truyền từ con trỏ chuột tới màn hình khi click
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Chỉ cho bên trắng đc người điều khiển
        if (BaseGameCTL.Current.CurrentPlayer == EPlayer.WHITE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 1000, CellLayerMask.value))
                {
                    Cell newCell = hit.collider.GetComponent<Cell>();

                    switch (newCell.State)
                    {
                        //Tức là chọn vào 1 ô bình thường => Quân đấy phải là quân của người đang chơi
                        //Thì ms có thể thực hiện đc 1 nc đi ms

                        case ECellState.NORMAL:
                            if (newCell._currentPiece != null && newCell._currentPiece.player == BaseGameCTL.Current.CurrentPlayer)
                            {
                                //Quân cờ đc chọn trc đó
                                if (currentSelectedCell != null)
                                {
                                    currentSelectedCell.SetCellState(ECellState.NORMAL);
                                    if (currentSelectedCell._currentPiece != null)
                                        currentSelectedCell._currentPiece.BeUnSelected();
                                }
                                currentSelectedCell = newCell;
                                currentSelectedCell.SetCellState(ECellState.SELECTED);
                            }
                            break;
                        case ECellState.TARGETED:
                            //1. Nếu trống sẽ di chuyển bình thường
                            //2. Nếu k trống mà là quân địch thì sẽ ăn
                            //3. Các trường hợp còn lại sẽ k làm j
                            //Đây là ô TARGETED , di chuyển quân cờ hiện tại sang vị trí này
                            if (newCell._currentPiece == null)
                            {
                                //Di chuyển bình thường
                                currentSelectedCell.MakeAMove(newCell);
                            }
                            else
                            {
                                if (newCell._currentPiece.player != BaseGameCTL.Current.CurrentPlayer)
                                {
                                    //Ăn
                                    currentSelectedCell._currentPiece.Attack(newCell);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }

    //Check vị trí con trỏ chuột


    //Bắt lấy ô hiện tại


    //Khi không có thì thôi


    /// <summary>
    /// Khởi tạo bàn cờ
    /// </summary>
    [ContextMenu("InitChessBoard")]
    public void InitChessBoard()
    {
        //basePosition = Vector2.zero + new Vector2(7 * cell_size, 7 * cell_size);
        basePosition = Vector2.zero + new Vector2(-7, -7);
        _cells = new Cell[8][];
        for (int i = 0; i < 8; i++)
        {
            _cells[i] = new Cell[8];
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject c = GameObject.Instantiate(cellPrefab, CanculatePosition(i, j),
                    Quaternion.identity) as GameObject;
                c.transform.parent = this.transform.GetChild(0);
                _cells[i][j] = c.GetComponent<Cell>();
                _cells[i][j].gameObject.SetActive(true);
                _cells[i][j].SetLocation(new CLocation(i, j));
                if ((i + j) % 2 == 0)
                {
                    _cells[i][j].Color = ECellColor.WHITE;
                }
                else
                {
                    _cells[i][j].Color = ECellColor.BLACK;
                }
            }
        }
    }

    public float[,] arrayLocationCurrent = new float[8, 8];
    public float[,] arrayLocationFuture = new float[8, 8];
    public static List<Cell> list = new List<Cell>();
    public int indexQueenWhite = 2;
    public int indexQueenBlack = 2;

    [ContextMenu("InitChessPieces")]
    public void InitChessPieces()
    {
        List<PieceInfor> listInfo = new List<PieceInfor>();

        //White
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_1", Path = "Pieces/W_PAWN", X = 0, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_2", Path = "Pieces/W_PAWN", X = 1, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_3", Path = "Pieces/W_PAWN", X = 2, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_4", Path = "Pieces/W_PAWN", X = 3, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_5", Path = "Pieces/W_PAWN", X = 4, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_6", Path = "Pieces/W_PAWN", X = 5, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_7", Path = "Pieces/W_PAWN", X = 6, Y = 1, Score = 10 });
        listInfo.Add(new PieceInfor() { Name = "W_PAWN_8", Path = "Pieces/W_PAWN", X = 7, Y = 1, Score = 10 });

        listInfo.Add(new PieceInfor() { Name = "W_CASTLE_1", Path = "Pieces/W_CASTLE", X = 0, Y = 0, Score = 50 });
        listInfo.Add(new PieceInfor() { Name = "W_CASTLE_2", Path = "Pieces/W_CASTLE", X = 7, Y = 0, Score = 50 });
        listInfo.Add(new PieceInfor() { Name = "W_KNIGHT_1", Path = "Pieces/W_KNIGHT", X = 1, Y = 0, Score = 30 });
        listInfo.Add(new PieceInfor() { Name = "W_KNIGHT_2", Path = "Pieces/W_KNIGHT", X = 6, Y = 0, Score = 30 });
        listInfo.Add(new PieceInfor() { Name = "W_BISHOP_1", Path = "Pieces/W_BISHOP", X = 2, Y = 0, Score = 30 });
        listInfo.Add(new PieceInfor() { Name = "W_BISHOP_2", Path = "Pieces/W_BISHOP", X = 5, Y = 0, Score = 30 });
        listInfo.Add(new PieceInfor() { Name = "W_QUEEN_1", Path = "Pieces/W_QUEEN", X = 3, Y = 0, Score = 80 });
        listInfo.Add(new PieceInfor() { Name = "W_KING_1", Path = "Pieces/W_KING", X = 4, Y = 0, Score = 9000 });

        //Black
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_1", Path = "Pieces/B_PAWN", X = 0, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_2", Path = "Pieces/B_PAWN", X = 1, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_3", Path = "Pieces/B_PAWN", X = 2, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_4", Path = "Pieces/B_PAWN", X = 3, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_5", Path = "Pieces/B_PAWN", X = 4, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_6", Path = "Pieces/B_PAWN", X = 5, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_7", Path = "Pieces/B_PAWN", X = 6, Y = 6, Score = -10 });
        listInfo.Add(new PieceInfor() { Name = "B_PAWN_8", Path = "Pieces/B_PAWN", X = 7, Y = 6, Score = -10 });

        listInfo.Add(new PieceInfor() { Name = "B_CASTLE_1", Path = "Pieces/B_CASTLE", X = 0, Y = 7, Score = -50 });
        listInfo.Add(new PieceInfor() { Name = "B_CASTLE_2", Path = "Pieces/B_CASTLE", X = 7, Y = 7, Score = -50 });
        listInfo.Add(new PieceInfor() { Name = "B_KNIGHT_1", Path = "Pieces/B_KNIGHT", X = 1, Y = 7, Score = -30 });
        listInfo.Add(new PieceInfor() { Name = "B_KNIGHT_2", Path = "Pieces/B_KNIGHT", X = 6, Y = 7, Score = -30 });
        listInfo.Add(new PieceInfor() { Name = "B_BISHOP_1", Path = "Pieces/B_BISHOP", X = 2, Y = 7, Score = -30 });
        listInfo.Add(new PieceInfor() { Name = "B_BISHOP_2", Path = "Pieces/B_BISHOP", X = 5, Y = 7, Score = -30 });
        listInfo.Add(new PieceInfor() { Name = "B_QUEEN_1", Path = "Pieces/B_QUEEN", X = 3, Y = 7, Score = -80 });
        listInfo.Add(new PieceInfor() { Name = "B_KING_1", Path = "Pieces/B_KING", X = 4, Y = 7, Score = -9000 });

        whiteLifeCells.Clear();
        blackLifeCells.Clear();
        blackCells.Clear();
        whiteCells.Clear();

        var totalWhite = 0;
        var totalBlack = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                this.arrayLocationCurrent[i, j] = 0;
            }
        }

        foreach (var item in listInfo)
        {
            GameObject GO = GameObject.Instantiate<GameObject>(ResourceCTL.Instance.GetGameObject(item.Path));
            GO.transform.parent = this.transform.GetChild(1);
            GO.name = item.Name;
            BasePiece p = GO.GetComponent<BasePiece>();
            p.setInfor(item, _cells[item.X][item.Y]);
            pieces.Add(p);

            this.arrayLocationCurrent[item.X, item.Y] = item.Score;

            //if (Cells[item.X][item.Y]._currentPiece.player == EPlayer.BLACK)
            //{
            //    Cells[item.X][item.Y]._currentPiece.BeSelected();
            //}
            //else
            //{
            //    Cells[item.X][item.Y]._currentPiece.BeSelected();
            //}


            Cells[item.X][item.Y]._currentPiece.BeSelected();

            foreach (var item2 in Cells[item.X][item.Y]._currentPiece._targetedCells)
            {
                if (item2._currentPiece != null)
                {
                    if (item2._currentPiece.player == EPlayer.BLACK)
                    {
                        if (Math.Abs(item2._currentPiece.Info.Score) >= totalBlack)
                        {
                            blackCells.Clear();
                            blackCells.Add(Cells[item.X][item.Y]);
                            blackCells.Add(item2);
                        }
                    }
                    else
                    {
                        if (Math.Abs(item2._currentPiece.Info.Score) >= totalWhite)
                        {
                            whiteCells.Clear();
                            whiteCells.Add(Cells[item.X][item.Y]);
                            whiteCells.Add(item2);
                        }
                    }
                }
                list.Add(item2);
            }
            var pieceInfor = new EnemyInfor(Cells[item.X][item.Y]._currentPiece, list);

            if (item.Score > 0)
            {
                whiteLifeCells.Add(pieceInfor);
            }
            else
            {
                blackLifeCells.Add(pieceInfor);
            }

            Cells[item.X][item.Y]._currentPiece.BeUnSelected();
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                this.arrayLocationFuture[i, j] = this.arrayLocationCurrent[i, j];
            }
        }
    }

    public Vector2 CanculatePosition(int i, int j)
    {
        var kt = basePosition + new Vector2(i * CELL_SIZE, j * CELL_SIZE);
        return kt;
    }
}
