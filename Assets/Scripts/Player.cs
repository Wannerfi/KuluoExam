using UnityEngine;
using UnityEngine.UI;

public class Player : Pawn
{
    public Status myStatus; // 玩家默认是 o
    private bool isfinished;

    private Image maskImage;
    
    private void Awake()
    {
        myStatus = Status.Round;
        maskImage = GameObject.Find("Canvas/mask").GetComponent<Image>();
    }

    public override void Begin()
    {
        isfinished = false;
        maskImage.raycastTarget = false;
    }
    
    public override bool IsFinished()
    {
        return isfinished;
    }
    
    public override Status GetMyStatus()
    {
        return myStatus;
    }
    
    public override void ChooseGuid(int index)
    {
        var chess = GameStatus.GetChess();
        chess.TryToSetGrids(index, myStatus);
        
        maskImage.raycastTarget = true;
        isfinished = true;
    }
}
