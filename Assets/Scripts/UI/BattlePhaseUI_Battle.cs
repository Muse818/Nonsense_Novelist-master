using System.Collections.Generic;
using UnityEngine;
public class BattlePhaseUI_Battle : BattleUI
{
    protected DeckView mDeckView = null;
    
    public BattlePhaseUI_Battle()
    { 
  
    }

    protected override void CreateUIPanel()
    {
        mUIPanel = ResMgr.GetInstance().Load<GameObject>("UI/Battle/phase_battle");
    }
    
    public override void Init() 
    {
        mDeckView = mUIPanel.transform.Find("Panel/ShootTime_New").GetComponent<DeckView>();
        mDeckView.Setup(Battle.CardDeckManager);
    }
    
    public override void Update(float deltaTime) 
    {
        
    }

    public void RefreshSlider(float amount)
    {}
}