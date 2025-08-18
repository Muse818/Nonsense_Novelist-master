using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// 挂在牌库UI上
/// </summary>
public class ShooterWordCheck : MonoBehaviour
{

    [Header("牌库主面板(手动)")]
    public GameObject mainPanal;

    [Header("牌库主面板组件(手动)")]
    public Text textCount;
    public Transform wordsArea;



    [Header("牌库预制物(手动)")]
    public GameObject word_adj;
    public GameObject word_verb;
    public GameObject word_item;
    public Color color_hasUse;
    public Color color_GoingToUse;
    public bool jiaoYi = false;


    [Header("牌库主面板组件(手动)")]
    public Button btn_cancel;
    public Button btn_Check;
    public Button btn_exit;


    private Dictionary<AbstractWord0,int > UIHasUsedCard = new Dictionary<AbstractWord0, int>();
    private Dictionary<AbstractWord0, int> UINoUsedCard = new Dictionary<AbstractWord0, int>();

    #region 尝试

    /// <summary>
    ///
    /// </summary>
    /// <param name="eventTrigger">需要添加事件的EventTrigger</param>
    /// <param name="eventTriggerType">事件类型</param>
    /// <param name="callback">回调函数</param>
    void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
      
    }
    #endregion


    #region 1

    public void OpenMainPanal()
    {
        UIHasUsedCard.Clear();
        UINoUsedCard.Clear();

        mainPanal.SetActive(true); 
        if (!jiaoYi)
        {
   
            btn_Check.gameObject.SetActive(false);
            btn_cancel.gameObject.SetActive(false);
            btn_exit.gameObject.SetActive(true);
        }
        else
        {
            btn_Check.gameObject.SetActive(false);
            btn_cancel.gameObject.SetActive(true);
            btn_exit.gameObject.SetActive(false);
            GetComponent<Animator>().Play("CardRes_Up");
            chooseWord = null;
        }
    
        textCount.text = GameMgr.instance.GetGoingToUseList().Count.ToString()+"/"+GameMgr.instance.GetNowList().Count.ToString()+"/" + GameMgr.instance.GetAllList().Count.ToString();
        //生成未用过的
        var list3 = GameMgr.instance.GetGoingToUseList().OrderBy(it => it.Name).ToList();
        foreach (var _word in list3)
        {

            PoolMgr.GetInstance().GetObj(word_item, (obj) =>
            {
                var word = obj.AddComponent(_word) as AbstractWord0;
                obj.GetComponent<Image>().color = color_GoingToUse;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = word.wordName;
                obj.transform.parent = wordsArea;
                obj.transform.localScale = Vector3.one;
                obj.GetComponentInChildren<Image>().SetNativeSize();
                if (obj.TryGetComponent<SeeWordDetail>(out var _s))
                    _s.SetPic(word);
                if (jiaoYi)
                {
                    obj.GetComponent<Button>().interactable = false;
                    var et = obj.AddComponent<EventTrigger>();
                    AddPointerEvent(et, EventTriggerType.PointerClick, (obj) => { ClickThis(et.gameObject); });

                }
            });
        }
        var list= GameMgr.instance.GetNowList().OrderBy(it => it.Name).ToList();
        foreach (var _word in list)
        { 
            
            PoolMgr.GetInstance().GetObj(word_adj, (obj) =>
            {
                var word = obj.AddComponent(_word) as AbstractWord0;

                //if (UINoUsedCard.Contains(word))//如果已有，则将
                //{
                    
                //}

                obj.GetComponentInChildren<TextMeshProUGUI>().text = word.wordName;
                obj.transform.parent = wordsArea;
                obj.transform.localScale = Vector3.one;
                obj.GetComponentInChildren<Image>().SetNativeSize();

                if (obj.TryGetComponent<SeeWordDetail>(out var _s))
                    _s.SetPic(word);

                if (jiaoYi)
                {
                    obj.GetComponent<Button>().interactable = false;
                    var et = obj.AddComponent<EventTrigger>();
                    AddPointerEvent(et, EventTriggerType.PointerClick, (obj) => { ClickThis(et.gameObject); });

                }

            });
        }
        //生成已用过的
        var list2 = GameMgr.instance.GetHasUsedList().OrderBy(it => it.Name).ToList();
        foreach (var _word in list2)
        {
            
            PoolMgr.GetInstance().GetObj(word_item, (obj) =>
            {
                var word = obj.AddComponent(_word) as AbstractWord0;
                obj.GetComponent<Image>().color = color_hasUse;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = word.wordName;
                obj.transform.parent = wordsArea;
                obj.transform.localScale = Vector3.one;
                obj.GetComponentInChildren<Image>().SetNativeSize();
                if (obj.TryGetComponent<SeeWordDetail>(out var _s))
                    _s.SetPic(word);
                if (jiaoYi)
                {
                    obj.GetComponent<Button>().interactable = false;
                    var et = obj.AddComponent<EventTrigger>();
                    AddPointerEvent(et, EventTriggerType.PointerClick, (obj) => { ClickThis(et.gameObject); });

                }
            });
        }
       

    }

    void CloseMainPanal()
    {
        mainPanal.SetActive(false);

        for(int i= wordsArea.childCount-1; i>=0;i--)
        {
            PoolMgr.GetInstance().PushObj(wordsArea.GetChild(i).gameObject.name, wordsArea.GetChild(i).gameObject);
        }
    }

    [HideInInspector]public GameObject chooseWord=null;
    void ClickThis(GameObject _botton)
    {
        if (chooseWord != _botton)
        {
            if (chooseWord != null)
                chooseWord.GetComponent<Image>().color = Color.white;
            chooseWord = _botton;
            chooseWord.GetComponent<Image>().color = Color.grey;
        }

        else
        {
            chooseWord.GetComponent<Image>().color = Color.white;
            chooseWord = null;
        }



        if (chooseWord == null)
        {
            btn_Check.gameObject.SetActive(false);
        }

        else
        {
            
            btn_Check.gameObject.SetActive(true);
        }
        
    }    
    #endregion


    #region 外部点击事件

    /// <summary>
    /// 点击外部入口-发射器
    /// </summary>
    public void ClickShooterButton()
    {
        OpenMainPanal(); 
        CharacterManager.instance.pause = true;
    }

    public void ClickExitButton()
    {
        CloseMainPanal();
        CharacterManager.instance.pause = false;
    }
    public void ClickCancelButton()
    {
        GetComponent<Animator>().Play("CardRes_Down");
    }
    public void ClickCheckButton()
    {
        GetComponent<Animator>().Play("CardRes_Down");
        GameMgr.instance.DeleteCardList(chooseWord.GetComponent<AbstractWord0>());
        GameMgr.instance.AddCardList(this.transform.parent.GetComponent<EventUI>().JY_chooseWord.GetType());
        this.transform.parent.GetComponent<EventUI>().CloseAnim();
    }

    #endregion

    #region 动画事件
    public void Anim_Down()
    {
        CloseMainPanal();
    }
    #endregion
}
