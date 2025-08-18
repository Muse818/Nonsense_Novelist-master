using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
///<summary>
///24.1.23策划debug功能拖拽脚本
///</summary>
class TestDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>图像位置</summary>
    private RectTransform rectTrans;
    /// <summary>词条父物体位置</summary>
    private Transform wordPanel;
    /// <summary>词条父物体位置</summary>
    private Transform testPanel;
    /// <summary>CanvasGroup组件</summary>
    private CanvasGroup canvasGroup;
    public Transform originPos;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
            if (canvas.name == "combatCanvas")
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
            this.transform.position = originPos.position;
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        this.transform.position = originPos.position;
    }
}
