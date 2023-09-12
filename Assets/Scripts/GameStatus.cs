using System.Collections.Generic;
using UnityEngine;

public class GameStatus
{
    static Chess chess = new Chess();
    
    public static Chess GetChess()
    {
        return chess;
    }
    
    public static bool IsGameOver()
    {
        return chess.IsOver();
    }

    private static GameStatus gs = new GameStatus();
    public static GameStatus Instance
    {
        get { return gs; }
    }
    
    private List<Pawn> playerList = new List<Pawn>();
    public List<Pawn> PlayerList
    {
        get { return playerList; }
    }
    public void Reset(Pawn player, Pawn ai, bool playerInitiative)
    {
        chess.Reset();
        foreach (var p in playerList)
            p.Reset();

        playerList = new List<Pawn>();
        if (playerInitiative)
        {
            playerList.Add(player);
            playerList.Add(ai);
        }
        else
        {
            playerList.Add(ai);
            playerList.Add(player);
        }
        foreach (var p in playerList)
            p.Init();
    }

    private GameDriver gd;

    public void Init(GameDriver gameDriver)
    {
        gd = gameDriver;
    }

    public void ShowLog(string str)
    {
        gd.ShowText(str);
    }
}
