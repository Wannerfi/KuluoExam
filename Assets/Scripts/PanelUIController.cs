using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUIController
{
    public Text log;
    public GameObject Begin;
    public Toggle playerInitiativeToggle;
    
    public void Init()
    {
        log = GameObject.Find("Canvas/Panel/Text").GetComponent<Text>();
        Begin = GameObject.Find("Canvas/Panel/Controller/Begin");
        playerInitiativeToggle = GameObject.Find("Canvas/Panel/Controller/Toggle").GetComponent<Toggle>();
    }

    public void Reset()
    {
        string showText = playerInitiativeToggle.isOn ? "你是先手" : "后手怎么赢啊";
        log.text = showText;
        Begin.GetComponent<Text>().text = "重新开始";
    }
}
