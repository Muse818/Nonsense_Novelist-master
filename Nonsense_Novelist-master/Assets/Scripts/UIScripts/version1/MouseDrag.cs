using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
///<summary>
///鼠标拖拽
///</summary>
class MouseDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>图像位置</summary>
    private RectTransform rectTrans;
    /// <summary>卡槽父物体位置</summary>
    private Transform gridPanel;
    /// <summary>卡槽父物体位置（用于测试）</summary>
    private Transform gridPanelForTest;
    /// <summary>词条父物体位置</summary>
    private Transform wordPanel;
    /// <summary>词条父物体位置</summary>
    private Transform testPanel;
    /// <summary>CanvasGroup组件</summary>
    private CanvasGroup canvasGroup;
    /// <summary>词条身上的技能脚本</summary>
    private AbstractWord0 absWord;

    /// <summary>形容词的加载圆圈</summary>
    public GameObject adjCircle;
    /// <summary>动词的加载圆圈</summary>
    public GameObject verbCircle;
    /// <summary>形容词圆圈加载的位置</summary>
    private Transform parentCircleTF;
    /// <summary>音效summary>
    private AudioSource audioSource;
    /// <summary>音效summary>
    private AudioSource audioSource_cantuse;
    /// <summary>词条详情</summary>
    public GameObject wordDetail;
    private GameObject otherCanvas;
    /// <summary>词条</summary>
    public static GameObject bulletInstance;
    /// <summary>存储WordCollisionShoot的词条属性</summary>
    public static AbstractWord0 abs;
    /// <summary>词条</summary>
    public GameObject bullet;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        absWord = GetComponent<AbstractWord0>();
        otherCanvas = GameObject.Find("MainCanvas");
        FindGrid();
        if (SceneManager.GetActiveScene().name == "Combat")
        {
            audioSource = GameObject.Find("AudioSource_wirte").GetComponent<AudioSource>();
            audioSource_cantuse = GameObject.Find("AudioSource_CantUse").GetComponent<AudioSource>();
        }
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
    }
    /// <summary>
    /// 正在拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //矫正鼠标位置偏移
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "MainCanvas")
            {
                rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        //若未使用，则回到最初位置
        if (rectTrans != null)
        {
            //FindGrid();
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            #region 版本一：将UI词条拖拽到角色身上

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                //播放词条拖拽上去的音效
                audioSource.Play();
                AbstractCharacter character = hit.collider.gameObject.GetComponent<AbstractCharacter>();

                //判断该词条是形容词还是动词
                if (absWord.GetType() == typeof(AbstractVerbs))
                {
                    AbstractVerbs b = this.GetComponent<AbstractVerbs>();

                    //if(CanUseVerb(character, b))//如果能够使用到角色身上
                    //{
                    //character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);
                    //character.realSkills = character.GetComponents<AbstractVerbs>();
                    Destroy(this.gameObject);
                    //}
                   //     else//不能
                   // {
                   //     audioSource_cantuse.Play();
                   // }
                }
                else if (absWord.GetType() == typeof(AbstractAdjectives))
                {
                   // if (CanUseAdj(character, absWord.GetComponent<AbstractAdjectives>()))
                   // {
                        this.GetComponent<AbstractAdjectives>().UseAdj(hit.collider.gameObject.GetComponent<AbstractCharacter>());
                        Destroy(this.gameObject);
                  //  }
                  //      else
                  //  {
                  //      audioSource_cantuse.Play();
                  //  }
                }
            }
            #endregion

        }
    }
    
    

    /// <summary>
    /// 将词条位置与卡槽位置相匹配
    /// </summary>
    public void FindGrid()
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "MainCanvas")
            {
                gridPanel = canvas.transform.Find("GridPanel");
                wordPanel = canvas.transform.Find("WordPanel");
                testPanel = canvas.transform.Find("TestPanel");
                gridPanelForTest = canvas.transform.Find("GridPanelForTest");
            }
        }
        if (wordPanel!=null)
        {
            for (int i = 0; i < wordPanel.childCount; i++)
            {
                wordPanel.GetChild(i).position = gridPanel.GetChild(i).position;
            }
            for (int i = 0; i < testPanel.childCount; i++)
            {
                testPanel.GetChild(i).position = gridPanelForTest.GetChild(i).position;
            }
        }       
    }
    /// <summary>
    /// 查看词条详细信息
    /// </summary>
    public void ShowDetails()
    {
        //获取背景板
        Transform a = Instantiate(wordDetail, otherCanvas.transform).transform.GetChild(0).GetChild(0);
        a.transform.GetChild(0).GetComponent<Text>().text = this.GetComponent<AbstractWord0>().wordName;
        a.transform.GetChild(1).GetComponent<Text>().text = this.GetComponent<AbstractWord0>().description;
        Time.timeScale = 0;
    }
    /// <summary>
    /// 关闭当前面板
    /// </summary>
    public void ClosePanel()
    {
        this.transform.parent.gameObject.SetActive(false);
        Time.timeScale = GameMgr.instance.timeSpeed;
        
    }
    
    
}
