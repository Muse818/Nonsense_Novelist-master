using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum settingUiType 
{
    Quality,Chara,
};
/// <summary>
/// 挂在设定Setting预制体上面
/// </summary>
public class Setting : MonoBehaviour
{
    private string resName = "UI/addsheding_";
    public Image uiTitle;
    [Header("品质概率")]
    [Header("平庸")] public int pingYong = 60;
    public GameObject py;
    [Header("巧思")] public int qiaoSi = 30;
    public GameObject qs;
    [Header("鬼才")] public int guiCai = 10;
    public GameObject gc;
    [Header("独特")] public int duTe = 10;
    public GameObject dt;

    public GameObject logo;
    private PointerEventData ed;
    private BaseEventData baseEventData;

    settingUiType typeMe= settingUiType.Quality;
    //是哪一边加入设定？左true右false
    bool isLeft = true;
    void Start()
    {

        if (!CharacterManager.instance.pause)
            CharacterManager.instance.pause = true;

    }

    public void InitSetting(settingUiType type,bool _isLeft)
    {
        isLeft = _isLeft;
        if (isLeft)
        {
            uiTitle.sprite = ResMgr.GetInstance().Load<Sprite>(resName + "A");
        }
        else
        {
            uiTitle.sprite = ResMgr.GetInstance().Load<Sprite>(resName + "B");

        }

        typeMe = type;
        switch (type)
        {
            case settingUiType.Chara:
                Character();
                break;
            case settingUiType.Quality:
                Quality();
                break;

        }
    }


