using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

 using UnityEngine.Events;
/// <summary>
/// 事件的类型
/// </summary>
[System.Serializable]
public enum EventType
{
    XiWang = 0,
    FangKe = 1,
    YiWai = 2,
    WeiJi = 3,
    JiaoYi = 4,
    ChangJing=5
}

public class EventUI : MonoBehaviour
{
    [Header("这是哪一个界面的UI")]
    public EventType type;
    bool isKey=false;
    private AudioPlay audioPlay;
    [HideInInspector]public Vector3 eventWorldPos = Vector3.one;

    //不重要数据
    List<eventExcelItem> needAllDate_nKey = new List<eventExcelItem>();
    List<eventExcelItem> needNowDate_nKey = new List<eventExcelItem>();
    //重要数据
    List<eventExcelItem> needAllDate_Key = new List<eventExcelItem>();
    List<eventExcelItem> needNowDate_Key = new List<eventExcelItem>();
    //函数中使用的数据
    List<eventExcelItem> tempAllDate = new List<eventExcelItem>();
    List<eventExcelItem> tempNowDate = new List<eventExcelItem>();


    [Header("牌库预制物(手动)")]
    public GameObject word_adj;
    public GameObject word_verb;
    public GameObject word_item;
    private AudioSource audioSource;
    private float volume = 0.4f;



    private void Awake()
    {
        triggerName = -1;
        KeyCharacter = -1;
        WJ_static = false;
        WJ_monster = -1;
        audioPlay = GameObject.Find("AudioSource").GetComponent<AudioPlay>();
    }
    private void Start()
    {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }
    #region 处理data 

    /// <summary>
    /// 在初始化后,处理isKEY和数据
    /// </summary>
    /// <param name="_isKey"></param>
    void DataInit(bool _isKey)
    {
        tempAllDate=null;
        tempNowDate=null;
        
        if (_isKey && (needAllDate_Key.Count != 0))
        {
            
            tempAllDate = needAllDate_Key;
            tempNowDate = needNowDate_Key;
        }
        else
        {
      
            tempAllDate = needAllDate_nKey;
            tempNowDate = needNowDate_nKey;
        }
    }

    /// <summary>
    /// 在面板最初打开的时候初始化
    /// </summary>
    /// <param name="_type"></param>
    void DealWithData(EventType _type)
    {
        //清空所有
        needAllDate_nKey.Clear();
        needAllDate_Key.Clear();
        needNowDate_nKey.Clear();
        needNowDate_Key.Clear();

        //获取该类型的当前所有可以发生的data
        foreach (var _t in GameMgr.instance.canHappenData_Key)
        {
            if (_t.type == _type)
            {
                if(!needAllDate_Key.Contains(_t))
                    needAllDate_Key.Add(_t);
                
                
            }
        }
        foreach (var _t in GameMgr.instance.canHappenData_nKey)
        {
            if (_t.type == _type)
            {
                if (!needAllDate_nKey.Contains(_t))
                    needAllDate_nKey.Add(_t);

            }
        }
       
        needNowDate_nKey.AddRange(needAllDate_nKey);
        needNowDate_Key.AddRange(needAllDate_Key);
    }


    
    void RefreshNowList()
    {
        if (needNowDate_nKey.Count <= 0)
        {
            needNowDate_nKey.AddRange(needAllDate_nKey);
        }
        if (needNowDate_Key.Count <= 0)
        {
            needNowDate_Key.AddRange(needAllDate_Key);
        }
    }


