using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CustomList : List<string>
{
    public new void Add(string item)
    {
        IsContentValid(item);
        base.Add(item);
    }

    private void IsContentValid(string content)
    {
        for (int tt = 0; tt < GameMgr.instance.leftData.Count; tt++)
        {
            var _t = GameMgr.instance.leftData[tt];
            if (_t.textTrigger == content)
            {
                GameMgr.instance.leftData.Remove(_t);
                if (_t.isKey) GameMgr.instance.canHappenData_Key.Add(_t);
                else GameMgr.instance.canHappenData_nKey.Add(_t);
            }
        }
        //foreach (var _t in GameMgr.instance.leftData)
        //{
            
        //}
    }
}


public class GameMgr : MonoSingleton<GameMgr>
{

    [HideInInspector] public List<Type> settingL=new List<Type>();
    [HideInInspector] public List<Type> settingR = new List<Type>();


    //关闭界面相关
    private GameObject exitPanel;
    GameObject exitObj;
    Button exitButton;
    Button cancelButton;
    bool hasOpenExit = false;
    public SettingList settingPanel;
    public GameObject CardRes;

    //当前的场景
    [HideInInspector] public int levelSenceIndex = 0;
    //当前的剧本
    [HideInInspector] public BookNameEnum nowBook;

    //玩家已有的书库
    List<BookNameEnum> bookList = new List<BookNameEnum>();

    //玩家已有的，词语全部获取完毕的书库
    List<BookNameEnum> bookAllGetList = new List<BookNameEnum>();

    //战斗总牌库
    [HideInInspector] public List<Type> wordList = new List<Type>();

    //当前未使用的牌的牌库
    List<Type> wordNowList = new List<Type>();

    //当前待使用的牌的牌库
    [HideInInspector] public List<Type> wordGoingUseList = new List<Type>();

    //当前已使用的牌的牌库
    List<Type> wordHasUseList = new List<Type>();

    //名词的消耗性使用相关
    [HideInInspector] public Dictionary<Type, List<int>> NwordTimes = new Dictionary<Type, List<int>>();//目前的所有牌库中，名词和其使用次数。
    [HideInInspector] public Dictionary<Type, List<int>> NwordCanUseTimes = new Dictionary<Type, List<int>>();//目前待使用的牌库中，名词和其使用次数。
    [HideInInspector] public List<Type> outVerbs=new List<Type>();
    //骰子数量
    int diceNumber = 2;

    [Header("（策划）伤害参数")]
    public float attackAmount=5;
    public void AddDice(int i)
    {
        diceNumber += i;
    }
    public int GetDice()
    {
        return diceNumber;
    }
    public void DeleteDice(int i)
    {
        diceNumber -= i;
        if (diceNumber < 0) diceNumber = 0;
    }



    //触发特殊文本
    [HideInInspector] public List<int> AttackHDList = new List<int>();
    [HideInInspector] public List<int> CureHDList = new List<int>();


    #region 事件
    //当前触发的所有事件
    [HideInInspector] public CustomList happenEvent = new CustomList();

    //当前可以触发的所有事件
    [HideInInspector] public List<eventExcelItem> canHappenData_nKey;
    [HideInInspector] public List<eventExcelItem> canHappenData_Key;
    [HideInInspector] public List<eventExcelItem> leftData;

    /// <summary>
    /// 读取excel表数据，加入所有没有前置条件的data
    /// </summary>
    /// <param name="_type"></param>
    void DealWithData()
    {
        canHappenData_nKey.Clear();
        canHappenData_Key.Clear();
        leftData.Clear();

        foreach (var _t in AllData.instance.data.items)
        {
            if ((_t.textTrigger == null) || (_t.textTrigger == ""))
            {
                if (!_t.isKey)
                {
                    if (!canHappenData_nKey.Contains(_t))
                        canHappenData_nKey.Add(_t);
                }
                else
                {
                    if (!canHappenData_Key.Contains(_t))
                        canHappenData_Key.Add(_t);
                }
            }
            else
            {
                if (!leftData.Contains(_t))
                    leftData.Add(_t);
            }
        }
    }


