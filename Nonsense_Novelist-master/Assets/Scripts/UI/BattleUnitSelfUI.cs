using UnityEngine;
using UnityEngine.UI;

public class BattleUnitSelfUI : BattleUI
{
    protected bool mEnabled = false;
    protected bool mRegistered = false;

    public BattleUnitSelfUI(BattleUnit _role)
    {
        mOwner = new UIOwner();
        mOwner.mUnit = _role;
        mOwner.mUnit.infoUI = this;
        mOwner.mUnitPos = mOwner.mUnit.UnitView.Root;

        mUIContent = new UnitUIContent();
    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Load<GameObject>("UI/Battle/battleUnitSelfUI");
        mUIContent.hpSlider = mUIPanel.transform.Find("HP").GetComponent<Slider>();
    }
    
    public class UIOwner
    {
        public BattleUnit mUnit = null;
        public Transform mUnitPos;

    }

    protected UIOwner mOwner;

    public UIOwner Owner
    {
        set
        {
            mOwner = value;
            mRegistered = true;
            OnRegistered();
        }

        get => mOwner;
    }
    
    public class UnitUIContent
    {
        public Slider hpSlider = null;

    }
    
    protected UnitUIContent mUIContent;
    public UnitUIContent UIContent => mUIContent;
    
    public Vector3 offset = new Vector3(-0.3f, 1.0f, 0);

    public bool IsRegistered => mRegistered;

    protected virtual void OnRegistered()
    {
    }

    public override void Init()
    {

    }

    public override void Update(float deltaTime)
    {
        FollowPlayerPos();
    }

    public override void LateUpdate(float deltaTime)
    {
        UIContent.hpSlider.value = Owner.mUnit.HpPercent;
    }


    protected void FollowPlayerPos()
    {
        if (Owner == null || !Owner.mUnit.IsValid())
            return;
        
        Vector3 worldPosition = Owner.mUnitPos.position + offset;
        Vector3 screenPosition = UIStatics.WorldToUIPosition(worldPosition);
        
        UIPanel.transform.localPosition = screenPosition;
        UIPanel.transform.localScale = Vector3.one * 0.1f;
    }
}
