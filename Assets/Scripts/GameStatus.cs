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
}
