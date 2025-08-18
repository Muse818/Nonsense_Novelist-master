using UnityEngine.UI;
using UnityEngine;

///<summary>
///全局按钮管理（挂在摄像机上）
///主要用于新游戏界面的下一页按钮管理
///</summary>
class ButtonManager : MonoBehaviour
{
    /// <summary>红楼梦</summary>
    private GameObject libraryHLM;
    /// <summary>红楼梦词库面板是否打开</summary>
    private bool isLibraryHLM = false;
    /// <summary>书本选择</summary>
    public Button selectBook;
    /// <summary>查看词库</summary>
    public Button seeWorLibrary;
    /// <summary>故事导入</summary>
    public Button intoStory;
    /// <summary>获得初始词条</summary>
    public Button getWord;
    /// <summary>获取幸运值</summary>
    public Button getLuckyValue;
    /// <summary>选择章节</summary>
    public Button selectChapter;
    /// <summary>第二章选择书本</summary>
    public Button two_selectBook;
    /// <summary>第二章接下来</summary>
    public Button two_nextNext;
    public PanelManager panelManager;
    public Image guide;

    private void Start()
    {
        libraryHLM = GameObject.Find("AllWordPanelF");
    }
    /// <summary>
    /// 词库按钮
    /// </summary>
    public void OpenWordLibrary()
    {
        libraryHLM.transform.GetChild(0).gameObject.SetActive(true);
        isLibraryHLM = true;
    }
    /// <summary>
    /// 背后的黑幕按钮
    /// </summary>
    public void BlackPanelButton()
    {
        if (isLibraryHLM)
        {
            libraryHLM.transform.GetChild(0).gameObject.SetActive(false);
            isLibraryHLM = false;
        }
    }
    /// <summary>
    /// 书本选择按钮
    /// </summary>
    public void Next_Select()
    {
        selectBook.interactable = true;
        //向上动画
        RectTransform rectTransform = selectBook.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons1, panelManager.originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons1.Length; i++)
        {
            if (panelManager.buttons1[i].name == selectBook.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages1);
                if (panelManager.Pages1[i].gameObject.activeSelf == false)
                    panelManager.Pages1[i].gameObject.SetActive(true);
            }
            //打开故事导入面板时，打开第二组面板
            /*if (panelManager.buttons1[i].name == panelManager.buttons1[3].name)
            {
                panelManager.OpenPanel(panelManager.Pages2, panelManager.Pages2[0], panelManager.buttons2[0].GetComponent<RectTransform>(), panelManager.g_btnUpY);
            }*/
        }
    }
    /// <summary>
    /// 查看词库按钮
    /// </summary>
    public void Next_SeeBook()
    {
        if (EntryDrawBox.count == 2)//选中两本书
        {
            seeWorLibrary.interactable = true;
            //向上动画
            RectTransform rectTransform = seeWorLibrary.GetComponent<RectTransform>();
            panelManager.AllButtonsDown(panelManager.buttons1, panelManager.originalY);
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.btnUpY, rectTransform.localPosition.z);

            //panel
            for (int i = 0; i < panelManager.buttons1.Length; i++)
            {
                if (panelManager.buttons1[i].name == seeWorLibrary.name)
                {
                    panelManager.CloseAllPanels(panelManager.Pages1);
                    if (panelManager.Pages1[i].gameObject.activeSelf == false)
                        panelManager.Pages1[i].gameObject.SetActive(true);
                }
                //打开故事导入面板时，打开第二组面板
                if (panelManager.buttons1[i].name == panelManager.buttons1[3].name)
                {
                    panelManager.OpenPanel(panelManager.Pages2, panelManager.Pages2[0], panelManager.buttons2[0].GetComponent<RectTransform>(), panelManager.g_btnUpY);
                }
            }
        }
    }
    /// <summary>
    /// 故事导入按钮
    /// </summary>
    public void Next_IntoStory()
    {
        intoStory.interactable = true;
        //向上动画
        RectTransform rectTransform = intoStory.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons1, panelManager.originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons1.Length; i++)
        {
            if (panelManager.buttons1[i].name == intoStory.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages1);
                if (panelManager.Pages1[i].gameObject.activeSelf == false)
                    panelManager.Pages1[i].gameObject.SetActive(true);
            }
            //打开故事导入面板时，打开第二组面板
            if (panelManager.buttons1[i].name == panelManager.buttons1[3].name)
            {
                panelManager.OpenPanel(panelManager.Pages2, panelManager.Pages2[0], panelManager.buttons2[0].GetComponent<RectTransform>(), panelManager.g_btnUpY);
            }
        }
    }
    
    /// <summary>
    /// 获取幸运值按钮
    /// </summary>
    public void Next_GetLuckyValue()
    {
        getLuckyValue.interactable = true;
        //向上动画
        RectTransform rectTransform = getLuckyValue.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == getLuckyValue.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }            
        }
    }
    /// <summary>
    /// 选择章节按钮
    /// </summary>
    public void Next_SelectChapter()
    {
        selectChapter.interactable = true;
        //向上动画
        RectTransform rectTransform = selectChapter.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == selectChapter.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }
            
        }
    }
    /// <summary>
    /// 获得初始词条按钮
    /// </summary>
    public void Next_GetInitialWord()
    {
        getWord.interactable = true;
        //向上动画
        RectTransform rectTransform = getWord.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == getWord.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }
        }
    }

    public void DestroyGuide()
    {
        Destroy(guide);
    }
    /// <summary>
    /// 选择书本按钮
    /// </summary>
    public void Next_SelectBook()
    {
        two_selectBook.interactable = true;
        //向上动画
        RectTransform rectTransform = two_selectBook.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == two_selectBook.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }

        }
    }
    /// <summary>
    /// 接下来按钮
    /// </summary>
    public void Next_NextChapter()
    {
        two_nextNext.interactable = true;
        //向上动画
        RectTransform rectTransform = two_nextNext.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == two_nextNext.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }

        }
    }
}