    public bool HaveCanHappenKeyEvent(int _enumNum)
    {
       
 
        foreach (var item in canHappenData_Key)
        {
          
            if (item.type == (EventType)(Convert.ToInt32(_enumNum)))
            {

                return true;
            }
        }
  
        return false;
    }

    #endregion

    [Header("数值调整(手动)")]
    public float afterScale = 0.28f;
    public float beforeScale = 18;
    public float afterClickScale = 0.44f;
    public Vector3 charaPosOffset = new Vector3(0,10,0);
    public float cardRate_1 = 20;
    public float cardRate_2 = 30;
    public float cardRate_3 = 30;
    public float cardRate_4 = 20;

    [Header("界面设置(手动)")]
    public GameObject UiCanvas;
    public GameObject characterCanvas;
    public GameProcessSlider gameProcess;
    public DraftUi draftUi;
    public GameObject combatCanvas;
    public EventCg EventCGAnim;
    public LevelController levelController;
    public bool eventHappen = false;

    [Header("开关(测试)")]
    public bool playEventCG = true;
    public bool DebugUi = false;
    private int stageIndex = 0;//游戏阶段
    [HideInInspector]public float time1=0;
    [HideInInspector] public float time2=0;
    [HideInInspector] public float timeSpeed = 1;

    override public void Awake()
    {
        base.Awake();
        DealWithData();
        CardRes.SetActive(false);
        draftUi.InitContent();
        EventCGAnim.gameObject.SetActive(false);
        print("Awake");

        //界面设置
        OpenCharacterPutting();

        //退出菜单
        exitPanel = Resources.Load<GameObject>("UI/exitPanel");
        //DontDestroyOnLoad(this.gameObject);

       // 牌库
        InitCardList();
        Time.timeScale = 1;
        CharacterManager.instance.pause = false;
    }

    private void Start()
    {
        ////print("Start");
        ////InitCardList();
        //////界面设置
        ////OpenCharacterPutting();

        ////退出菜单
        ////exitPanel = Resources.Load<GameObject>("UI/exitPanel");
        ////DontDestroyOnLoad(this.gameObject);

        ////牌库
        ////InitCardList();
    }
    private void Update()
    {
        time1 += Time.deltaTime;
        if (!CharacterManager.instance.pause) { time2 += Time.deltaTime; }

        //退出菜单
        if (hasOpenExit) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitObj = Instantiate(exitPanel);
            Time.timeScale = 0f;
            hasOpenExit = true;
            exitButton = exitObj.transform.GetComponentsInChildren<Button>()[0];
            cancelButton = exitObj.transform.GetComponentsInChildren<Button>()[1];
            exitButton.onClick.AddListener(ExitButton);
            cancelButton.onClick.AddListener(BackToGame);
        }

        if (!DebugUi) return;
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (characterCanvas.GetComponentInChildren<DebugUi>()) return;
 