    /// <summary>
    /// 随机出一个品质，并随机其中三个具体设定
    /// </summary>
    void Quality()
    {
        //this.transform.GetChild(3).GetComponentInChildren<Text>().text= "选择一个设定";
        this.transform.GetChild(3).GetComponent<Button>().interactable = false;
        //概率抽取                
        int numx = UnityEngine.Random.Range(1, 101);
        if (numx <= pingYong) { numx = 0; }
        else if (numx > pingYong && numx < pingYong + qiaoSi) numx = 1;
        else if (numx >= pingYong + qiaoSi && numx < pingYong + qiaoSi + guiCai) numx = 2;


        //删除
        if (this.transform.GetChild(1).childCount != 0)
        {
            for (int i = this.transform.GetChild(1).childCount - 1; i >= 0; i--)
            {
                Destroy(this.transform.GetChild(1).GetChild(i).gameObject);
            }
        }


        //从品质中抽取三个设定（设定写完之后再完成）
        switch (numx)//点击确定按钮的时候push对象池
        {
            case 0: //平庸
                for(int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(py, (a) => {
                        Type py0 = AllSkills.RandomPY();
                        var ppy= a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的平庸卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/pingyong/"+ppy.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ppy.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ppy.info;//描述
                                                                                                //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (obj) => { PointerClick(a); });                                                                                                                                             //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < ppy.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                        
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + ppy.lables[i]);
                            lg.GetComponent<Image>().SetNativeSize();
                            lg.transform.localScale = Vector3.one;
                        }
                    });
                }
                break;
            case 1:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(qs, (a) => {
                        Type qs0 = AllSkills.RandomQS();
                        var qss= a.AddComponent(qs0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的巧思卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/qiaosi/" + qss.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qss.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = qss.info;//描述
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (obj)=> { PointerClick(a); });                                                                                                                                             //生成标签

                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < qss.lables.Count; i++)//生成标签
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + qss.lables[i]);
                        }
                    });
                }
                break;
            case 2:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(gc, (a) => {
                        Type gc0 = AllSkills.RandomGC();
                        var gcc= a.AddComponent(gc0)as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的鬼才卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/guicai/" + gcc.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gcc.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gcc.info;//描述
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, ( data) => { PointerClick( a); });                                                                                                                                             //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < gcc.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + gcc.lables[i]);
                        }
                    });
                }
                break;
            case 3:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(dt, (a) => {
                        Type gc0 = AllSkills.RandomDT();
                        var gcc = a.AddComponent(gc0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的鬼才卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/dute/" + gcc.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gcc.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gcc.info;//描述
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (data) => { PointerClick(a); });                                                                                                                                             //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < gcc.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + gcc.lables[i]);
                        }
                    });
                }
                break;
        }
    }



    /// <summary>
    /// 出三个character标签的设定
    /// </summary>
    void Character()
    {
        //this.transform.GetChild(3).GetComponentInChildren<Text>().text = "选择一个设定";
        this.transform.GetChild(3).GetComponent<Button>().interactable = false;

        //删除
        if (this.transform.GetChild(1).childCount != 0)
        {
            for (int i = this.transform.GetChild(1).childCount - 1; i >= 0; i--)
            {
                Destroy(this.transform.GetChild(1).GetChild(i).gameObject);
            }
        }


        //概率抽取                
        int numx = UnityEngine.Random.Range(1, 101);
        if (numx <= pingYong) { numx = 0; }
        else if (numx > pingYong && numx < pingYong + qiaoSi) numx = 1;
        else if (numx >= pingYong + qiaoSi && numx < pingYong + qiaoSi + guiCai) numx = 2;
        else if (numx >= pingYong + qiaoSi+ guiCai && numx < pingYong + qiaoSi + guiCai+duTe) numx = 3;

        for (int x = 0; x < 3; x++)
        {
            //先抽取一张卡
            Type py0 = AllSkills.RandomChara();
            AbstractSetting _py0 = Activator.CreateInstance(py0) as AbstractSetting;
         
            //根据卡的品质生成
            switch (_py0.level)//点击确定按钮的时候push对象池
            {
                case SettingLevel.PingYong: //平庸
                    PoolMgr.GetInstance().GetObj(py, (a) =>
                    {                   
                        var ppy = a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的平庸卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/pingyong/" + ppy.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ppy.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ppy.info;//描述
                                                                                                //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (obj) => { PointerClick(a); });                                                                                                                                             //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < ppy.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));

                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + ppy.lables[i]);
                            lg.GetComponent<Image>().SetNativeSize();
                            lg.transform.localScale = Vector3.one;
                        }
                    });
                    break;
                    
                case SettingLevel.QiaoSi:
                   
                    PoolMgr.GetInstance().GetObj(qs, (a) =>
                    {
                        var qss = a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的巧思卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/qiaosi/" + qss.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qss.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = qss.info;//描述
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (obj) => { PointerClick(a); });                                                                                                                                             //生成标签

                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < qss.lables.Count; i++)//生成标签
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + qss.lables[i]);
                        }
                    });

                    break;
                case SettingLevel.GuiCai:
                   
                    PoolMgr.GetInstance().GetObj(gc, (a) =>
                    {
                        var gcc = a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的鬼才卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/guicai/" + gcc.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gcc.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gcc.info;//描述
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (data) => { PointerClick(a); });                                                                                                                                             //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < gcc.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + gcc.lables[i]);
                        }
                    });

                    break;
                case SettingLevel.DuTe:
                    PoolMgr.GetInstance().GetObj(dt, (a) => {
                           
                        var gcc = a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的鬼才卡牌内容
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/dute/" + gcc.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gcc.settingName;//标题
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gcc.info;//描述
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (data) => { PointerClick(a); });                                                                                                                                             //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < gcc.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + gcc.lables[i]);
                        }
                    });
                    break;

            }
        }

    }

    public void RefreshEvent()
    {
        InitSetting(typeMe,false);
    }




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
    private GameObject click = null;


    #region 外部点击事件
    /// <summary>
    /// 选择其中一个设定
    /// </summary>
    /// <param name="a">a是点击的物体（含有eventTrigger）</param>
    void PointerClick(GameObject a)
    {
        if (click != a)//click不是当前选中的物体
        {
            if (click != null)//click不是空
                click.GetComponent<Image>().color = Color.white;
            a.GetComponent<Image>().color = new Color(210/255f,200/255f,170/255f);
            click = a;
        }
        else
        {
            a.GetComponent<Image>().color = Color.white;
            click = null;
        }
        if (click == null)//click没选中物体
        {
            //this.transform.GetChild(3).GetComponentInChildren<Text>().text = "选择一个设定";
            this.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }
        else//click选中物体
        {
            //this.transform.GetChild(3).GetComponentInChildren<Text>().text = "确定";
            this.transform.GetChild(3).GetComponent<Button>().interactable = true;
        }
    }




    /// <summary>
    /// 点击选中按钮
    /// </summary>
    public void ClickCheck()
    {
        if (CharacterManager.instance.pause)
            CharacterManager.instance.pause = false;
        ///不知道是那一队加入设定欸，先乱写了噢
        if (isLeft)
        {

            GameMgr.instance.settingL.Add(click.GetComponent<AbstractSetting>().GetType());
            GameMgr.instance.settingPanel.RefreshList();
            Destroy(gameObject);
        }
        else
        {

            GameMgr.instance.settingR.Add(click.GetComponent<AbstractSetting>().GetType());
            GameMgr.instance.settingPanel.RefreshList();
            Destroy(gameObject);
        }
    }
    #endregion
}
