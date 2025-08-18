using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class AddBooKAndWords : MonoBehaviour
{
    [Header("【书本三选一】界面")]
    public Transform chooseBooksParent;
    Vector3 bookOriPos=Vector3.zero;
    int bookOriNumber = -1;
    private BookNameEnum[] book = new BookNameEnum[3];
    [Header("【查看书本包含的词条】界面-手动")]
    public Transform checkWordsParent;
    public Transform wordsArea;
    [Header("词条预制体（手动）")]
    public GameObject prefab_noun;
    public GameObject prefab_verb;
    public GameObject prefab_adj;

    private bool hasEnter = false;
    private void Awake()
    {
        //关闭进行中的其它界面(暂时没写，因为好像不关也可以)


        //关闭书本词条界面，开启三选一界面
        checkWordsParent.gameObject.SetActive(false);
        chooseBooksParent.gameObject.SetActive(true);
        chooseBooksParent.GetChild(0).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(1).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(2).GetComponent<Image>().color = Vector4.one;
        //
        GetBook();
    }

    /// <summary>
    /// 获取没用过的书。随机抽三本，将其显示在定点上
    /// </summary>
    void GetBook()
    {
        int[] ds= new int[3]{ 0,0,0};
        book[0] = BookNameEnum.Salome;
        book[1] = BookNameEnum.EgyptMyth;
        book[2] = BookNameEnum.ZooManual;
        //获取没用的书
        //这里先随便简写



        //刷新button的图样。
        chooseBooksParent.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book[0].ToString());
        chooseBooksParent.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book[1].ToString());
        chooseBooksParent.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book[2].ToString());
    }

    #region 按钮点击
    public void Click_A_Book()
    {
        if (hasEnter) return;
        ClickBook(0);
    }
    public void Click_B_Book()
    {
        if (hasEnter) return;
        ClickBook(1);
    }
    public void Click_C_Book()
    {
        if (hasEnter) return;
        ClickBook(2);
    }

    private void ClickBook(int _book)
    {
        hasEnter = true;
        //关闭三选一界面，进入详细界面
        if (_book != 0) chooseBooksParent.GetChild(0).GetComponent<Image>().color = Vector4.zero;
        if (_book != 1) chooseBooksParent.GetChild(1).GetComponent<Image>().color = Vector4.zero;
        if (_book != 2) chooseBooksParent.GetChild(2).GetComponent<Image>().color = Vector4.zero;

        bookOriNumber = _book;
        bookOriPos = chooseBooksParent.GetChild(_book).position;

        chooseBooksParent.GetChild(_book).position = checkWordsParent.Find("book").position;
        checkWordsParent.gameObject.SetActive(true);
        //开启那个界面，并且生成词组
        switch(book[_book])
        {
            case BookNameEnum.HongLouMeng:
                {
                    InsWords(AllSkills.hlmList_adj,WordKindEnum.adj);
                    InsWords(AllSkills.hlmList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.hlmList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    InsWords(AllSkills.crystalList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.crystalList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.crystalList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.ZooManual:
                {
                    InsWords(AllSkills.animalList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.animalList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.animalList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    InsWords(AllSkills.maYiDiGuoList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.maYiDiGuoList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.maYiDiGuoList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    InsWords(AllSkills.liuXingBXList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.liuXingBXList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.liuXingBXList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    InsWords(AllSkills.aiJiShenHuaList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.aiJiShenHuaList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.aiJiShenHuaList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    InsWords(AllSkills.humanList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.humanList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.humanList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.Salome:
                {
                    InsWords(AllSkills.shaLeMeiList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.shaLeMeiList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.shaLeMeiList_verb, WordKindEnum.verb);
                }
                break;
        }


    }
    #endregion



    /// <summary>
    /// 将书中的所有词条显示出来
    /// </summary>
    void InsWords(List<Type> wordsList, WordKindEnum wordKind)
    {
        switch (wordKind)
        {
            case WordKindEnum.adj:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_adj, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                            obj.transform.parent = wordsArea;
                            obj.transform.localScale = Vector3.one;
                        });
                    }
                }break;
            case WordKindEnum.noun:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_noun, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                            obj.transform.parent = wordsArea;
                            obj.transform.localScale = Vector3.one;
                        });
                    }
                }
                break;
            case WordKindEnum.verb:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_verb, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                            obj.transform.parent = wordsArea;
                            obj.transform.localScale = Vector3.one;
                        });
                    }
                }
                break;


        }

               
    }


    /// <summary>
    /// 将显示出来的所有词条收回
    /// </summary>
    void DeleteWords()
    {

        for (int i = wordsArea.childCount-1; i >=0; i--)
        {
            foreach (var a in wordsArea.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(a);
            }
            PoolMgr.GetInstance().PushObj(wordsArea.GetChild(i).gameObject.name, wordsArea.GetChild(i).gameObject);
        }
    
    }
  


    /// <summary>
    /// 玩家在查看词条界面点击了按钮【确认】，回到游戏，界面消失，加入词条
    /// </summary>
    public void ClickYes()
    {
        //GameMgr.instance.AddBookList(book[bookOriNumber]);
    }

    /// <summary>
    /// 玩家在查看词条界面点击了按钮【返回】，词条界面清空，回到书本选择界面
    /// </summary>
    public void ClickReturn()
    {
        hasEnter = false;
        DeleteWords();

        chooseBooksParent.GetChild(0).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(1).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(2).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(bookOriNumber).position = bookOriPos;
    }

    public void MouseEnterWord()
    {
      
    }
}
