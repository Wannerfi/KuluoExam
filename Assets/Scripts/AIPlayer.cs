using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIPlayer : Pawn
{
    public Status myStatus;
    private bool isfinished;

    private void Awake()
    {
        aiPip.Add(win1);
        aiPip.Add(win2);
        
        myStatus = Status.Cross;
        isfinished = false;
    }

    public delegate void PrintShowText(string str);
    public event PrintShowText ShowText;

    public override void Reset()
    {
        ShowText = null;
    }

    private List<string> ridicuteText = new List<string>()
    {
        "你会输的",
        "给我时间想想",
        "哈哈哈，你个辣鸡",
        "屑,啊哈",
        "要不送你悦刻五代抽抽",
        "别闹，小玩意儿"
    };
    
    public override void Begin()
    {
        isfinished = false;

        var str = ridicuteText[Random.Range(0, ridicuteText.Count)];
        ShowText.Invoke(str);
        float delay = Random.Range(.3f, 1.3f);
        Invoke("ChooseGrid", delay);
    }

    public override bool IsFinished()
    {
        return isfinished;
    }

    public override Status GetMyStatus()
    {
        return myStatus;
    }

    private List<Func<int>> aiPip = new List<Func<int>>();
    
    public void ChooseGrid()
    {       
        int index = -1;
        foreach (var p in aiPip)
        {
            index = p();
            if (index != -1)
            {
                ChooseGuid(index);
                break;
            }
        }
        isfinished = true;
    }

    public override void ChooseGuid(int index)
    {
        var chess = GameStatus.GetChess();
        chess.TryToSetGrids(index, myStatus);
    }

    private int win1()
    {
        var chess = GameStatus.GetChess();
        
        Func<Status, int> foo = (Status s) =>
        {
            for (int i = 0; i < 9; ++i)
            {
                if (!chess.IsSet(i) && chess.TestOver(i, s))
                {
                    return i;
                }
            }

            return -1;
        };

        // ai赢 - 不能让玩家赢
        int index = foo(myStatus);
        if (index > 0)
            return index;

        foreach (Status s in Enum.GetValues(typeof(Status)))
        {
            if (s != Status.None && s != myStatus)
                return foo(s);
        }

        return -1;
    }

    private int[] indexProp1 = new[] { 4, };
    private int[] indexProp2 = new[] { 0, 2, 8, 6 };
    private int[] indexProp3 = new[] { 1, 3, 5, 7 };
    private int win2()
    {
        var chess = GameStatus.GetChess();
        // 先中间，后四角
        foreach (int i in indexProp1)
        {
            if (!chess.IsSet(i))
            {
                return i;
            }
        }

        int random = Random.Range(0, 3);
        for (int i = 0;i < 3; ++i, ++random)
        {
            if (!chess.IsSet(random) && !chess.IsSet((random + 1) % indexProp2.Length))
                return random;
        }
        random = Random.Range(0, 3);
        for (int i = 0;i < 3; ++i, ++random)
            if (!chess.IsSet(random)) return random;

        random = Random.Range(0, 3);
        for (int i = 0;i < 3; ++i, ++random)
        {
            if (!chess.IsSet(random)) return random;
        }
        return -1;
    }
}