    #endregion


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
        Debug.Log("AddPointerEnterEvent");
    }
    #endregion


    //当前正在处理的nowEvent
    private eventExcelItem nowEvent;
    private eventExcelItem[] event_YW=new eventExcelItem[3];



    IEnumerator TypeDelay(string text, TextMeshProUGUI textUI, float delay)
    {
        int _index = 0;
        textUI.text= "";
        while (_index< text.Length)
        {
            textUI.text += text[_index];
            _index++;
            yield return new WaitForSeconds(delay);
        }
    }


    #region 五种类型的事件

    #region 意外
    /// <summary>
    /// 打开意外面板时的初始刷新
    /// </summary>
    public void OpenInit_YiWai()
    {
        //根据触发事件的信息，处理筛选所有的文本信息
        DataInit(false);

        //获取组件
        checkBotton = this.transform.Find("CheckButton").GetComponent<Button>();
        checkBotton.gameObject.SetActive(false);
        Transform cardParent = this.transform.Find("CardGroup");
        TextMeshProUGUI[] text = cardParent.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < 9; i += 3)
        {
            var et = cardParent.GetChild(i / 3).GetComponent<EventTrigger>();
            AddPointerEvent(et, EventTriggerType.PointerClick, (obj) => { Click_YW(et.gameObject); });

            //抽取满足条件的事件文本
            int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
            print("最大：" + tempNowDate.Count);
            int loopCount = 0;
            while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
            {
                _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
                if (loopCount > 50)
                {
                    print("死循环");return;
                }
            }

            //切换UI文字内容
            text[i].text = tempNowDate[_r].name;
            text[i + 1].text = tempNowDate[_r].textEvent;
            text[i + 2].text = tempNowDate[_r].textEfc;
            event_YW[i/3] = tempNowDate[_r];

            //刷新已用事件列表（不重复抽取）
            tempNowDate.Remove(tempNowDate[_r]);
            RefreshNowList();
        }
    }


    private GameObject nowChosen_YW = null;
    public void Click_YW(GameObject obj)
    {
        if (nowChosen_YW != obj)
        {
            if (nowChosen_YW != null)
                nowChosen_YW.GetComponent<Image>().color = Color.white;
            nowChosen_YW = obj;
            nowChosen_YW.GetComponent<Image>().color = Color.grey;
            nowEvent = event_YW[obj.transform.GetSiblingIndex()];
        }

        else
        {
            nowChosen_YW.GetComponent<Image>().color = Color.white;
            nowChosen_YW = null;
            nowEvent = null; 
        }



        if (nowChosen_YW == null)
        {
            checkBotton.gameObject.SetActive(false);
        }

        else
        {
            checkBotton.gameObject.SetActive(true);
        }
    }
    public void Close_YiWai()
    {
       
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
        
    }


    #endregion


    #region 访客
    Animator tempAnimator;
    WaitForFixedUpdate tempAnim= new WaitForFixedUpdate();
    [HideInInspector]public int KeyCharacter = -1;
    [HideInInspector]public int triggerName = -1;
    public void OpenInit_FangKe()
    {
        
        DataInit(isKey); 
        KeyCharacter = -1;
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") &&
            (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {
            //如果有条件并且条件没满足,就重找一个
            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("死循环");
                return;
            }
        }
       
        //
        Image sprite = this.transform.Find("Sprite").GetComponent<Image>();
        Image bubble= this.transform.Find("bubble").GetComponent<Image>();
        TextMeshProUGUI words = bubble.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();

        sprite.GetComponent<Animator>().SetBool("0", true);
        if(triggerName != -1) sprite.GetComponent<Animator>().SetBool(triggerName.ToString(), false);
        tempAnimator = sprite.GetComponent<Animator>();
        StartCoroutine(FangKeAnimation());//将动画机清零


        if (triggerName == -1)
        {
            if (KeyCharacter != -1)
            {

                triggerName = KeyCharacter;//本身就是ID 
            }
            else
            {
                triggerName = GameMgr.instance.GetNextCreateChara();//数组，非ID
                triggerName += 1;
            }
        }
        else
        {
            GameMgr.instance.GetNextCreateChara(triggerName-1);
        }

        print("triggerName" + triggerName.ToString()); ;
        sprite.GetComponent<Animator>().SetBool(triggerName.ToString(),true);
        //sprite.SetNativeSize();


        if ((tempNowDate[_r].happen != null) && (tempNowDate[_r].happen != ""))
            KeyCharacter = int.Parse(tempNowDate[_r].happen);
        nowEvent = tempNowDate[_r];
       
        //info.text = tempNowDate[_r].name;
        StartCoroutine(TypeDelay((tempNowDate[_r].name ), info, 0.05f));
       // words.text = tempNowDate[_r].textEvent;
        StartCoroutine(TypeDelay((tempNowDate[_r].textEvent), words, 0.05f));

        RefreshNowList();
    }

    IEnumerator FangKeAnimation()
    {
        int loopMax =0;
        while ((!tempAnimator.GetBool("0"))&&(loopMax<25))
        {
            //print("loopMax" + loopMax);
            loopMax++;
            yield return tempAnim;
        }
        tempAnimator.SetBool("0", false);
        if(triggerName!=-1)
        tempAnimator.SetBool(triggerName.ToString(), true);
    }
    void Close_FangKe()
    {
        if (isKey && KeyCharacter != -1)
        { 
            GameMgr.instance.CreateTheCharacterPut(KeyCharacter);
        }
        else
        {
            GameMgr.instance.CreateCharacterPut(1);
        }   
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion


    #region 希望
    Button checkBotton;
    private List<int> showBook=new List<int>();

    public void OpenInit_XiWang()
    {
        DataInit(isKey);

        //随机抽书
        showBook.Clear();
        Transform cardParent = this.transform.Find("CardGroup");
        checkBotton = this.transform.Find("CheckButton").GetComponent<Button>();
        checkBotton.gameObject.SetActive(false);

        for (int tt = 0;tt < 3; tt++)
        {
            //删除
            if (cardParent.GetChild(tt).GetChild(0).childCount != 0)
            {
                for (int i = cardParent.GetChild(tt).GetChild(0).childCount-1; i >= 0; i--)
                {
                    Destroy(cardParent.GetChild(tt).GetChild(0).GetChild(i).gameObject);
                }
            }
        }
       


        for (int i = 0; i < 3; i++)
        {
            //随机抽书
            int _lC = 0;
            int _rb = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
            while (_lC < 50 && showBook.Contains(_rb))
            {
                _rb = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
                _lC++;
                if (_lC > 40) print("死循环");
            }
            //如果是第一本书，有一半概率是剧本的书
            if (i == 0)
            {
                int _rbbb = UnityEngine.Random.Range(0, 2);
                if (_rbbb == 1) _rb = (int)(GameMgr.instance.nowBook);
            }

            showBook.Add(_rb);


            //显示书本图画
            var book = (BookNameEnum)int.Parse(_rb.ToString());
            cardParent.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book.ToString());
            
            if (i == 1) ClickWord_XW(cardParent.GetChild(i).gameObject);

            //随机抽取词组
            List<Type> _typeList = GameMgr.instance.GetBookList(book);
            _typeList.AddRange(GameMgr.instance.GetBookList(BookNameEnum.allBooks));
            List<Type> _typeListRare = new List<Type>();
            for (int j = 0; j < 3; j++)
            {
                //随机抽取级别
                int _rb1 = UnityEngine.Random.Range(0, 100);
                if (_rb1 < GameMgr.instance.cardRate_1)//稀有度1
                {
                    _typeListRare = AllSkills.Rare_1;
                }
                else if (_rb1 < GameMgr.instance.cardRate_2)//稀有度2
                {
                    _typeListRare = AllSkills.Rare_2;
                }
                else if (_rb1 < GameMgr.instance.cardRate_3)//稀有度3
                {
                    _typeListRare = AllSkills.Rare_3;
                }
                else if (_rb1 < GameMgr.instance.cardRate_4)//稀有度4
                {
                    _typeListRare = AllSkills.Rare_4;
                }

                //在书本和通用随机抽词语
                int _rbb = UnityEngine.Random.Range(0, _typeList.Count);
                int _loopCount = 0;
                while ((!_typeListRare.Contains(_typeList[_rbb])) && _loopCount < 50)
                {
                    _rbb = UnityEngine.Random.Range(0, _typeList.Count);
                    _loopCount++;
                }

                PoolMgr.GetInstance().GetObj(word_adj, (obj) =>
                {
                    var _word = obj.AddComponent(_typeList[_rbb]) as AbstractWord0;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                    
                    obj.transform.parent = cardParent.GetChild(i).GetChild(0);
                    obj.transform.localScale = new Vector3(1.7f,1.7f,1.7f);
                    obj.GetComponent<Button>().interactable = false;

             
                    if (obj.TryGetComponent<SeeWordDetail>(out var _s))
                        _s.SetPic(_word);

                });
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
        }

        //在当前的所有数据中随机抽一个
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);

        _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        TextMeshProUGUI info = this.transform.Find("info").GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = this.transform.Find("title").GetComponentInChildren<TextMeshProUGUI>();
        title .text = tempNowDate[_r].name;
        //打字机效果
        StartCoroutine(TypeDelay((tempNowDate[_r].textEvent), info, 0.05f));
        nowEvent = tempNowDate[_r];

        RefreshNowList();

    }

    private GameObject chooseBook =null;
    public void ClickWord_XW(GameObject _botton)
    {
        if (chooseBook != _botton)
        {
            if(chooseBook!=null)
                chooseBook.GetComponent<Image>().color = Color.white;
            chooseBook = _botton;
            chooseBook.GetComponent<Image>().color = Color.grey;
        }

        else
        {
            chooseBook.GetComponent<Image>().color = Color.white;
            chooseBook = null;
        }
            


        if (chooseBook == null)
        {
            checkBotton.gameObject.SetActive(false);
        }

        else
        {
            checkBotton.gameObject.SetActive(true);
        }
            

    }
    

    public void Close_XiWang()
    {
        foreach (var _w in chooseBook.gameObject.GetComponentsInChildren<AbstractWord0>())
        {
            GameMgr.instance.AddCardList(_w.GetType());
        }
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion


    #region 交易
    GameObject cardPanal;
    [HideInInspector]public AbstractWord0 JY_chooseWord=null;
    Transform choose=null;
    Vector3 chooseScale = new Vector3(0.341f,0.32f,0.341f);
    public void OpenInit_JiaoYi()
    {        
        //读取已拥有的书中，未获得的卡，概率相同
        TextMeshProUGUI titleText= this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI title = this.transform.Find("title").GetComponent<TextMeshProUGUI>();
        Transform CardGroup = this.transform.Find("CardGroup");
        if (CardGroup.childCount > 0)
        {
            for (int _c = 0; _c < CardGroup.childCount; _c++)
            {
                Destroy(CardGroup.GetChild(_c).gameObject);
            }
            
        }


        DataInit(isKey);
        JY_chooseWord = null;
        choose = this.transform.Find("choose");
        cardPanal = this.transform.Find("cardRes").gameObject;

        cardPanal.SetActive(false);
   

        string adr_detail = "UI/WordInformation";




        //把选择框设置为看不见
        choose.transform.localScale = Vector3.zero;

        #region 抽取卡牌
        int[] _rWord =new int[3];
        
        int _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        int _loop = 0;
        while (GameMgr.instance.wordList.Contains(AllSkills.list_all[_random]) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("死循环");
        }
        _rWord[0] = _random;

        _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        _loop = 0;
        while (((_rWord[0]== _random )|| GameMgr.instance.wordList.Contains(AllSkills.list_all[_random])) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("死循环");
        }
        _rWord[1] =_random;


        _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        _loop = 0;
        while (((_rWord[0] == _random) || (_rWord[1] == _random) || GameMgr.instance.wordList.Contains(AllSkills.list_all[_random])) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("死循环");
        }
        _rWord[2] = _random;
        #endregion


        //生成卡牌
        for (int i = 0; i < 3; i++)
        {
            
            //生成卡牌
            //PoolMgr.GetInstance().GetObj(adr_detail, (obj) =>
            //{
                var  obj = ResMgr.GetInstance().Load<GameObject>(adr_detail);
                var _word=obj.AddComponent(AllSkills.list_all[_rWord[i]])as AbstractWord0;
                obj.GetComponentInChildren<WordInformation>().SetIsDetail(true);
                obj.GetComponent<Canvas>().overrideSorting = false;

                if (_word == null) print("this.gameObject.GetComponent<AbstractWord0>()");
                else
                    obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);


                obj.transform.parent = CardGroup;
                obj.transform.localPosition = new Vector3(0, 0, 3);
                obj.GetComponent<Canvas>().overrideSorting = false;

                obj.transform.localScale = Vector3.one*1.2f;
                obj.GetComponent<Canvas>().overrideSorting = false;

                //增加按钮组件
                var _botton =obj.AddComponent<Button>();
                _botton.transition = Selectable.Transition.None;
                _botton.onClick .AddListener(()=>ClickWord_YJ(_botton));
                // obj.GetComponent<Canvas>().overrideSorting = true;
            //});
            
        }

        //抽取事件信息

        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        title.text = tempNowDate[_r].name;

        //切换文字内容
        StartCoroutine(TypeDelay(( tempNowDate[_r].textEvent), titleText, 0.05f));
        //titleText.text = tempNowDate[_r].name+"\n"+ tempNowDate[_r].textEvent;
        nowEvent = tempNowDate[_r];

        //刷新已用事件列表
        tempNowDate.Remove(tempNowDate[_r]);
        RefreshNowList();


    }


    void ClickWord_YJ(Button which)
    {
        if(choose==null) choose = this.transform.Find("choose");
  
        if (JY_chooseWord == which.gameObject.GetComponent<AbstractWord0>())
        {
            choose.SetParent(which.gameObject.transform.parent.parent);
            choose.localScale = Vector3.zero;
            JY_chooseWord = null;
        }
        else
        {
            JY_chooseWord = which.gameObject.GetComponent<AbstractWord0>();
            choose.SetParent(which.gameObject.transform);
            choose.localScale = chooseScale;
            choose.localPosition = Vector3.zero;

          
            //cardPanal.GetComponent<ShooterWordCheck>().OpenMainPanal();
        }
  
        
        
    }


    public void JY_OpenCardRes()
    {
        cardPanal.GetComponent<ShooterWordCheck>().OpenMainPanal();
    }

    void Close_JiaoYi()
    {
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }



    #endregion


    #region 危机
    [HideInInspector] public bool WJ_static = false;//外部在open之前调用
    [HideInInspector] public int WJ_monster = -1;//外部在open之前调用
    private GameObject monster;
    public void OpenInit_WeiJi()
    {
        monster = null;
        DataInit(isKey);
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        var _spAnim = this.transform.Find("SpriteGroup").Find("L").GetComponent<Animator>();
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//如果有条件并且条件没满足,就重找一个

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("死循环");
                return;
            }
        }

        //设置按钮
        if (WJ_static)
        {
            this.transform.Find("button").Find("WelcomeButton").gameObject.SetActive(true);
            this.transform.Find("button").Find("EscapeButton").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("button").Find("WelcomeButton").gameObject.SetActive(true);
            this.transform.Find("button").Find("EscapeButton").gameObject.SetActive(true);
        }

        //抽取怪物
        if (WJ_monster >= 0)
        {
            GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().GetNextCreateMonster(WJ_monster);
            monster=GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().CreateMonster(1)[0];
            //audioPlay.Boss_GuaiWu();
        }
        else
        {
            WJ_monster= GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().GetNextCreateMonster();
            monster=GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().CreateMonster(1)[0];
            //audioPlay.Boss_GuaiWu();
        }
        _spAnim.Play(WJ_monster.ToString());


        //
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = this.transform.Find("title").GetComponentInChildren<TextMeshProUGUI>();
        title.text = tempNowDate[_r].name ;
        StartCoroutine(TypeDelay((tempNowDate[_r].textEvent), info, 0.05f));
        nowEvent = tempNowDate[_r];
        RefreshNowList();

    }


    public void Click_WeiJi()//点击逃离危机
    {
        print("Click_WeiJi");
        if (monster != null)
            Destroy(monster);
        Destroy(this.gameObject);
    
    }

    public void Close_WeiJi()//点击迎接危机
    {
 
        GameMgr.instance.gameProcess.WeiJiOpen();
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion


    #region 场景

    SpriteRenderer sp_cj_image;
    SpriteRenderer sp_cj_word;
    int cj_changeTo = -1;
    public void OpenInit_ChangJing()
    {
        DataInit(isKey);

        cj_changeTo = -1;
        sp_cj_image =transform.Find("scenephoto").Find("scene_jiuba").GetComponent<SpriteRenderer>();
        sp_cj_word=transform.Find("scenephoto").Find("scene_jiuba1").GetComponent<SpriteRenderer>();

        //抽场景
        cj_changeTo = UnityEngine.Random.Range(0, tempNowDate.Count);
        int _loopC = 0;
        while ((cj_changeTo == GameMgr.instance.levelSenceIndex) && (_loopC < 50))
        {
            cj_changeTo = UnityEngine.Random.Range(0, tempNowDate.Count);  _loopC++;
        }
     
        GameMgr.instance.levelSenceIndex = cj_changeTo;

        //切换文字内容
        var titleText = this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeDelay((tempNowDate[cj_changeTo].name + "\n" + tempNowDate[cj_changeTo].textEvent), titleText, 0.05f));
        nowEvent = tempNowDate[cj_changeTo];
        sp_cj_image.sprite = ResMgr.GetInstance().Load<Sprite>("UI/" + (tempNowDate[cj_changeTo].happen));
        sp_cj_word.sprite = ResMgr.GetInstance().Load<Sprite>("UI/" + (tempNowDate[cj_changeTo].happen) + "_word");

        //刷新已用事件列表
        tempNowDate.Remove(tempNowDate[cj_changeTo]);
        RefreshNowList();

    }
    public void Close_ChangJing()
    {
        if (cj_changeTo == -1) return;

        //变更场景
       
        GameMgr.instance.ChangeLevelTo(cj_changeTo);

        //
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion

    #endregion


    #region 事件触发效果invoke-happen 
    //对应test1表格happen那一行，写事件的函数名称


    ///【意外】解锁一个发射槽，并获得一个骰子
    void Happen_UnlochkShoot()
    {
        print("Happen_UnlochkShoot");
        GameMgr.instance.AddDice(1);

    }  ///发射器因断电而失控
    void Happen_ShooterBad()
    {
        print("Happen_ShooterBad");


    }  
    
    ///B队出现大量电子羊，并获得“虚拟生命力”设定
    void Happen_BSettingXNSML()
    {
        print("Happen_BSettingXNSML");

        GameMgr.instance.settingR.Add(typeof(XuNiShengMingLi));
        GameMgr.instance.settingPanel.RefreshList();
    }

    ///A队获得“化学极乐”设定
    void Happen_ASettingHXJL()
    {
        print("Happen_ASettingHXJL");

        GameMgr.instance.settingL.Add( typeof(HuaXueJiLe));
        GameMgr.instance.settingPanel.RefreshList();
    }
    ///【意外】Nexus-6型手臂加入牌库
    void Happen_GetNexus6()
    {
        print("Happen_GetNexus6");
        GameMgr.instance.AddCardList(new Nexus_6Arm().GetType());
    }

    ///【意外】随机一队，跳过界面流程，进入危机对抗敌人的环节
    void Happen_WeiJi()
    {
        print("Happen_WeiJi");
        GameMgr.instance.gameProcess.CreateWeijiEvent(false,0);

    }

    ///【意外】两个队伍都获得“人性尚存”，见细节文档-设定表
    void Happen_SettingRXSC()
    {
        print("Happen_SettingRXSC");

        GameMgr.instance.settingL.Add(typeof(RenXingShangCun));
        GameMgr.instance.settingR.Add(typeof(RenXingShangCun));
        GameMgr.instance.settingPanel.RefreshList();

    }

    ///【意外】进入一轮特殊的设定获取，所有的选项都带有【角色】标签，且不限制稀有度
    void Happen_SettingCharacter()
    {
        print("Happen_SettingCharacter");
        string adr = "UI/Setting";
        var obj = ResMgr.GetInstance().Load<GameObject>(adr);
        obj.GetComponent<Setting>().InitSetting(settingUiType.Chara,false);
    }

    ///【意外】两个队伍都获得“神经紊乱”，见细节文档-设定表
    void Happen_SettingSJWL()
    {
        print("Happen_SettingSJWL");

        GameMgr.instance.settingL.Add(typeof(ShenJingWenLuan));
        GameMgr.instance.settingR.Add(typeof(ShenJingWenLuan));
        GameMgr.instance.settingPanel.RefreshList();
    }

    ///【意外】三张被植入的记忆加入牌库
    void Happen_GetBeiZhiRuMemory()
    {
        print("Happen_GetBeiZhiRuMemory");
        GameMgr.instance.AddCardList(new BeiZhiRuDeJiYi().GetType());
        GameMgr.instance.AddCardList(new BeiZhiRuDeJiYi().GetType());
        GameMgr.instance.AddCardList(new BeiZhiRuDeJiYi().GetType());
    }
    #endregion



    #region 外部点击/调用事件
    public void RefreshEvent()
    {
        //刷新当前
        switch (type)
        {
            case EventType.FangKe:
                { OpenInit_FangKe(); }
                break;
            case EventType.YiWai:
                { OpenInit_YiWai(); }
                break;
            case EventType.XiWang:
                { OpenInit_XiWang(); }
                break;
            case EventType.JiaoYi:
                { OpenInit_JiaoYi(); }
                break;
            case EventType.ChangJing:
                { OpenInit_ChangJing(); }
                break;

        }
    }

    public void Open(bool _isKey)
    {
       
        isKey = _isKey;

        GameMgr.instance.OpenEventUi();
       CharacterManager.instance.pause = true;
        
        
        DealWithData(type);
        switch (type)
        {
            case EventType.XiWang:
                {
                    OpenInit_XiWang();
                }
                break;
            case EventType.FangKe:
                {
                    OpenInit_FangKe();
                }
                break;
            case EventType.YiWai:
                {
                    OpenInit_YiWai();
                }
                break;
            case EventType.JiaoYi:
                {
                    OpenInit_JiaoYi();
                }
                break;
            case EventType.WeiJi:
                {
                    OpenInit_WeiJi();
                }
                break;
            case EventType.ChangJing:
                {
                    OpenInit_ChangJing();
                }
                break;
        }
    }

    public void CloseAnim()
    {
        GetComponent<Animator>().Play("EventUI_Dis1");
        //BGM恢复
        audioSource.volume = 0.4f;
    }

    public void Close()
    {
        CharacterManager.instance.pause = false;
        GameMgr.instance.eventHappen = false;
        //BGM恢复
        audioSource.volume = 0.4f;
        //处理当前的nowChosenEvent
        if ((type != EventType.FangKe) && (type != EventType.ChangJing))
        {
            if ((nowEvent.happen != null) && (nowEvent.happen != ""))
            {
                print("nowEvent" + nowEvent.happen);
                Invoke(nowEvent.happen,0);
            } 
        }

        //内容加入草稿本
        GameMgr.instance.draftUi.AddContent(nowEvent.textDraft);
        
        //关闭面板
        switch (type)
        {
            case EventType.XiWang: 
                {
                    Close_XiWang();
                }break;
            case EventType.FangKe:
                {
                    Close_FangKe();
                }
                break;
            case EventType.YiWai:
                {
                    Close_YiWai();
                }
                break;
            case EventType.JiaoYi:
                {
                    Close_JiaoYi();
                }
                break;
            case EventType.WeiJi:
                {
                    Close_WeiJi();
                }
                break;
            case EventType.ChangJing:
                {
                    Close_ChangJing();
                }
                break;
        }
    }

    #endregion



}
