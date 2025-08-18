using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///角色详细信息面板（挂在该面板的预制体本身）
///</summary>
class CharacterDetails_x : MonoBehaviour
{
    public Text[] texts1;
    public Text[] texts2;
    public Text[] texts3;
    public Text[] texts4;    
    public Image charaName;
    public Image charaImage;
    public Image bookImage;
    public GameObject wordPrefab;
    public Transform nounTF;
    public Transform verbTF;
    private bool isFirstNoun;
    private bool isFirstVerb;
    private bool isFirstAdj;
    public Transform statePanel;
    public GameObject stateImage;

    /// <summary>获取mousedown脚本 </summary>
    private MouseDown mouseDown;

    //废弃参数
    //public GameObject importantPanel;
    //private List<GameObject> importantImage=new List<GameObject>();
    //public GameObject imagePrefab;

    private void Start()
    {
        mouseDown= GameObject.Find("MainCamera").GetComponent<MouseDown>();

        #region 获取重要之人(废弃)
        /*
        //for (int i = 0; i < mouseDown.abschara.importantNum.Count; i++)
        {
            GameObject a = Instantiate(imagePrefab);
            importantImage.Add(a);
            //importantImage[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + "head" + mouseDown.abschara.importantNum[i].ToString());
            //importantImage[i].transform.SetParent(importantPanel.transform);
            a.transform.localScale = new Vector3(1, 1, 1);//必须放在SetParent后面
        }*/
        #endregion

        ///0、首页信息
        //角色名和图片
        charaName.sprite = Resources.Load<Sprite>("FirstStageLoad/" + "Character_" + mouseDown.abschara.Asset.unitName);
        charaImage.sprite = Resources.Load<Sprite>("FirstStageLoad/" + mouseDown.abschara.Asset.unitName);

        //书本图片
        bookImage.sprite = Resources.Load<Sprite>("WordImage/Book/" + mouseDown.abschara.Role.Data.mBook.ToString());
        //print(mouseDown.abschara.bookName.ToString());

        //角色身份（特性）左下角的短纸片
        texts4[0].text = mouseDown.abschara.Asset.roleName;

        //4、背景页信息面板
        texts3[0].text = mouseDown.abschara.Asset.roleInfo;

    }
    private void Update()
    {
        //1、状态页信息面板
        //HP
        texts1[0].text = ((int)mouseDown.absRole.Hp).ToString() + "/" + ((int)mouseDown.absRole.MaxHp).ToString();
        //ATK
        texts1[1].text = IntToString.SwitchATK(mouseDown.absRole.GetAttributeValue(AttributeType.Attack));
        //def
        texts1[2].text = IntToString.SwitchATK(mouseDown.absRole.GetAttributeValue(AttributeType.Def));
        //san
        texts1[3].text = IntToString.SwitchATK(mouseDown.absRole.GetAttributeValue(AttributeType.San));
        //psy
        texts1[4].text = IntToString.SwitchATK(mouseDown.absRole.GetAttributeValue(AttributeType.Psy));

        AdjDetails();


        //2、名词页信息面板
        NounDetails();

        //3、动词页信息面板
        VerbDetails();

    }
    /// <summary>
    /// 2、名词信息面板
    /// </summary>
    private void NounDetails()
    {
        if (!isFirstNoun)
        {
            isFirstNoun = true;
            AbstractItems[] items = mouseDown.abschara.gameObject.GetComponents<AbstractItems>();

            //全部遍历一遍
            for (int i = 0; i < items.Length; i++)
            {
                GameObject word = Instantiate(wordPrefab);
                word.AddComponent(items[i].GetType());
                word.transform.SetParent(nounTF);
                word.transform.localScale = Vector3.one;

                //加载图片

                //同一词的数量
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (items[i].itemID == items[j].itemID)
                    {
                        word.transform.GetChild(0).GetComponent<WordCountText>().count++;
                    }
                }
            }
            //去重
            for (int i = 0; i < nounTF.childCount; i++)
            {
                for (int j = i + 1; j < nounTF.childCount; j++)
                {
                    if (nounTF.GetChild(i).GetComponent<AbstractItems>().itemID == nounTF.GetChild(j).GetComponent<AbstractItems>().itemID)
                    {
                        Destroy(nounTF.GetChild(j).gameObject);
                    }
                }
            }

        }
    }
    /// <summary>
    /// 3、动词页信息面板
    /// </summary>
    private void VerbDetails()
    {
        if (!isFirstVerb)
        {
            isFirstVerb = true;
            AbstractVerbs[] verbs = mouseDown.abschara.gameObject.GetComponents<AbstractVerbs>();

            //全部遍历一遍
            for (int i = 0; i < verbs.Length; i++)
            {
                GameObject word = Instantiate(wordPrefab);
                word.AddComponent(verbs[i].GetType());
                word.transform.SetParent(nounTF);
                word.transform.localScale = Vector3.one;

                //加载图片

                //同一词的数量
                for (int j = i + 1; j < verbs.Length; j++)
                {
                    if (verbs[i].skillID == verbs[j].skillID)
                    {
                        word.transform.GetChild(0).GetComponent<WordCountText>().count++;
                    }
                }
            }
            //去重
            for (int i = 0; i < verbTF.childCount; i++)
            {
                for (int j = i + 1; j < verbTF.childCount; j++)
                {
                    if (verbTF.GetChild(i).GetComponent<AbstractVerbs>().skillID == verbTF.GetChild(j).GetComponent<AbstractVerbs>().skillID)
                    {
                        Destroy(verbTF.GetChild(j).gameObject);
                    }
                }
            }

        }
    }
    /// <summary>
    /// 1、状态栏信息
    /// </summary>
    private void AdjDetails()
    {
        if (!isFirstAdj)
        {
            isFirstAdj = true;
            AbstractAdjectives[] adj = mouseDown.abschara.gameObject.GetComponents<AbstractAdjectives>();

            //全部遍历一遍
            for (int i = 0; i < adj.Length; i++)
            {
                GameObject word = Instantiate(stateImage);
                word.AddComponent(adj[i].GetType());
                word.transform.SetParent(statePanel);
                word.transform.localScale = Vector3.one;

                //加载图片
                //stateImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStage/CharacterDetails_x/ShuXing/"+);
                //同一词的数量
                for (int j = i + 1; j < adj.Length; j++)
                {
                    if (adj[i].adjID == adj[j].adjID)
                    {
                        word.transform.GetChild(0).GetComponent<WordCountText>().count++;
                    }
                }
            }
            //去重
            for (int i = 0; i < statePanel.childCount; i++)
            {
                for (int j = i + 1; j < statePanel.childCount; j++)
                {
                    if (statePanel.GetChild(i).GetComponent<AbstractAdjectives>().adjID == statePanel.GetChild(j).GetComponent<AbstractAdjectives>().adjID)
                    {
                        Destroy(statePanel.GetChild(j).gameObject);
                    }
                }
            }

        }
    }

    /// <summary>
    /// 属性面板的关闭按钮（用预制体挂脚本来赋值）
    /// </summary>
    public void CloseCharaPanel()
    {
        Destroy(this.transform.parent.gameObject);
        Time.timeScale = GameMgr.instance.timeSpeed;
        mouseDown.shoot.GetComponent<Shoot>().enabled = true;

        mouseDown.isShow = false;
    }
}
