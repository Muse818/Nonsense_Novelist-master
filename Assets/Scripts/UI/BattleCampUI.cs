using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using UnityEngine;
using UnityEngine.UI;

public class BattleCampUI_Left : BattleUI
{
    protected bool mEnabled = false;
    protected bool mRegistered = false;

    public BattleCampUI_Left()
    {
        mUIContent = new CampUIContent();
    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Load<GameObject>("UI/Battle/leftCampHp");
        mUIContent.CamphpSlider1 = mUIPanel.transform.Find("Hp").GetComponent<Slider>();
        mUIContent.CamphpSlider1.value = 1;
    }
    public class CampUIContent
    {
        public Slider CamphpSlider1 = null;
    }

    protected CampUIContent mUIContent;
    public CampUIContent UIContent => mUIContent;
    public override void Init()
    {
        EventManager.Subscribe<BattleCamp,float>(EventEnum.ChangeCampHp, OnCampHpChanged);
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void LateUpdate(float deltaTime)
    {
       
    }

    public void OnCampHpChanged(BattleCamp camp,float hppersent) 
    {
        if (camp == BattleCamp.Camp1)
        {
            UIContent.CamphpSlider1.value = hppersent;
        }
    }
}
public class BattleCampUI_Right : BattleUI
{
    protected bool mEnabled = false;
    protected bool mRegistered = false;

    public BattleCampUI_Right()
    {
        mUIContent = new CampUIContent();
    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Load<GameObject>("UI/Battle/rightCampHp");
        mUIContent.CamphpSlider1 = mUIPanel.transform.Find("Hp").GetComponent<Slider>();
        mUIContent.CamphpSlider1.value = 1;
    }
    public class CampUIContent
    {
        public Slider CamphpSlider1 = null;
    }

    protected CampUIContent mUIContent;
    public CampUIContent UIContent => mUIContent;
    public override void Init()
    {
        EventManager.Subscribe<BattleCamp, float>(EventEnum.ChangeCampHp, OnCampHpChanged);
    }

    public override void Update(float deltaTime)
    {

    }

    public override void LateUpdate(float deltaTime)
    {

    }

    public void OnCampHpChanged(BattleCamp camp, float hppersent)
    {
        if (camp == BattleCamp.Camp2)
        {
            UIContent.CamphpSlider1.value = hppersent;
        }
    }
}