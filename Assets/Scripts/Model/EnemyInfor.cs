using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfor : MonoBehaviour
{
    public BasePiece piece { get; set; }
    public List<Cell> targetedCells { get; private set; }



    public EnemyInfor(BasePiece cellInfor, List<Cell> cells)
    {
        piece = cellInfor;
        SetTargetedCells(cells);
    }

    public EnemyInfor()
    {

    }

    public void SetTargetedCells(List<Cell> cells)
    {
        targetedCells = cells;
    }
}
