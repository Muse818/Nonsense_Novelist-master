using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleUI : UIBase
{
    public BattleBase Battle {
        get => mBattle;
        set => mBattle = value;
    }
    protected BattleBase mBattle;
    public BattleUI() { } 
    
    public override void Init() { }

    public override void Update(float deltaTime) { }

    public override void LateUpdate(float deltaTime) { }

    public void Setup(BattleUIManager uiManager)
    {
        Battle = uiManager.Battle;
        
        CreateUIPanel();
        
        var transform = UIPanel.transform;
        
        transform.SetParent(uiManager.BattleCanvas.transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        var rect = transform as RectTransform;
        if (rect != null)
        {
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
        }
        
        Init();
    }
}
