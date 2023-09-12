using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Status
{
    None,
    Cross,
    Round
}

public class ItemStatus : MonoBehaviour
{


    private static readonly Dictionary<Status, string> statusToImage = new Dictionary<Status, string>()
    {
        { Status.None , null},
        { Status.Cross , "Image/cross"},
        { Status.Round , "Image/round"},
    };


    public void ChangeStatus(Status s)
    {
        string imagePath = null;
        if (statusToImage.TryGetValue(s, out imagePath))
        {
            if (imagePath == null)
            {
                imageComp.sprite = null;
            }
            else
            {
                imageComp.sprite = Resources.Load<Sprite>(imagePath);
                button.enabled = false; // 选中后不准再选
            }
        }
    }
    
    private Status curStatus = Status.None;
    private Image imageComp;
    private Button button;

    public int number = 0; // 格子编号

    public void Reset()
    {
        curStatus = Status.None;
        ChangeStatus(Status.None);
        button.enabled = true;
    }

    private void Awake()
    {
        GameStatus.GetChess().AddGridItem(this); // 注册
        
        imageComp = GetComponent<Image>();
        imageComp.sprite = null;
        
        button = GetComponent<Button>();
    }

    void Start()
    {
        var gd = GameObject.Find("GameDriver").GetComponent<GameDriver>();
        
        button.onClick.AddListener(() =>
        {
            gd.GetCurPawn().ChooseGuid(number);
        });
    }

    ~ItemStatus()
    {
        button.onClick.RemoveAllListeners();
    }
}