            Instantiate<GameObject>(ResMgr.GetInstance().Load<GameObject>("debugUI"), characterCanvas.transform);
        }
        
    }

    #region 新 牌库
    /// <summary>
    /// 通用卡组
    /// </summary>
    void StartCardList()
    {
        //测试
        AddCardList(new HunHe().GetType());
        AddCardList(new HunHe().GetType());
        AddCardList(new HunHe().GetType()); AddCardList(new HunHe().GetType()); AddCardList(new HunHe().GetType());

        return;
        ////

        AddCardList(new FuTouAxe().GetType());
        AddCardList(new QiGuaiShiXiang().GetType()); 
        AddCardList(new HeartBroken().GetType());
        AddCardList(new FengLi().GetType());
        AddCardList(new QuicklyGrowing().GetType());
        AddCardList(new LuoYingBinFen().GetType());
        AddCardList(new JianRuPanShi().GetType());
  
        RefreshNowList();
    }

    /// <summary>
    /// 牌库初始化(包含初始牌库组)
    /// </summary>
    /// 

    void InitCardList()
    {
        wordList.Clear();
        wordNowList.Clear();
        wordGoingUseList.Clear();
        wordHasUseList.Clear();

        NwordTimes.Clear();
        //wordHasUseTimes.Clear();
        NwordCanUseTimes.Clear();

        happenEvent.Clear();

        //测试用
        // wordList.AddRange(new Type[] { typeof(BuryFlower), typeof(QiChongShaDance) });

        nowBook = BookNameEnum.ElectronicGoal;

        //加入通用词组
        StartCardList();
        print("牌库初始化完成" + wordList.Count + wordGoingUseList.Count);
    }





    /// <summary>
    /// 往卡组里加入卡牌
    /// </summary>
    /// <param name="_word"></param>
    public void AddCardList(Type _word)
    {
        
        wordList.Add(_word);
        wordNowList.Add(_word);
    

        if (_word.BaseType == typeof(AbstractItems))
        {
            //如果输入的词语是名词，则往可用名词列表中加入词语与其次数
            if (NwordTimes.ContainsKey(_word))
            {
                NwordTimes[_word].Add((int)_word.GetField("s_useTimes").GetValue(null));
            }
            else
            {
                NwordTimes.Add(_word, new List<int> { (int)_word.GetField("s_useTimes").GetValue(null) });
                //print("[NwordTimes]Add:" + (_word as AbstractItems).wordName + (int)Type.GetType(_word.GetType().Name).GetField("s_useTimes").GetValue(null));
            }

            if (NwordCanUseTimes.ContainsKey(_word))
            {
                NwordCanUseTimes[_word].Add((int)_word.GetField("s_useTimes").GetValue(null));
                //print("[NwordCanUseTimes]Add:" + (_word as AbstractItems).wordName + (int)Type.GetType(_word.GetType().Name).GetField("s_useTimes").GetValue(null));
            }
            else
            {
                NwordCanUseTimes.Add(_word, new List<int> { (int)_word.GetField("s_useTimes").GetValue(null) });
                // print("[NwordCanUseTimes]AddNew:" + (_word as AbstractItems).wordName + (int)Type.GetType(_word.GetType().Name).GetField("s_useTimes").GetValue(null));
            }

        }
    }


    /// <summary>
    /// 从卡组里删除卡牌
    /// </summary>
    /// <param name="_word"></param>
    public void DeleteCardList(AbstractWord0 _word)
    {
        if (_word == null) print("null");
        if (wordList.Contains(_word.GetType()))
        {
            wordList.Remove(_word.GetType());
            //随机删掉一张使用次数还有的词语
            if (_word is AbstractItems)
            {
                if (NwordTimes.ContainsKey((_word as AbstractItems).GetType()))
                {
                    //NwordTimes[(_word as AbstractItems).GetType()].Remove(0);
                     NwordTimes[(_word as AbstractItems).GetType()].RemoveAll(item => item == 0);
                    if (NwordTimes[(_word as AbstractItems).GetType()].Count <= 0)
                    {
                        NwordTimes.Remove((_word as AbstractItems).GetType());
                        
                    }
                }
                if (NwordCanUseTimes.ContainsKey((_word as AbstractItems).GetType()))
                {
                    //NwordCanUseTimes[(_word as AbstractItems).GetType()].Remove(0);
                    NwordCanUseTimes[(_word as AbstractItems).GetType()].RemoveAll(item => item == 0);
                    if (NwordCanUseTimes[(_word as AbstractItems).GetType()].Count <= 0)
                    {
                        NwordCanUseTimes.Remove((_word as AbstractItems).GetType());
                       
                    }
                }
            }
        }

        if (wordNowList.Contains(_word.GetType()))
        {
     
            wordNowList.Remove(_word.GetType());
            return;
        }
        else if (wordHasUseList.Contains(_word.GetType()))
        {
            wordHasUseList.Remove(_word.GetType());
            return;
        }
        else if (wordGoingUseList.Contains(_word.GetType()))
        {
            bool not = false;
            int i = 0;
            while ( (wordGoingUseList[i] != _word.GetType()) && (i < wordGoingUseList.Count))
            {
                i++;
            }
            if (wordGoingUseList[i] == _word.GetType())
            {
                wordGoingUseList[i]=GetNowListOne();
            }
            
        }
        RefreshNowList();
    }

    public void DeleteCardList(Type _word)
    {
        if (_word == null) print("null");

        if (wordList.Contains(_word))
        {

            wordList.Remove(_word);
            if (_word.BaseType == typeof(AbstractItems))
            {

                if (NwordTimes.ContainsKey(_word))
                {
                    //NwordTimes[_word].Remove(0);
                    NwordTimes[_word].RemoveAll(item => item == 0);
                    if (NwordTimes[_word].Count <= 0)
                    {
                        NwordTimes.Remove(_word);
                        
                    }
                }
                if (NwordCanUseTimes.ContainsKey(_word))
                {
                    //NwordCanUseTimes[_word].Remove(0);
                    NwordCanUseTimes[_word].RemoveAll(item => item == 0);
                    if (NwordCanUseTimes[_word].Count <= 0)
                    {
                        NwordCanUseTimes.Remove(_word);
                      
                    }
                }
            }
        }


        if (wordNowList.Contains(_word))
        {
            wordNowList.Remove(_word);
    
        }
        else if (wordHasUseList.Contains(_word))
        {
            wordHasUseList.Remove(_word);
       
        }
        else if (wordGoingUseList.Contains(_word))
        {
            int i = 0;
            while ((wordGoingUseList[i] != _word.GetType())&&(i<wordGoingUseList.Count-1))
            {
                i++;
                if (i >= wordGoingUseList.Count-1) break;
            }
            if (wordGoingUseList[i] == _word.GetType())
            {
                wordGoingUseList[i] = GetNowListOne();
            }
        }

        RefreshNowList();
    }

    public void UseCard(Type _word)
    {
        if (_word.BaseType == typeof(AbstractItems))
        {

            if (NwordCanUseTimes.ContainsKey(_word))
            {

                int _r = UnityEngine.Random.Range(0, NwordCanUseTimes[_word].Count);

                print("times:" + NwordCanUseTimes[_word][_r]);
                if (NwordCanUseTimes[_word][_r]>0)
                    NwordCanUseTimes[_word][_r] -= 1;
                if (NwordCanUseTimes[_word][_r] <= 0)
                {
                    DeleteCardList(_word);
                    //NwordCanUseTimes[_word].Remove(_r);
                    //if (NwordCanUseTimes[_word].Count <= 0)
                    //{
                    //    NwordCanUseTimes.Remove(_word);
                    //    DeleteCardList(_word);
                    //}
                }
                  
                RefreshNowList();
                
            }
            
        }
        if (_word.BaseType == typeof(AbstractVerbs))
        {
            DeleteCardList(_word);
            outVerbs.Add(_word);
        }
    }


    public void DetectVerb(Type _word)
    {
        if (outVerbs.Contains(_word))
        {
            AddCardList(_word);
            outVerbs.Remove(_word);
        }
    }
    /// <summary>
    /// 返回已经使用的词条们
    /// </summary>
    /// <returns></returns>
    public List<Type> GetHasUsedList()
    {
        //wordHasUseList.OrderBy(it => it.Name).ToList();
        return wordHasUseList;
    }
    /// <summary>
    /// 返回未使用的词条们
    /// </summary>
    /// <returns></returns>
    public List<Type> GetNowList()
    {
        //wordNowList.OrderBy(it => it.Name).ToList();
        return wordNowList;
    }
    public List<Type> GetGoingToUseList()
    {
        //wordNowList.OrderBy(it => it.Name).ToList();
        return wordGoingUseList;
    }
    /// <summary>
    /// 随机返回一个未使用的词条
    /// </summary>
    /// <returns></returns>
    public Type GetNowListOne()
    {
        
        RefreshNowList();

        if (wordNowList.Count == 0)
        {
            return null;
        }

        int count = UnityEngine.Random.Range(0, wordNowList.Count);
        
      
        // print("卡组里没有卡牌+" + count);
        var _res = wordNowList[count];
        wordNowList.Remove(_res);
      
        //wordHasUseList.Add(_res);

        //if (_res.BaseType == typeof(AbstractItems))
        //{
        //    if (!NwordCanUseTimes.ContainsKey(_res))
        //    {
        //        print("!GameMgr.instance.NwordCanUseTimes.ContainsKey(this)");
        //    }
        //    int _random = UnityEngine.Random.Range(0, NwordCanUseTimes[_res].Count);
        //    NwordCanUseTimes[_res][_random] -= 1;

        //    if (NwordCanUseTimes[_res][_random] <= 0)
        //    {
        //        DeleteCardList(_res);
        //    }

        //}
        return _res;
    }



    public Type GetGoingUseList()
    {

       // RefreshNowList();
        //全部解锁前，只有3个槽位
        for (int i = 0; i < 3; i++)
        {
           
            wordGoingUseList.Add(GetNowListOne());
         

        }
        CreateOneCharacter.firstUseCardlist = false ;
        //wordHasUseList.Add(wordGoingUseList[0]);

        //print(wordGoingUseList[0].)
        //if (wordGoingUseList[0].IsAssignableFrom(typeof(AbstractVerbs)))
        //{
        //    print("动词1");
        //}

        return wordGoingUseList[0];
    }
    public Type GetGoingUseListOne()
    {
        // RefreshNowList();
        wordGoingUseList.Add(GetNowListOne());
        //wordHasUseList.Add(wordGoingUseList[0]);

        return wordGoingUseList[0];
    }
    public List<Type> GetAllList()
    {
        return wordList;
    }

    /// <summary>
    /// 刷新现有卡牌列表
    /// </summary>
    public void RefreshNowList()
    {
        wordHasUseList.Clear();
        foreach (var _c in wordList)
        {
            if ((!wordNowList.Contains(_c)) && (!wordGoingUseList.Contains(_c)))
            {
                wordHasUseList.Add(_c);
            }
        }
        if ((wordNowList.Count == 0) )
        {
            
            wordNowList.AddRange(wordList);
            wordHasUseList.Clear();

            NwordCanUseTimes.Clear();
            foreach (var _card in NwordTimes)
            {
                if (NwordCanUseTimes.ContainsKey(_card.Key))
                {
                    NwordCanUseTimes[_card.Key].Clear();
                    NwordCanUseTimes[_card.Key].AddRange(_card.Value);

                }
                else
                {
                    NwordCanUseTimes.Add(_card.Key, _card.Value);
                }
            }
        }
    }

    #region 书本
    //获取已有的书本列表
    public List<BookNameEnum> GetBookList()
    {
        return bookList;
    }



    /// <summary>
    /// 检测是否一本书的所有词组都已经获取。返回是或否
    /// </summary>
    /// <param name="_book"></param>
    /// <returns></returns>
    public bool IsBookWordAllGet(BookNameEnum _book)
    {
        if (bookAllGetList.Contains(_book)) return true;

        List<Type> _typeList = GetBookList(_book);
        if (_typeList == null)
        {
            print("_typeList == null"); return true;
        }


        foreach (var _w in _typeList)
        {
            if (!wordList.Contains(_w))
            {
                return false;
            }
        }
        bookAllGetList.Add(_book);
        return true;
    }


    public List<Type> GetBookList(BookNameEnum _book)
    {
        List<Type> _typeList = null;
        switch (_book)
        {
            case BookNameEnum.HongLouMeng:
                {
                    _typeList = AllSkills.hlmList_all;
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    _typeList = AllSkills.crystalList_all;
                }
                break;
            case BookNameEnum.Salome:
                {
                    _typeList = AllSkills.shaLeMeiList_all;
                }
                break;
            case BookNameEnum.ZooManual:
                {
                    _typeList = AllSkills.animalList_all;
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    _typeList = AllSkills.maYiDiGuoList_all;
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    _typeList = AllSkills.liuXingBXList_all;
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    _typeList = AllSkills.aiJiShenHuaList_all;
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    _typeList = AllSkills.humanList_all;
                }
                break;
            case BookNameEnum.allBooks:
                {
                    _typeList = AllSkills.commonList_all;
                }
                break;

        }
        return _typeList;
    }


    /// <summary>
    /// 随机获取书本中还未获取的词语
    /// </summary>
    /// <param name="_book"></param>
    public Type GetBookListNeedWordOne(BookNameEnum _book)
    {
        //已经没有没获得的词语了，返回null
        if (IsBookWordAllGet(_book)) return null;

        List<Type> _typeList = GetBookList(_book);
        if (_typeList == null)
        {
            print("_typeList == null"); return null;
        }


        int _R = UnityEngine.Random.Range(0, _typeList.Count);

        while (wordList.Contains(_typeList[_R]))
        {
            _R = UnityEngine.Random.Range(0, _typeList.Count);

        }
        return _typeList[_R];
    }

    #endregion



    public void SetCombatStartList(List<Type> _list)
    {
        bookList.Clear();
        wordList.Clear();
        wordList = _list;
    }
    public void AddCombatStartList(List<Type> _list)
    {

        foreach (var _i in _list)
        {

            if (!wordList.Contains(_i))
            {
                _i.GetType();
                int _r = 1;
                //var count = _i.GetField("rarity").GetValue(null);
                //if (count != null) _r = (int)count;

                //for (int i = 0; i < _r; i++)
                //{
                wordList.Add(_i);
                //}
            }

        }
    }

    //public void AddBookList(BookNameEnum _book)
    //{
    //    switch (_book)
    //    {
    //        case BookNameEnum.HongLouMeng:
    //            {
    //                if (!bookList.Contains(BookNameEnum.HongLouMeng))
    //                {
    //                    //AddCombatStartList(AllSkills.hlmList_all);
    //                    bookList.Add(BookNameEnum.HongLouMeng);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.CrystalEnergy:
    //            {
    //                if (!bookList.Contains(BookNameEnum.CrystalEnergy))
    //                {
    //                    //AddCombatStartList(AllSkills.crystalList_all);
    //                    bookList.Add(BookNameEnum.CrystalEnergy);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.Salome:
    //            {
    //                if (!bookList.Contains(BookNameEnum.Salome))
    //                {
    //                    //AddCombatStartList(AllSkills.shaLeMeiList_all);
    //                    bookList.Add(BookNameEnum.Salome);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.ZooManual:
    //            {
    //                if (!bookList.Contains(BookNameEnum.ZooManual))
    //                {
    //                    //AddCombatStartList(AllSkills.animalList_all);
    //                    bookList.Add(BookNameEnum.ZooManual);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.PHXTwist:
    //            {
    //                if (!bookList.Contains(BookNameEnum.PHXTwist))
    //                {
    //                    // AddCombatStartList(AllSkills.maYiDiGuoList_all);
    //                    bookList.Add(BookNameEnum.PHXTwist);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.FluStudy:
    //            {
    //                if (!bookList.Contains(BookNameEnum.FluStudy))
    //                {
    //                    //AddCombatStartList(AllSkills.liuXingBXList_all);
    //                    bookList.Add(BookNameEnum.FluStudy);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.EgyptMyth:
    //            {
    //                if (!bookList.Contains(BookNameEnum.EgyptMyth))
    //                {
    //                    //AddCombatStartList(AllSkills.aiJiShenHuaList_all);
    //                    bookList.Add(BookNameEnum.EgyptMyth);
    //                }
    //            }
    //            break;
    //        case BookNameEnum.ElectronicGoal:
    //            {
    //                if (!bookList.Contains(BookNameEnum.ElectronicGoal))
    //                {
    //                    //AddCombatStartList(AllSkills.humanList_all);
    //                    bookList.Add(BookNameEnum.ElectronicGoal);
    //                }
    //            }
    //            break;
    //        case BookNameEnum.allBooks:
    //            {
    //                if (!bookList.Contains(BookNameEnum.allBooks))
    //                {
    //                    AddCombatStartList(AllSkills.commonList_all);
    //                    bookList.Add(BookNameEnum.allBooks);
    //                }

    //            }
    //            break;

    //    }
    //}
    //public bool HasBook(BookNameEnum _book)
    //{
    //    if (bookList.Contains(_book))
    //        return true;
    //    return false;
    //}
    #endregion


    #region 卡牌稀有度&&游戏阶段

    public void AddStage(int i)
    {
        stageIndex += i;
        bool b=SetRareTo(stageIndex);
        if (!b) print("stageIndex超出游戏设定");
    }
    public int GetStage()
    {
        return stageIndex;
    }
    public void SetStageTo(int i)
    {
        stageIndex = i;
        bool b = SetRareTo(stageIndex);
        if (!b) print("stageIndex超出游戏设定");
    }

    private bool SetRareTo(int _stage)
    {
        if (_stage >= AllData.instance.cardRareDate.items.Length)
            return false;

        //这里默认数据顺序和表格一样。如果出错了，加上index检测
        cardRate_1 = AllData.instance.cardRareDate.items[_stage].rate1;
        cardRate_2 = AllData.instance.cardRareDate.items[_stage].rate2;
        cardRate_3 = AllData.instance.cardRareDate.items[_stage].rate3;
        cardRate_4 = AllData.instance.cardRareDate.items[_stage].rate4;

        return true;
    }


    #endregion

    #region level
    public void ChangeLevelTo(int start)
    {

        levelController.SetLevelTo(start);
    }
    #endregion

    #region 事件气泡
    /// <summary>
    /// 显示事件的飘字
    /// </summary>
    public void PopupEvent(Vector3 pos, string name, string info)
    {
        var obj=ResMgr.GetInstance().Load<GameObject>("UI/popEvent"); 
        //世界坐标转画布坐标
        Vector2 canvasSize = characterCanvas.GetComponent<RectTransform>().sizeDelta;
        Vector3 viewPortPos3d = Camera.main.WorldToViewportPoint(pos);
        Vector2 viewPortRelative = new Vector2(viewPortPos3d.x - 0.5f, viewPortPos3d.y - 0.5f);
        Vector2 cubeScreenPos = new Vector2(viewPortRelative.x * canvasSize.x, viewPortRelative.y );
        obj.GetComponent<RectTransform>().position = pos;
        obj.transform.parent = characterCanvas.transform;
        obj.transform.localScale = Vector3.one;
         StartCoroutine(MoveToPosCanvas( obj.GetComponent<RectTransform>(), name,info));
        


    }

    WaitForSeconds waitFrame = new WaitForSeconds(0.02f);
    IEnumerator MoveToPosCanvas(RectTransform obj,string text1,string text2)
    {  
        var pos = new Vector2(-796, -425);
        Image image = obj.gameObject.GetComponentInChildren<Image>();
        TextMeshProUGUI text = obj.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        //停留在最开始的点 
        float i = 0;
        obj.transform.Find("Image").GetComponent<Animator>().Play("burst");
        while (i< 1f)
        {
            i += 0.02f;
            
            text.gameObject.SetActive(false);
            text.text = text1;
            yield return waitFrame;
        }

        obj.transform.Find("Image").GetComponent<Animator>().Play("card");
        //移动到草稿本
        while (Mathf.Abs((obj.anchoredPosition- pos).x)>2f)
        {
            
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            obj.anchoredPosition -= 0.04f * (obj.anchoredPosition - pos);
            yield return waitFrame;
        }

        //停留在草稿本
        i = 0;
        text.text = "";
        int _index = 0;
        float textDelay = 0.06f;
        float textDAll = 0;
        obj.transform.Find("Image").GetComponent<Animator>().Play("burst");
        while (i < 1f)
        {
            i += 0.02f;
            
            text.gameObject.SetActive(true);
            if (i > textDAll)
            {
                textDAll += textDelay;
                if (_index < text2.Length)
                {
                    text.text += text2[_index];
                    _index += 1;
                }
            }
          
            yield return waitFrame;
        }
        Destroy(obj.gameObject);
    }


    #endregion


    #region exit菜单相关
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackToGame()
    {hasOpenExit = false;
        Destroy(exitObj.gameObject);
        Time.timeScale = GameMgr.instance.timeSpeed;
        
    }
    #endregion




    #region 调用各种界面
    void OpenCharacterPutting()
    {
        combatCanvas.gameObject.SetActive(true);
        draftUi.gameObject.SetActive(false);
        UiCanvas.gameObject.SetActive(true);
    }

    public int GetNextCreateChara()
    {
        return UiCanvas.GetComponentInChildren<CreateOneCharacter>().GetNextCreateChara();
    }
    public void GetNextCreateChara(int _sds)
    {
         UiCanvas.GetComponentInChildren<CreateOneCharacter>().GetNextCreateChara(_sds);
    }
    public void CreateCharacterPut(int initCharacter)
    {
        //镜头拉远
        Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f); 
        //生成面板  进入放角色页面
        CharacterManager.instance.pause = true;
        if (UiCanvas != null)
            UiCanvas.SetActive(true);
        UiCanvas.GetComponentInChildren<CreateOneCharacter>().CreateNewCharacter(initCharacter);
    }
    public void CreateTheCharacterPut(int characterID)
    {
        //镜头拉远
        Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
        //生成面板  进入放角色页面
        CharacterManager.instance.pause = true;
        if (UiCanvas != null)
            UiCanvas.SetActive(true);
        UiCanvas.GetComponentInChildren<CreateOneCharacter>().CreateTheCharacter(characterID);
    }

    public void OpenEventUi()
    {
   
        draftUi.gameObject.SetActive(false);
        UiCanvas.gameObject.SetActive(false);
    }

    public void ShowGameUI()
    {
        combatCanvas.gameObject.SetActive(true);
    }

    public void HideGameUI()
    {
        combatCanvas.gameObject.SetActive(false);
        draftUi.gameObject.SetActive(false);
        UiCanvas.gameObject.SetActive(false);
    }
    #endregion


    #region cg
    WaitForFixedUpdate wait= new WaitForFixedUpdate();
    public void PlayCG(string name, float delayTime)
    {
        if (playEventCG == false) return;
        if (delayTime > 0)
        {
            StartCoroutine(WaitAndCg(name, delayTime));
        }
        else

        { GameMgr.instance.EventCGAnim.gameObject.SetActive(true);
            GameMgr.instance.EventCGAnim.PlayEventCG(name); }
    }
    IEnumerator WaitAndCg(string name,float delayTime)
    {
        float t = 0;
        while (t < delayTime)
        {
            yield return wait;
            t += Time.deltaTime;
        }
        //播放开场动画
        GameMgr.instance.EventCGAnim.gameObject.SetActive(true);
        GameMgr.instance.EventCGAnim.PlayEventCG(name);
    }

    #endregion

    #region 游戏阶段
    public void CreateMonster(int id)
    {
        int _index = -1;
        for (int ttt = 0; (ttt < AllData.instance.monsterDate.items.Length)&&(_index!=-1);ttt++)
        {
            if ((AllData.instance.monsterDate.items[ttt].Mid == id)&&(AllData.instance.monsterDate.items[ttt].name==stageIndex))
            {
                _index = ttt;
            }
        }

        if (_index == -1) return;

        var _data = AllData.instance.monsterDate.items[_index];
        int _id = id - 110;
        var _monster = Instantiate<GameObject>(UiCanvas.GetComponent<CreateOneCharacter>().monsterPrefabs[_id]);
        var _mAc = _monster.GetComponent<AbstractCharacter>();
        _mAc.Camp = CampEnum.stranger;
        _mAc.maxHp = _data.hp; 
        _mAc.hp = _mAc.maxHp;
        _mAc.def = _data.def;
        _mAc.atk = _data.atk;
        _mAc.psy = _data.psy;
        _mAc.san = _data.san;
        //放置在合适的位置

        //增加自带技能
        if ((_data.word1 != "") & (_data.word1 != null))
        {
            Type type = Type.GetType(_data.word1);
            if (type != null)
            {
                _monster.AddComponent(type);
            }
        }
    }
    #endregion
}
