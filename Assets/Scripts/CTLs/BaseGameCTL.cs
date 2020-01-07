using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BaseGameCTL : MonoBehaviour
{
    public static BaseGameCTL Current;

    public GameObject gameOverDialog;
    public GameObject textGameOver;

    private EGameState _gameState;

    public EPlayer CurrentPlayer { get; private set; }

    public EGameState GameState
    {
        get { return _gameState; }
        set { _gameState = value; }
    }


    void Awake()
    {
        Current = this;
        CurrentPlayer = EPlayer.WHITE;
        GameState = EGameState.PLAYING;
    }

    public async Task SwitchTurnAsync()
    {
        CurrentPlayer = CurrentPlayer == EPlayer.WHITE ? EPlayer.BLACK : EPlayer.WHITE;



        if (CurrentPlayer == EPlayer.BLACK)
        {
            await Task.Delay(500);
        }

        ChessBoard.Current.blackCells.Clear();
        ChessBoard.Current.whiteCells.Clear();

        var totalWhite = 0;
        var totalBlack = 0;

        int i;
        int j;

        var lifeLenght = ChessBoard.Current.blackLifeCells.Count;
        for (i = 0; i < lifeLenght; i++)
        {
            if (ChessBoard.Current.blackLifeCells[i].piece != null)
            {
                ChessBoard.Current.blackLifeCells[i].targetedCells.Clear();
                ChessBoard.Current.blackLifeCells[i].piece.BeSelected();
                var list = new List<Cell>();
                var cellCounts = ChessBoard.Current.blackLifeCells[i].piece._targetedCells.Count;
                for (j = 0; j < cellCounts; j++)
                {
                    if (ChessBoard.Current.blackLifeCells[i].piece._targetedCells[j]._currentPiece != null)
                    {
                        if (Math.Abs(ChessBoard.Current.blackLifeCells[i].piece._targetedCells[j]._currentPiece.Info.Score) >= totalBlack)
                        {
                            ChessBoard.Current.blackCells.Clear();
                            ChessBoard.Current.blackCells.Add(ChessBoard.Current.blackLifeCells[i].piece._currentCell);
                            ChessBoard.Current.blackCells.Add(ChessBoard.Current.blackLifeCells[i].piece._targetedCells[j]);
                        }
                    }


                    list.Add(ChessBoard.Current.blackLifeCells[i].piece._targetedCells[j]);
                }
                ChessBoard.Current.blackLifeCells[i].SetTargetedCells(list);
                ChessBoard.Current.blackLifeCells[i].piece.BeUnSelected();
            }
        }

        lifeLenght = ChessBoard.Current.whiteLifeCells.Count;
        for (i = 0; i < lifeLenght; i++)
        {
            if (ChessBoard.Current.whiteLifeCells[i].piece != null)
            {
                ChessBoard.Current.whiteLifeCells[i].targetedCells.Clear();
                ChessBoard.Current.whiteLifeCells[i].piece.BeSelected();
                var list = new List<Cell>();
                var cellCounts = ChessBoard.Current.whiteLifeCells[i].piece._targetedCells.Count;
                for (j = 0; j < cellCounts; j++)
                {
                    if (ChessBoard.Current.whiteLifeCells[i].piece._targetedCells[j]._currentPiece != null)
                    {
                        if (Math.Abs(ChessBoard.Current.whiteLifeCells[i].piece._targetedCells[j]._currentPiece.Info.Score) >= totalWhite)
                        {
                            ChessBoard.Current.whiteCells.Clear();
                            ChessBoard.Current.whiteCells.Add(ChessBoard.Current.whiteLifeCells[i].piece._currentCell);
                            ChessBoard.Current.whiteCells.Add(ChessBoard.Current.whiteLifeCells[i].piece._targetedCells[j]);
                        }
                    }

                    list.Add(ChessBoard.Current.whiteLifeCells[i].piece._targetedCells[j]);
                }
                ChessBoard.Current.whiteLifeCells[i].SetTargetedCells(list);
                ChessBoard.Current.whiteLifeCells[i].piece.BeUnSelected();
            }
        }

        for (i = 0; i < 8; i++)
        {
            for (j = 0; j < 8; j++)
            {
                ChessBoard.Current.arrayLocationCurrent[i, j] = ChessBoard.Current.arrayLocationFuture[i, j];
            }
        }

        if (CurrentPlayer == EPlayer.BLACK && GameState == EGameState.PLAYING)
        {
            ComputerMoveCTL.computer.MoveRandom();
        }
    }

    /// <summary>
    /// Kiểm tra trạng thái của bàn cờ đã GAMEOVER chưa
    /// </summary>
    public EGameState CheckGameState()
    {
        return EGameState.PLAYING;
    }

    public async void GameOver(EPlayer winPlayer)
    {
        Debug.Log("Win Player : " + winPlayer);
        await Task.Delay(1000);
        GameState = EGameState.GAME_OVER;
        gameOverDialog.SetActive(true);
        textGameOver.GetComponent<TextMeshProUGUI>().text = "Win Player : " + winPlayer;
    }

}
