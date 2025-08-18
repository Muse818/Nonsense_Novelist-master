using UnityEngine;
public class BattlePhaseUI_Battle : BattleUI
{
    public BattlePhaseUI_Battle()
    { 
  
    } 

    protected override void CreateUIPanel()
    {
        mUIPanel=ResMgr.GetInstance().Load<GameObject>("UI/Battle/phase_battle");
    }
    
    public override void Init() 
    {
        base.Init();
       
    }

    
    public override void Update(float deltaTime) 
    {
        
    }

    public void RefreshSlider(float amount)
    {}
}