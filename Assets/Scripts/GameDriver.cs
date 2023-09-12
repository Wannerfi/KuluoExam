using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDriver : MonoBehaviour
{
    public Player Player;
    public AIPlayer AIPlayer;
    private List<Pawn> playerList = new List<Pawn>();
    
    private int curPlayer = 0;
    private bool isPlaying;
    
    public Pawn GetCurPawn()
    {
        return playerList[curPlayer];
    }

    private Text gameText;
    private GameObject Begin;
    
    void Start()
    {
        gameText = GameObject.Find("Canvas/Panel/Text").GetComponent<Text>();
        Begin = GameObject.Find("Canvas/Panel/Controller/Begin");
        var beginBtn = Begin.GetComponent<Button>();
        beginBtn.onClick.AddListener(ResetGame);
    }

    void ResetGame()
    {
        GameStatus.GetChess().Reset();
        foreach (var p in playerList)
        {
            p.Reset();
        }
        
        curPlayer = 0;
        playerList = new List<Pawn>();
        string showText = null;
        if (GameObject.Find("Canvas/Panel/Controller/Toggle").GetComponent<Toggle>().isOn)
        {
            playerList.Add(Player);
            playerList.Add(AIPlayer);
            showText = "你是先手";
        }
        else
        {
            playerList.Add(AIPlayer);
            playerList.Add(Player);
            showText = "后手怎么赢啊";
        }

        AIPlayer.ShowText += ShowText;
        gameText.text = showText;
        Begin.GetComponent<Text>().text = "重新开始";
        isPlaying = true;
        playerList[curPlayer].Begin();
    }

    private void Update()
    {
        if (isPlaying && playerList[curPlayer].IsFinished())
        {
            if (GameStatus.IsGameOver())
            {
                isPlaying = false;
                ShowOver();
                return;
            }
            
            curPlayer = (curPlayer + 1) % playerList.Count;
            playerList[curPlayer].Begin();
        }
    }

    private void ShowOver()
    {
        gameText.text = "Game Over";
        gameText.color = Color.red;
    }

    private void ShowText(string str)
    {
        gameText.text = str;
    }
}
