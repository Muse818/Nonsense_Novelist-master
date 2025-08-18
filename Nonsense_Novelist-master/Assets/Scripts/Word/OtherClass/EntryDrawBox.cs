using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
/// <summary>
/// 抽词条的盒子
/// </summary>
class EntryDrawBox : MonoBehaviour
{
    /// <summary>词条预制体</summary>
    public GameObject wordPrefab;
    /// <summary>父物体变换组件</summary>
    public Transform parentTF;
    /// <summary>战斗界面词条最大数量</summary>
    private int wordNum = 10;
    /// <summary>词条盒子加载的三颗星</summary>
    public Image threeStar;
    public float oneWordTimer = 0f;
    public float oneWordTime = 6f;
    public float oneWordTimer2 = 0f;
    public float oneWordTime2 = 20f;
    #region panel
    /// <summary>书桌界面父panel</summary>
    public Transform bookDeskPanel;
    /// <summary>书桌界面红楼梦形容词父panel</summary>
    public Transform hlmbookDeskPanel;
    /// <summary>书桌界面仿生人父panel</summary>
    public Transform fangPanel;
    /// <summary>书桌界面动物园父panel</summary>
    public Transform animalPanel;
    /// <summary>书桌界面水晶能量父panel</summary>
    public Transform shuijingPanel;
    /// <summary>书桌界面莎乐美形容词父panel</summary>
    public Transform shalemeiPanel;
    /// <summary>书桌界面蚂蚁帝国父panel</summary>
    public Transform mayiPanel;
    /// <summary>书桌界面流行病学父panel</summary>
    public Transform liuPanel;
    /// <summary>书桌界面埃及神话父panel</summary>
    public Transform aijiPanel;
    /// <summary>书桌界面通用父panel</summary>
    public Transform commonPanel;
    #endregion

    /// <summary>box按钮第一次点击</summary>
    public bool boxFirst = false;

    /// <summary>CD加载满，点击能否生成词条</summary>
    //public bool isCreateWord = false;
    public int wordNumm = 0;
    /// <summary>名词toggle</summary>
    public Toggle toggle_noun;
    /// <summary>动词toggle</summary>
    public Toggle toggle_verb;
    /// <summary>形容词toggle</summary>
    public Toggle toggle_adj;

    #region list
    private List<GameObject> hlm_noun = new List<GameObject>();
    private List<GameObject> hlm_verb = new List<GameObject>();
    private List<GameObject> hlm_adj = new List<GameObject>();
    private List<GameObject> ani_noun = new List<GameObject>();
    private List<GameObject> ani_verb = new List<GameObject>();
    private List<GameObject> ani_adj = new List<GameObject>();
    private List<GameObject> aiji_noun = new List<GameObject>();
    private List<GameObject> aiji_verb = new List<GameObject>();
    private List<GameObject> aiji_adj = new List<GameObject>();
    private List<GameObject> shuijing_noun = new List<GameObject>();
    private List<GameObject> shuijing_verb = new List<GameObject>();
    private List<GameObject> shuijing_adj = new List<GameObject>();
    private List<GameObject> liuxing_noun = new List<GameObject>();
    private List<GameObject> liuxing_verb = new List<GameObject>();
    private List<GameObject> liuxing_adj = new List<GameObject>();
    private List<GameObject> common_noun = new List<GameObject>();
    private List<GameObject> common_verb = new List<GameObject>();
    private List<GameObject> common_adj = new List<GameObject>();
    private List<GameObject> sahlemei_noun = new List<GameObject>();
    private List<GameObject> sahlemei_verb = new List<GameObject>();
    private List<GameObject> sahlemei_adj = new List<GameObject>();
    private List<GameObject> mayi_noun = new List<GameObject>();
    private List<GameObject> mayi_verb = new List<GameObject>();
    private List<GameObject> mayi_adj = new List<GameObject>();
    private List<GameObject> fang_noun = new List<GameObject>();
    private List<GameObject> fang_verb = new List<GameObject>();
    private List<GameObject> fang_adj = new List<GameObject>();
    #endregion

    public static int count = 0;
    public GameObject bookButton;
    private string book1;
    private string book2;
    private List<GameObject> toggleTwo=new List<GameObject>();
    public Transform bookPanel;

