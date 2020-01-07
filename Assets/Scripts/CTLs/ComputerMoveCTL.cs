using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMoveCTL : MonoBehaviour
{
    public static ComputerMoveCTL computer;

    void Awake()
    {
        computer = this;
    }

    public void MoveRandom()
    {

        var cellsSelected = GetCellSelected();

        if (cellsSelected[0]._currentPiece != null)
        {
            cellsSelected[0].SetCellState(ECellState.SELECTED);
        }

        //Giống phần ăn chung của quân trắng
        //var i = Random.Range(0, ChessBoard.Current.Cells[x][y]._currentPiece._targetedCells.Count);

        if (cellsSelected[1]._currentPiece == null)
        {
            //Di chuyển bình thường
            cellsSelected[0].MakeAMove(cellsSelected[1]);
        }
        else
        {
            if (cellsSelected[1]._currentPiece.player != BaseGameCTL.Current.CurrentPlayer)
            {
                //Ăn
                cellsSelected[0]._currentPiece.Attack(cellsSelected[1]);
            }
        }
    }

    public List<BasePiece> listPieces { get; private set; }
    public float min { get; private set; }

    public void setMin(float min2)
    {
        min = min2;
    }

    public void SetCells(List<BasePiece> list)
    {
        listPieces = list;
    }

    public List<Cell> GetCellSelected()
    {
        var side = -1;
        var runCount = 0;
        var max = -9999.0;
        setMin(9999);
        var total = 0;
        var cells = new List<Cell>();
        var listCells = new List<Cell>();
        var listNew = new List<BasePiece>();
        foreach (var item in ChessBoard.Current.blackLifeCells)
        {
            var x = item.piece.location.X;
            var y = item.piece.location.Y;
            x = 10;
            y = 12;
            total = 0;
            foreach (var item2 in item.targetedCells)
            {
                listNew.Clear();
                listCells.Clear();

                listCells.Add(item.piece._currentCell);
                listCells.Add(item2);

                listNew.Add(item.piece);

                if (item2._currentPiece != null)
                {
                    total = item2._currentPiece.Info.Score;
                    listNew.Add(item2._currentPiece);
                }
                else
                {
                    total = 0;
                    listNew.Add(null);
                }
                SetCells(listNew);

                ChessBoard.Current.arrayLocationFuture[item.piece._currentCell.location.X, item.piece._currentCell.location.Y] = 0;
                ChessBoard.Current.arrayLocationFuture[item2.location.X, item2.location.Y] = item.piece.Info.Score;

                item.piece._currentCell.SetPiece(null);
                item2.SetPiece(item.piece);

                setMin(9999);
                var scorTotal = total;
                float scorMin = 0;

                foreach (var item3 in ChessBoard.Current.whiteLifeCells)
                {
                    if (item3.piece != null)
                    {
                        if (listNew[1] != null && listNew[1] == item3.piece)
                        {
                            continue;
                        }
                        item3.targetedCells.Clear();
                        item3.piece.BeSelected();
                        foreach (var item4 in item3.piece._targetedCells)
                        {
                            if (item4._currentPiece != null)
                            {
                                scorMin = item4._currentPiece.Info.Score;
                            }
                            else
                            {
                                scorMin = 0;
                            }
                            if (scorMin < min)
                            {
                                setMin(scorMin);
                            }
                        }
                        item3.piece.BeUnSelected();
                    }
                }

                total = scorTotal;

                var diem = min + total;

                ChessBoard.Current.arrayLocationFuture[item.piece._currentCell.location.X, item.piece._currentCell.location.Y] = listPieces[0].Info.Score;
                if (listPieces[1] == null)
                {
                    ChessBoard.Current.arrayLocationFuture[item2.location.X, item2.location.Y] = 0;
                }
                else
                {
                    ChessBoard.Current.arrayLocationFuture[item2.location.X, item2.location.Y] = listPieces[1].Info.Score;
                }

                item.piece._currentCell.SetPiece(listPieces[0]);
                item2.SetPiece(listPieces[1]);

                if (diem > max)
                {
                    max = diem;
                    cells.Clear();
                    cells.Add(listCells[0]);
                    cells.Add(listCells[1]);
                }
            }
        }

        if (cells[1]._currentPiece != null)
        {
            Debug.Log(cells[1]._currentPiece);
        }


        return cells;
    }


    public Hashtable stackWhite { get; set; }
    public Hashtable stackBlack { get; set; }

    public float LoopDeepDown(int side, int runCount, float score)
    {
        if (runCount < 4)
        {
            if (side == -1)
            {
                foreach (var item in ChessBoard.Current.blackLifeCells)
                {
                    if (!stackBlack.ContainsKey(item.piece.Info.Name))
                    {
                        item.piece.BeSelected();
                        var x = item.piece.location.X;
                        var y = item.piece.location.Y;
                        foreach (var item2 in item.targetedCells)
                        {
                            item.piece.location = item2.location;
                            if (item2._currentPiece != null)
                            {
                                stackWhite.Add(item2._currentPiece.Info.Name, item2._currentPiece);
                                score += LoopDeepDown(side * -1, runCount++, score + item2._currentPiece.Info.Score);
                                
                            }
                            else
                            {
                                score += LoopDeepDown(side * -1, runCount++, score + 0);
                            }
                        }
                    }
                }
            }
            else
            {

            }
        }
        else
        {
            return 0;
        }
        return -1;
    }



    //public bool CheckConditionRandom(int x, int y)
    //{
    //    if (ChessBoard.Current.Cells[x][y]._currentPiece != null)
    //    {
    //        if (ChessBoard.Current.Cells[x][y]._currentPiece.player == EPlayer.BLACK)
    //        {
    //            ChessBoard.Current.Cells[x][y]._currentPiece.BeSelected();
    //            if (ChessBoard.Current.Cells[x][y]._currentPiece._targetedCells.Count > 0)
    //            {
    //                ChessBoard.Current.Cells[x][y]._currentPiece.BeUnSelected();
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}
}
