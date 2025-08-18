using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    [Header("(手动设置)panel1")]
    public Transform panel1;//命名
    private string titleName;
    public Text textInput;

    [Header("(手动设置)panel2")]
    public Transform panel2;//查看书本
    public Text title;
    public GameObject closeBook;
    public GameObject openBook;
    public Text contentL;
    public Text contentR;
    public int indexPage=0;

    private string[] content;
    private string content_string;
    public int lineWords = 18;
    public int lineCount = 13;
    private Dictionary<int, int> pageContent = new Dictionary<int, int>();

    [Header("(手动设置)panel3")]
    public Transform panel3;//收到信件
    public GameObject letterBig;
    private Animator letetrAnim;
    public GameObject letetrScroe;//信件得分
    public Scrollbar scroll;
    private Vector3 letterScroePos;
    private void Awake()
    {
        //初始化，打开panel1关闭其它
        ChangePanel(1);

        //赋值
        letetrAnim = letterBig.GetComponent<Animator>();


        //
        this.GetComponent<Canvas>().worldCamera = Camera.main;
      
        content = GameMgr.instance.draftUi.MergeContent_B();
        content_string = GameMgr.instance.draftUi.MergeContent_A();
       // CalculateAllPageIndex();
    }

    /// <summary>
    /// 换到panel几？_panel=1234
    /// </summary>
    /// <param name="_panel"></param>
    private void ChangePanel(int _panel)
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);
      
        switch (_panel)
        {
            case 1:
                {
                    panel1.gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    panel2.gameObject.SetActive(true);
                    //当前页数为最大页
                    InitContent();
                    //RefreshPanal2();
                }
                break;
            case 3:
                {
                    panel3.gameObject.SetActive(true);
                }
                break;
        
        }
    }


    #region draftUI移植
    public GameObject sentenseObj;
    public Transform parentL;
    public Transform parentR;
    List<string> _content = new List<string>();
    int maxPage = 0;//最大页数（实际）
     int nowPage = 0;//现在页数（0开始）
     List<int> pageCountL = new List<int>();//在最开始不加0
     List<int> pageCountR = new List<int>();
    List<Transform> pageChildL = new List<Transform>();
    List<Transform> pageChildR= new List<Transform>();

    public TextMeshProUGUI textPagel;

    public TextMeshProUGUI textPager;
    //转行计算
    float sizeWidth;
    float sizeFont;
    bool isleftNOW = true;
    private void InitContent()
    {
        sizeWidth = (sentenseObj.transform.Find("showText").GetComponent<RectTransform>().rect.width);
        sizeFont = (sentenseObj.transform.Find("showText").GetComponent<TextMeshProUGUI>().fontSize);
        maxPage = 0;
        _content = GameMgr.instance.draftUi.content;

        //遍历所有的句子，找到对应的行数
        pageCountL.Clear(); pageCountR.Clear();
        pageChildL.Clear(); pageChildR.Clear();

        int _l = 0;
        int _r = 0;
        //生成句子,绑定组件
        for (int i = 0; i < _content.Count; i++)
        {
            if (isleftNOW)//在左边的界面计算
            {
              
                PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
                {
                    obj.transform.parent = parentL;
                    obj.transform.localScale = Vector3.one;

                    var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                    var _inputField = obj.GetComponent<TMP_InputField>();
                    _showText.text = _content[i];

                    //转行            
                    var count = Mathf.Floor((sizeFont * (content[i].Length)) / (sizeWidth - _showText.margin.x));
                    for (int x = 0; x < count - 1; x++)
                    {
                        _inputField.text += "\n";
                    }
                    _inputField.text += "\n";

                    //刷新页面并且计算页数
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentL);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentL);
                    pageChildL.Add(obj.transform);
                    if ((RectTransformUtility.WorldToScreenPoint(Camera.main, obj.GetComponent<RectTransform>().position)).y <= 350f)
                    {
                        maxPage += 1;
                        pageCountL.Add(_l);
                        isleftNOW = false;
                    
                    }
                    _l++;
                });
                nowPage = maxPage - (maxPage % 2);
                ShowPageSentences(nowPage);
            }
            else//右边
            {
                PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
                {
                    obj.transform.parent = parentR;
                    obj.transform.localScale = Vector3.one;

                    var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                    var _inputField = obj.GetComponent<TMP_InputField>();
                    _showText.text = _content[i];

                    //转行            
                    var count = Mathf.Floor((sizeFont * (content[i].Length)) / (sizeWidth - _showText.margin.x));
                    for (int x = 0; x < count - 1; x++)
                    {
                        _inputField.text += "\n";
                    }
                    _inputField.text += "\n";

                    //刷新页面并且计算页数
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentR);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentR);
                    pageChildR.Add(obj.transform);
                    if ((RectTransformUtility.WorldToScreenPoint(Camera.main, obj.GetComponent<RectTransform>().position)).y <= 250f)
                    {
                        maxPage += 1;
                        pageCountR.Add(_r);
                        isleftNOW = true;
                    }
                    _r++;
                });
                nowPage = maxPage - (maxPage % 2);
                ShowPageSentences(nowPage);
            }
        }
            //将现在的页数设为最大页数，隐藏其它页数的句子。
        //nowPage = maxPage-(maxPage%2);
        //ShowPageSentences(nowPage);
      
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentL);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentR);
    }

    private void ShowPageSentences(int _page)
    {
        //textPagel.text = (nowPage+1).ToString() + " / " + (maxPage).ToString();
        textPagel.text = (nowPage + 1).ToString();
        textPager.text = (nowPage + 2).ToString();
        if ((_page < 0) || (_page >= maxPage))//nowpage从0开始，是数组指针;maxPage是Count，从1开始
        {
            print("结算页面翻页出错"+_page);
            return;
        }

        int _maxLpage= maxPage - (maxPage % 2); //最大页数的左边页面的实际对应nowpage
     
        if ((nowPage != 0) && (nowPage < _maxLpage))//中间页数
        {
      
            for (int x = 0; x < pageChildL.Count; x++)//l
            {
                if ((x >= pageCountL[nowPage/2 - 1])&& (x <pageCountL[nowPage  / 2]))
                {
                    pageChildL[x].gameObject.SetActive(true);
                }
                else
                { pageChildL[x].gameObject.SetActive(false); }
            }
            //r
            for (int x = 0; x < pageChildR.Count; x++)
            {
                if ((x >= pageCountR[nowPage  / 2 - 1]) && (x < pageCountR[nowPage  / 2]))
                {
                    pageChildR[x].gameObject.SetActive(true);
                }
                else
                { pageChildR[x].gameObject.SetActive(false); }
            }
        }
        else//第一页或者最后一页
        {
            if (nowPage == 0)//第一页
            {
                if (maxPage == 0)
                {

                    for (int x = 0; x < pageChildL.Count; x++)//L
                    {
                        pageChildL[x].gameObject.SetActive(true);
                    }
                    for (int x = 0; x < pageChildR.Count; x++)
                    {
                        pageChildR[x].gameObject.SetActive(false);
                    }
                }
                else if (maxPage == 1)
                {

                    for (int x = 0; x < pageChildL.Count; x++)//L
                    {
                        pageChildL[x].gameObject.SetActive(true);
                    }
                    for (int x = 0; x < pageChildR.Count; x++)//R
                    {
                        pageChildR[x].gameObject.SetActive(true);
                    }
                }
                else
                {

                    for (int x = 0; x < pageChildL.Count; x++)//L
                    {
                        if (x >= pageCountL[0])//pageCountL[0]本身计入下一页
                        {
                            pageChildL[x].gameObject.SetActive(false);
                        }
                        else
                            pageChildL[x].gameObject.SetActive(true);
                    }
                    for (int x = 0; x < pageChildR.Count; x++)//R
                    {
                        if (x >= pageCountR[0])//pageCountL[0]本身计入下一页
                        {
                            pageChildR[x].gameObject.SetActive(false);
                        }
                        else
                            pageChildR[x].gameObject.SetActive(true);
                    }
                }

            }
          
            else if ((nowPage == _maxLpage) && (maxPage == _maxLpage))//最后一页左边
            {

                for (int x = 0; x < pageChildL.Count; x++)
                {
                    if ((x < pageCountL[(nowPage/ 2 )- 1]))//pageCountL[nowPage / 2]本身计入下一页
                    {
                        pageChildL[x].gameObject.SetActive(false);
                    }
                    else
                    { pageChildL[x].gameObject.SetActive(true); }
                }
                for (int x = 0; x < pageChildR.Count; x++)
                {
                    pageChildR[x].gameObject.SetActive(false);
                }
            }
            else if ((nowPage == _maxLpage) && (maxPage != _maxLpage))//最后一页右边
            {
   
                for (int x = 0; x < pageChildL.Count; x++)//L
                {
                    if ((x < pageCountL[(nowPage / 2)-1]))//pageCountL[nowPage / 2]本身计入下一页
                    {
                        pageChildL[x].gameObject.SetActive(false);
                    }
                    else
                    { pageChildL[x].gameObject.SetActive(true); }
                }
                for (int x = 0; x < pageChildR.Count; x++)//R
                {
                    if ((x < pageCountR[(nowPage/ 2)-1 ]))//pageCountL[nowPage / 2]本身计入下一页
                    {
                        pageChildR[x].gameObject.SetActive(false);
                    }
                    else
                    { pageChildR[x].gameObject.SetActive(true); }
                }
            }
            else
            {
                print("未知情况");
            }

        }

    }


    public void ClickNextPage()
    {
        if (nowPage == (maxPage - (maxPage % 2))) return;
        nowPage+=2;
        ShowPageSentences(nowPage);
    }
    public void ClickLastPage()
    {
        if (nowPage == 0) return;
        nowPage -= 2;
        ShowPageSentences(nowPage);
    }



    #endregion


    #region 废弃显示方法
    //private void RefreshPanal2()
    //{
    //    //页码=0时，左1右2.最大值可能为0也可能为1
    //    if (indexPage + 1 <= pageContent.Count)   //左-单(1)
    //    {
    //        if (!pageContent.ContainsKey(indexPage + 1))
    //        {
    //            contentL.text = content_string.Substring(pageContent[indexPage]);
    //        }
    //        else
    //        {
    //            contentL.text = content_string.Substring(pageContent[indexPage],
    //                pageContent[indexPage + 1] - pageContent[indexPage]);
    //        }
    //    }
    //    else
    //    {
    //        contentL.text = "";
    //    }

    //    if (indexPage +2 <= pageContent.Count) //右-双(2)
    //    {
    //        if (!pageContent.ContainsKey(indexPage + 2))
    //        {
    //            contentR.text = content_string.Substring(pageContent[indexPage + 1]);
    //        }
    //        else
    //        {
    //            contentR.text = content_string.Substring(pageContent[indexPage + 1],
    //                pageContent[indexPage + 2] - pageContent[indexPage + 1]);
    //        }
    //    }
    //    else
    //    {
    //        contentR.text = "";
    //    }
    //}
    //private void CalculateAllPageIndex()
    //{
    //    print(content.Length+ "Length");
    //    int _INDEX = 0;
    //    int _wordIndex = 0;
    //    int o = 0;
    //    pageContent.Add(0, 0); _INDEX++;
    //    foreach (var _c in content)
    //    {
    //        o += Mathf.CeilToInt(_c.Length / (lineWords))+1;
    //        print("o" + o);
    //        if (o >= (lineCount))
    //        {
    //            int i = o - lineCount;//超出的行数
    //            int j = ((_c.Length % (lineWords)) == 0 ? lineWords : (_c.Length % (lineWords))) + Mathf.Clamp((i - 1), 0, lineCount + 1) * lineWords;
    //            int x = _c.Length - j;
    //            _wordIndex += x;

    //            pageContent.Add(_INDEX, _wordIndex);

    //            _INDEX++;
    //            o = 0;
    //            o +=i;
    //            _wordIndex += j; _wordIndex++;
    //        }
    //        else
    //        {
    //            _wordIndex += _c.Length;
    //            _wordIndex++;
    //        }


    //    }

    //    return;
    //}
    //public void LastPage()
    //{
    //    if (indexPage <= 0) return;
    //    indexPage -= 2;
    //    RefreshPanal2();
    //}

    //public void NextPage()
    //{
    //    if (indexPage + 2 >= pageContent.Count) return;
    //    indexPage += 2;
    //    RefreshPanal2();
    //}
    #endregion

    #region 外部点击事件



    public void BackToStudyScene()
    {
        //RecordMgr.instance.AddRecord(titleName, GameMgr.instance.draftUi.content, 2);
        RecordMgr.instance.SaveByJson(titleName, GameMgr.instance.draftUi.content, 2);
        SceneManager.LoadScene("Study");
        PoolMgr.GetInstance().Clear();
        //gameObject.GetComponent<LoadingScene>().EnterNextScene();
    }
    public void ChangeText()
    {
        titleName = textInput.text ;
        
    }
    public void Change1To2()
    { 
        ChangeText();
        ChangePanel(2);
        P2_ClickOpenBook();
        if((titleName==null)||(titleName.Length<1)) title.text = "未命名作品";
        else title.text = titleName;
    }
    public void P2_ClickCloseBook()
    {
        closeBook.SetActive(false);
        openBook.SetActive(true);
    }
    public void P2_ClickOpenBook()
    {
        closeBook.SetActive(true);
        openBook.SetActive(false);
    }
    public void Change2To3()
    {
        ChangePanel(3);

        letterBig.SetActive(false);
        letetrAnim.enabled = true;
        letetrAnim.SetBool("open", false);
        letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
    }
    public void P3_ClickLetter()
    {
        letterBig.SetActive(true);
        letetrAnim.SetBool("open", true);
    }

    Vector3 vector010 = new Vector3(0, 1, 0);
    public void P3_LetterScroll()
    {
        if (letetrAnim != null && (letetrAnim.enabled))
        {
            letetrAnim.enabled=false; letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
        }
       
        print(" scroll.value" + scroll.value);

        letetrScroe.GetComponent<RectTransform>().localPosition = letterScroePos + vector010 * 1200 * scroll.value;
    }
    //在3的动画中调用

    #endregion
}