    //加载初始六个词条(废弃)
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Combat")
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    for (int i = 0; i < AllSkills.absWords.Length; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        word.AddComponent(AllSkills.absWords[i]);
                        if (word.GetComponent<AbstractWord0>() != null)
                        {
                            word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        }
                        word.transform.SetParent(parentTF);
                    }
                }
            }
        }
    }
    //缓冲词条盒子的CD，CD满了生成一个词条（废弃）
    private void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().name == "Combat")
        {
            oneWordTimer += Time.deltaTime;
            if (oneWordTimer <= oneWordTime)
            {
                if (wordNumm == 0)
                {
                    threeStar.GetComponent<Image>().fillAmount = (float)(oneWordTimer / oneWordTime);
                }
                else
                {
                    threeStar.GetComponent<Image>().fillAmount = 1;
                }
                return;
            }
            else
            {
                if (wordNumm < 3)
                {
                    wordNumm++;
                }
                oneWordTimer = 0f;
            }

        }
    }

    public void CreateOneWord()
    {
        if (wordNumm > 0)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    if (parentTF.childCount < wordNum)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = AllSkills.CreateSkillWord();
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(parentTF);
                    }
                }
            }
            wordNumm--;
        }
    }

    /// <summary>
    /// 书桌界面全部词条
    /// </summary>
    public void BookDeskDrawBox()
    {
        if (boxFirst == false)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    for (int i = 0; i < AllSkills.list_all.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = AllSkills.list_all[i];
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(bookDeskPanel);
                    }
                }
                boxFirst = true;
            }
        }
    }
    /*//版本一废弃方法
     /// <summary>
      /// 书桌界面全部名词词条
      /// </summary>
        public void BookDeskNounWords()
    {
        if (nounFirst == false)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    for (int i = 0; i < AllSkills.list_noun.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = AllSkills.AllNounWords(i);
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(bookDeskNounPanel);
                    }
                }
                nounFirst = true;
            }
        }
    }*/
    /*/// <summary>
    /// 书桌界面全部动词词条
    /// </summary>
    public void BookDeskVerbWords()
    {
        if (verbFirst == false)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    for (int i = 0; i < AllSkills.list_verb.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = AllSkills.AllVerbWords(i);
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(bookDeskVerbPanel);
                    }
                }
                verbFirst = true;
            }
        }
    }*/
    /*/// <summary>
    /// 书桌界面全部形容词词条
    /// </summary>
    public void BookDeskAdjWords()
    {
        if (adjFirst == false)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    for (int i = 0; i < AllSkills.list_adj.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = AllSkills.AllAdjWords(i);
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(bookDeskAdjPanel);
                    }
                }
                adjFirst = true;
            }
        }
    }*/


    /// <summary>
    /// 书桌界面点击书本显示词条
    /// </summary>
    public void BookDeskDiffBook()
    {
        // 书桌界面《红楼梦》词条
        if (EventSystem.current.currentSelectedGameObject.name == "红楼梦")
        {
            ShowWords(hlm_noun, hlm_verb, hlm_adj, hlmbookDeskPanel, AllSkills.hlmList_noun, AllSkills.hlmList_verb, AllSkills.hlmList_adj);
        }
        // 书桌界面《仿生人》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "仿生人")
        {
            ShowWords(fang_noun, fang_verb, fang_adj, fangPanel, AllSkills.humanList_noun, AllSkills.humanList_verb, AllSkills.humanList_adj);
        }
        // 书桌界面《动物园》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "动物园")
        {
            ShowWords(ani_noun, ani_verb, ani_adj, animalPanel, AllSkills.animalList_noun, AllSkills.animalList_verb, AllSkills.animalList_adj);
        }
        // 书桌界面《埃及神话》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "埃及神话")
        {
            ShowWords(aiji_noun, aiji_verb, aiji_adj, aijiPanel, AllSkills.aiJiShenHuaList_noun, AllSkills.aiJiShenHuaList_verb, AllSkills.aiJiShenHuaList_adj);
        }
        // 书桌界面《莎乐美》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "莎乐美")
        {
            ShowWords(sahlemei_noun, sahlemei_verb, sahlemei_adj, shalemeiPanel, AllSkills.shaLeMeiList_noun, AllSkills.shaLeMeiList_verb, AllSkills.shaLeMeiList_adj);
        }
        // 书桌界面《流行病学》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "流行病学")
        {
            ShowWords(liuxing_noun, liuxing_verb, liuxing_adj, liuPanel, AllSkills.liuXingBXList_noun, AllSkills.liuXingBXList_verb, AllSkills.liuXingBXList_adj);
        }
        // 书桌界面《蚂蚁帝国》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "蚂蚁帝国")
        {
            ShowWords(mayi_noun, mayi_verb, mayi_adj, mayiPanel, AllSkills.maYiDiGuoList_noun, AllSkills.maYiDiGuoList_verb, AllSkills.maYiDiGuoList_adj);
        }
        // 书桌界面《水晶能量》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "水晶能量")
        {
            ShowWords(shuijing_noun, shuijing_verb, shuijing_adj, shuijingPanel, AllSkills.crystalList_noun, AllSkills.crystalList_verb, AllSkills.crystalList_adj);
        }
        // 书桌界面《通用》词条
        else if (EventSystem.current.currentSelectedGameObject.name == "通用")
        {
            ShowWords(common_noun, common_verb, common_adj, commonPanel, AllSkills.commonList_noun, AllSkills.commonList_verb, AllSkills.commonList_adj);
        }
    }


    /// <summary>
    /// 勾选模式下生成词条(包含名动形)
    /// </summary>
    /// <param name="words_noun">该书的名词容器</param>
    /// <param name="words_verb">该书的名动词容器</param>
    /// <param name="words_adj">该书的形容词容器</param>
    /// <param name="panelTF">该书的panel</param>
    /// <param name="bookword_noun">AllSkills中该书的名词</param>
    /// <param name="bookword_verb">该书的动词</param>
    /// <param name="bookword_adj">该书的形容词</param>
    private void ShowWords(List<GameObject> words_noun, List<GameObject> words_verb, List<GameObject> words_adj, Transform panelTF, List<Type> bookword_noun, List<Type> bookword_verb, List<Type> bookword_adj)
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "MainCanvas")
            {
                if (toggle_noun.isOn && words_noun.Count == 0)
                {
                    for (int i = 0; i < bookword_noun.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = bookword_noun[i];
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(panelTF);

                        words_noun.Add(word);
                    }
                }
                if (toggle_verb.isOn && words_verb.Count == 0)
                {
                    for (int i = 0; i < bookword_verb.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = bookword_verb[i];
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(panelTF);

                        words_verb.Add(word);
                    }
                }
                if (toggle_adj.isOn && words_adj.Count == 0)
                {
                    for (int i = 0; i < bookword_adj.Count; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = bookword_adj[i];
                        word.AddComponent(absWord);
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(panelTF);

                        words_adj.Add(word);
                    }
                }
            }
        }

    }

    /// <summary>
    /// 点击名动形toggle
    /// </summary>
    public void ToggleEvent()
    {

        //清空list，删除
        //判定当前所在panel
        if (commonPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(common_noun, common_verb, common_adj, commonPanel, AllSkills.commonList_noun, AllSkills.commonList_verb, AllSkills.commonList_adj);
        }
        else if (hlmbookDeskPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(hlm_noun, hlm_verb, hlm_adj, hlmbookDeskPanel, AllSkills.hlmList_noun, AllSkills.hlmList_verb, AllSkills.hlmList_adj);
        }
        else if (animalPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(ani_noun, ani_verb, ani_adj, animalPanel, AllSkills.animalList_noun, AllSkills.animalList_verb, AllSkills.animalList_adj);
        }
        else if (liuPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(liuxing_noun, liuxing_verb, liuxing_adj, liuPanel, AllSkills.liuXingBXList_noun, AllSkills.liuXingBXList_verb, AllSkills.liuXingBXList_adj);
        }
        else if (aijiPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(aiji_noun, aiji_verb, aiji_adj, aijiPanel, AllSkills.aiJiShenHuaList_noun, AllSkills.aiJiShenHuaList_verb, AllSkills.aiJiShenHuaList_adj);
        }
        else if (shuijingPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(shuijing_noun, shuijing_verb, shuijing_adj, shuijingPanel, AllSkills.crystalList_noun, AllSkills.crystalList_verb, AllSkills.crystalList_adj);
        }
        else if (shalemeiPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(sahlemei_noun, sahlemei_verb, sahlemei_adj, shalemeiPanel, AllSkills.shaLeMeiList_noun, AllSkills.shaLeMeiList_verb, AllSkills.shaLeMeiList_adj);
        }
        else if (fangPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(fang_noun, fang_verb, fang_adj, fangPanel, AllSkills.humanList_noun, AllSkills.humanList_verb, AllSkills.humanList_adj);
        }
        else if (mayiPanel.gameObject.activeSelf == true)
        {
            RemoveAndDestroy(mayi_noun, mayi_verb, mayi_adj, mayiPanel, AllSkills.maYiDiGuoList_noun, AllSkills.maYiDiGuoList_verb, AllSkills.maYiDiGuoList_adj);
        }
    }

    private void RemoveAndDestroy(List<GameObject> word_noun, List<GameObject> word_verb, List<GameObject> word_adj, Transform panelTF, List<Type> bookword_noun, List<Type> bookword_verb, List<Type> bookword_adj)
    {
        if (!toggle_noun.isOn)
        {
            for (int i = 0; i < word_noun.Count; i++)
            {
                Destroy(word_noun[i]);
            }
            word_noun.RemoveRange(0, word_noun.Count);
        }
        if (!toggle_verb.isOn)
        {
            for (int i = 0; i < word_verb.Count; i++)
            {
                Destroy(word_verb[i]);
            }
            word_verb.RemoveRange(0, word_verb.Count);
        }
        if (!toggle_adj.isOn)
        {
            for (int i = 0; i < word_adj.Count; i++)
            {
                Destroy(word_adj[i]);
            }
            word_adj.RemoveRange(0, word_adj.Count);
        }
        if (toggle_noun.isOn)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    if (word_noun.Count == 0)
                    {
                        for (int i = 0; i < bookword_noun.Count; i++)
                        {
                            GameObject word = Instantiate(wordPrefab, canvas.transform);
                            Type absWord = bookword_noun[i];
                            word.AddComponent(absWord);
                            word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                            word.transform.SetParent(panelTF);

                            word_noun.Add(word);
                        }
                    }

                }
            }
        }
        if (toggle_verb.isOn)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    if (word_verb.Count == 0)
                    {
                        for (int i = 0; i < bookword_verb.Count; i++)
                        {
                            GameObject word = Instantiate(wordPrefab, canvas.transform);
                            Type absWord = bookword_verb[i];
                            word.AddComponent(absWord);
                            word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                            word.transform.SetParent(panelTF);

                            word_verb.Add(word);
                        }
                    }
                }
            }
        }
        if (toggle_adj.isOn)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    if (word_adj.Count == 0)
                    {
                        for (int i = 0; i < bookword_adj.Count; i++)
                        {
                            GameObject word = Instantiate(wordPrefab, canvas.transform);
                            Type absWord = bookword_adj[i];
                            word.AddComponent(absWord);
                            word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                            word.transform.SetParent(panelTF);

                            word_adj.Add(word);
                        }
                    }
                }
            }
        }

    }
    /// <summary>
    /// 新游戏界面勾选两本书
    /// </summary>
    public void ToggleBookSelect()
    {
        //选中
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
        {
            count++;
            toggleTwo.Add(EventSystem.current.currentSelectedGameObject);
            print("on");
        }
        else//取消选中
        {
            count--;
            toggleTwo.Remove(EventSystem.current.currentSelectedGameObject);
            print("no on");
        }

    }
    /// <summary>
    /// 新游戏界面生成两本选中的书
    /// </summary>
    public void InstantiateTwoBook()
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "MainCanvas")
            {
                for(int i = 0; i < toggleTwo.Count; i++)
                {
                    GameObject book = Instantiate(bookButton, canvas.transform);
                    //将选中的书赋值给预制体
                    book.name = toggleTwo[i].name;
                    book.transform.localScale = Vector3.one;
                    book.transform.SetParent(bookPanel);
                }
            }
        }
    }
}
