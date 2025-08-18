using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dice : MonoBehaviour
{
    [Header("骰子的父物体必须是挂有脚本的面板本体")]
    public TextMeshProUGUI textNumber;

    private void Awake()
    {
        textNumber = this.GetComponentInChildren<TextMeshProUGUI>();
        textNumber.text = GameMgr.instance.GetDice().ToString();
    }
    //在点击时，刷新这个页面。挂在骰子预制体上，放置于各界面预制体中

    public void ClickDice()
    {
        if (GameMgr.instance.GetDice() <= 0)
        {
            return;
        }

        GameMgr.instance.DeleteDice(1);
        textNumber.text = GameMgr.instance.GetDice().ToString();
        //刷新当前页面
        if (this.transform.parent.TryGetComponent<EventUI>(out var e))
        {
            e.RefreshEvent();
        }
        if (this.transform.parent.TryGetComponent<Setting>(out var a))
        {
            a.RefreshEvent();
        }
        
    }
}
