using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 挂在每个句子组件身上。控制句子的拖拽，记录是否被修改、是否被删除等状态
/// </summary>
public class DragDraftText : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    //拖拽用
    private Vector3 originPos;
    private RectTransform rectTransform;
    private Vector3 clickOffset;

    //关联
    private Transform parent;
    private DraftUi draftUi;

    //canDrag在进入黒墨水状态时开启；CanBegin在墨水次数大于0时开启
    [HideInInspector]public bool canDrag = false;
    private bool canBegin=false;

    //外部红墨水和蓝墨水调用
    [HideInInspector] public bool hasChange = false;
    [HideInInspector] public bool hasDelete = false;
    [HideInInspector] public bool canDelete = false;

    public int index;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
        //if (draftUi == null) Debug.LogWarning("draftUi找不到");
    }

    public void RefreshIndex()
    {
        index=transform.GetSiblingIndex();
    }
    #region 拖拽事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = this.transform.parent;
        draftUi = parent.parent.GetComponent<DraftUi>();
        if (!canDrag) return;
        if (!draftUi.IsInkEnough(0)) { canBegin = false; return; }
        else  canBegin = true;

        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        clickOffset = pos- rectTransform.position;
        originPos = rectTransform.position;

        parent.GetComponent<VerticalLayoutGroup>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (!canDrag) return;
        if (!canBegin) return;

        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos- clickOffset;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        if (!canDrag) return;
        if (!canBegin) return;

        parent = this.transform.parent;
        draftUi = parent.parent.GetComponent<DraftUi>();
        //判断是否在某个ui之上
        int changeIndex=-1;
        int index = parent.childCount-1;


        while (index>=0&& (changeIndex==-1))
        {
            if (parent.GetChild(index) != this.transform)
            {
               if(originPos.y >= parent.GetChild(index).GetComponent<RectTransform>().position.y)
                {
                    if ( rectTransform.position.y <= parent.GetChild(index).GetComponent<RectTransform>().position.y)
                    {
                        
                        changeIndex = index;
                        this.transform.SetSiblingIndex(index);
                        draftUi.ChangeIndexContent(this.index, index);
                        draftUi.RefreshIndex();
                        draftUi.UseInkOnce(0);
                    } 
                }
           
            }
            index--;
        }


         index =0;
        while (index<parent.childCount && (changeIndex == -1))
        {
            if (parent.GetChild(index) != this.transform)
            {
                if (originPos.y <= parent.GetChild(index).GetComponent<RectTransform>().position.y)
                {

                    if (rectTransform.position.y >= parent.GetChild(index).GetComponent<RectTransform>().position.y)
                    {

                        changeIndex = index;
                        this.transform.SetSiblingIndex(index);
                        draftUi.ChangeIndexContent(this.index, index);
                        draftUi.RefreshIndex();
                        draftUi.UseInkOnce(0);
                    }
                }
            }
            index++;
        }
        //归零
        rectTransform.position = originPos;
        parent.GetComponent<VerticalLayoutGroup>().enabled = true;


    }

    #endregion


    #region 点击事件
    string _text;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canDelete) return;

        if (this.transform.Find("showText").GetComponent<TextMeshProUGUI>().text[0] == '<')
        { hasDelete = true; } else { hasDelete = false; }

        parent = this.transform.parent;
        draftUi = parent.parent.GetComponent<DraftUi>();
        if ((!hasDelete)&&(draftUi.IsInkEnough(1)))
        {
           _text = this.transform.Find("showText").GetComponent<TextMeshProUGUI>().text;
            this.transform.Find("showText").GetComponent<TextMeshProUGUI>().text = "<s>" + _text + "</s>";
            draftUi.UseInkOnce(1);
            hasDelete = true;
            return;
        }
        if (hasDelete && (draftUi.IsInkEnough(1)))
        {
          
            this.transform.Find("showText").GetComponent<TextMeshProUGUI>().text = _text;
            draftUi.UseInkOnce(1);
            hasDelete = false;
            return;
        }

    }
    #endregion

}
