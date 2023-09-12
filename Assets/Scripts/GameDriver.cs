using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDriver : MonoBehaviour
{
    public Player Player;
    public AIPlayer AIPlayer;
    
    private int curPlayer = 0;
    private bool isPlaying;
    
    public Pawn GetCurPawn()
    {
        return GameStatus.Instance.PlayerList[curPlayer];
    }

    private PanelUIController panelController = new PanelUIController();

    private void Awake()
    {
        GameStatus.Instance.Init(this);
        panelController.Init();
    }

    void Start()
    {
        panelController.Begin.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameStatus.Instance.Reset(Player, AIPlayer, panelController.playerInitiativeToggle.isOn);
            panelController.Reset();
            
            curPlayer = 0;
            isPlaying = true;
            GetCurPawn().Begin();
        });
    }

    private void Update()
    {
        if (isPlaying && GetCurPawn().IsFinished())
        {
            if (GameStatus.IsGameOver())
            {
                isPlaying = false;
                ShowOver();
                return;
            }
            
            curPlayer = (curPlayer + 1) % GameStatus.Instance.PlayerList.Count;
            GetCurPawn().Begin();
        }
    }

    private void ShowOver()
    {
        panelController.log.text = "Game Over";
        panelController.log.color = Color.red;
        GameStatus.GetChess().ShowOverGridItem();
    }

    public void ShowText(string str)
    {
        panelController.log.text = str;
    }
}
