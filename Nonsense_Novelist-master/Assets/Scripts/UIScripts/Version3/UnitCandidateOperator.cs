using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCandidateOperator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler
{
    [Header("控制物体")] [SerializeField]
    public GameObject mTarget;

    protected RectTransform mRectTransform;
    protected CanvasGroup mCanvasGroup;
    protected Canvas mCanvas;
    protected Vector2 mOriginalLocalPointerPosition;
    protected Vector2 mOriginalPanelPosition;

    protected int mSpawnKind = 0;
    protected BattleBase mBattle = null;

    protected bool mDragging = false;

    public void Setup(int unitKind)
    {
        mSpawnKind = unitKind;

        mBattle = BattleRunner.Battle;
    }

    void Start()
    {
        if (mTarget == null)
        {
            mTarget = transform.gameObject;
        }

        mRectTransform = mTarget.GetComponent<RectTransform>();
        mCanvas = mTarget.GetComponentInParent<Canvas>();
        mCanvasGroup = mTarget.GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!mDragging && eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameObject.Find("BattleUICanvas/CharacterDetail(Clone)") == null)
            {
                var a = AssetManager.Load<GameObject>("UI/CharacterDetail");
                a.transform.SetParent(GameObject.Find("BattleUICanvas").transform);
                a.transform.localPosition = Vector3.zero;
                a.transform.localScale = Vector3.one;
                //获取点击角色的脚本信息
                a.GetComponentInChildren<CharacterDetail>().Open(mSpawnKind);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        mDragging = true;
        
        mOriginalPanelPosition = mRectTransform.anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mCanvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera, out mOriginalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!mDragging)
            return;
        
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            OnEndDrag(eventData);
            return;
        }
        
        if (mCanvas == null)
            return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mCanvas.transform as RectTransform,
                eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector2 offsetToOriginal = localPointerPosition - mOriginalLocalPointerPosition;
            mRectTransform.anchoredPosition = mOriginalPanelPosition + offsetToOriginal;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!mDragging)
            return;

        mDragging = false;
        mRectTransform.anchoredPosition = mOriginalPanelPosition;

        var unit = mBattle.Stage.SpawnUnit(mSpawnKind, UnitSlotType.BackSeat | UnitSlotType.FrontSeat, ClientUtils.GetMouseWorldPosition());
        if (unit != null)
        {
            enabled = false;
            mTarget.SetActive(false);
        }
    }
}
