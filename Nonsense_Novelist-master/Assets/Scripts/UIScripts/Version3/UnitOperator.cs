using System;
using UnityEngine;
using System.Collections;

public class UnitOperator : MonoBehaviour
{
    protected UnitViewBase mView = null;
    protected Transform mTarget = null;
    protected Vector3 mOriginPos;
    protected Vector3 mOriginScale;
    // todo : find a better way to change spine color when drag
    
    [Header("拖动表现")]
    public float mAfterClickScale = 0.44f;
    
    protected Vector3 mOffset;
    protected float mZCoordinate;
    
    public void Setup(UnitViewBase unitView)
    {
        mView = unitView;
        mTarget = mView.Root;
        
        enabled = true;
    }

    private void Start()
    {
        Setup(GetComponentInChildren<UnitViewBase>());
    }

    private void OnMouseDown()
    {
        mOriginPos = mTarget.position;
        mOriginScale = mTarget.localScale;
        
        mZCoordinate = Camera.main.WorldToScreenPoint(mOriginPos).z;
        
        //先让点击物体中心与鼠标点击处同步。
        mTarget.position = GetMouseWorldPos();
        
        mTarget.localScale = ScaleWithTure(mOriginScale, mAfterClickScale);
        mOffset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;
        transform.position = newPosition;
    }

    private void OnMouseUp()
    {
        var targetSlot = mView.Role.Battle.Stage.FindClosestSlot(UnitSlotType.BackSeat | UnitSlotType.FrontSeat, GetMouseWorldPos(),
            1.0f, true);
        
        if (targetSlot == null || targetSlot.Unit == mView.Role)
        {
            mTarget.position = mOriginPos;
            mTarget.localScale = mOriginScale;
        }
        else
        {
            var oriSlot = mView.Role.Slot;
            var targetSlotUnit = targetSlot.Unit;
            
            if (targetSlotUnit == null)
            {
                oriSlot.Remove();
                targetSlot.OccupiedBy(mView.Role);
            }
            else
            {
                oriSlot.Remove();
                targetSlot.Remove();
                
                oriSlot.OccupiedBy(targetSlotUnit);
                targetSlot.OccupiedBy(mView.Role);
            }
        }
    }
    
    Vector3 ScaleWithTure(Vector3 scale, float muti)
    {
        return new Vector3(NnMathUtils.GetSign(scale.x), NnMathUtils.GetSign(scale.y), NnMathUtils.GetSign(scale.z)) * muti;
    }
    
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}