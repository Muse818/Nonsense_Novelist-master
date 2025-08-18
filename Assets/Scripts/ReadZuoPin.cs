using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 挂在草稿本母节点上。
/// </summary>
public class ReadZuoPin : MonoBehaviour
{
    List<string> content = new List<string>();
    // 不换行的的空格符
    public static readonly string NO_BREAKING_SPACE = "\u00A0";//"\u3000";

    private string sentenseAdr = "UI/draftSentence";
    private GameObject sentenseObj;
    private Transform parent;
    bool select = false;

    private TextMeshProUGUI bookName;

    //转行计算
    float sizeWidth;
    float sizeFont;

    //页数相关
    TextMeshProUGUI textPage;
    public int maxPage = 1;
    public int nowPage = 1;
    List<int> pageCount = new List<int>();
    List<Transform> pageChild = new List<Transform>();

    private void Awake()
    {
        sentenseObj = ResMgr.GetInstance().Load<GameObject>(sentenseAdr);
        parent = this.transform.Find("Panel");

        foreach (var _a in GetComponentsInChildren<TMP_InputField>())
        {
            _a.text = "";
            _a.transform.Find("showText").gameObject.SetActive(true);
            _a.transform.Find("editText").gameObject.SetActive(false);
            _a.caretColor -= new Color(0, 0, 0, 1);
        }

        select = false;
        bookName= this.transform.Find("bookName").GetComponent<TextMeshProUGUI>();
        textPage = this.transform.Find("page").GetComponent<TextMeshProUGUI>();

        //预制体的大小和字号大小，计算转行用
        sizeWidth = (sentenseObj.transform.Find("showText").GetComponent<RectTransform>().rect.width);
        sizeFont = (sentenseObj.transform.Find("showText").GetComponent<TextMeshProUGUI>().fontSize);
    }

    #region button绑定

    public void SetContent(SaveArticle _index)
    {
        content = _index.content;
        bookName.text = _index.title;
    }

    /// <summary>开启草稿本界面 </summary>
    public void openDraft()
    {
        this.gameObject.SetActive(true);
        
        InitDraft();
    }
    /// <summary>关闭草稿本界面 </summary>
    public void closeDraft()
    {
    

        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            if (parent.GetChild(i).GetComponent<DragDraftText>().hasDelete)
            {
                content.RemoveAt(parent.GetChild(i).GetComponent<DragDraftText>().index);

            }
            PoolMgr.GetInstance().PushObj(sentenseObj.name, parent.GetChild(i).gameObject);
        }
        this.gameObject.SetActive(false);

    }
    /// <summary>草稿本界面初始化 </summary>
    void InitDraft()
    {

        //遍历所有的句子，找到对应的行数
        maxPage = 1;
        pageCount.Clear();
        pageCount.Add(0);
        pageChild.Clear();

        //生成句子,绑定组件
        for (int i = 0; i < content.Count; i++)
        {
            PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
            {
                obj.transform.parent = parent;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<DragDraftText>().index = i;

                var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                var _inputField = obj.GetComponent<TMP_InputField>();

                _showText.text = content[i];
              

                //转行            
                var count = Mathf.Floor((sizeFont * (content[i].Length)) / (sizeWidth - _showText.margin.x));
                for (int x = 0; x < count - 1; x++)
                {
                    _inputField.text += "\n";
                }
                _inputField.text += "\n";

                //刷新页面并且计算页数
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);

                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
               
                if ((RectTransformUtility.WorldToScreenPoint(Camera.main, obj.GetComponent<RectTransform>().position)).y <= 50f)
                {
                    maxPage += 1;
                    pageCount.Add(i);
                }
                pageChild.Add(obj.transform);
            });

            //将现在的页数设为最大页数，隐藏其它页数的句子。
            nowPage = maxPage;

            ShowPageSentences(nowPage);

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
    }
    /// <summary>展示_page页的句子 </summary>
    public void ShowPageSentences(int _page)
    {
        textPage.text = nowPage + " / " + maxPage;
 
        //nowpage\_page\maxpage都是从1开始的）
        if (_page < 0 || _page > maxPage)
        {
            print("调用页码出错");
            return;
        }

        if (_page == maxPage)
        {
            for (int x = 0; x < pageChild.Count; x++)
            {
                if (x < pageCount[nowPage - 1])
                {
                    pageChild[x].gameObject.SetActive(false);
                }
                else
                { pageChild[x].gameObject.SetActive(true); }
            }
        }
        else if (_page == 1)
        {
            for (int x = 0; x < pageChild.Count; x++)
            {
                if (x >= pageCount[nowPage])
                {
                    pageChild[x].gameObject.SetActive(false);
                }
                else
                { pageChild[x].gameObject.SetActive(true); }
            }
        }
        else
        {
            for (int x = 0; x < pageChild.Count; x++)
            {
                if ((x <= pageCount[nowPage - 1]) || (x >= pageCount[nowPage]))
                {
                    pageChild[x].gameObject.SetActive(false);
                }
                else
                { pageChild[x].gameObject.SetActive(true); }
            }
        }
    }
    public void NextPage()
    {
        if (nowPage == maxPage) return;
        nowPage++;
        ShowPageSentences(nowPage);
    }
    public void LastPage()
    {
        if (nowPage == 1) return;
        nowPage--;
        ShowPageSentences(nowPage);
    }
    public void RefreshIndex()
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            parent.GetChild(i).GetComponent<DragDraftText>().RefreshIndex();
        }
    }

    #endregion
}



