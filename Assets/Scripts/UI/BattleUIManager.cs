using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : UIManager
{
    public List<BattleUI> BattleUiDic = new List<BattleUI>();

    public BattleUIManager()
    {
    }

    protected Canvas mBattleCanvas;
    public Canvas BattleCanvas => mBattleCanvas;

    public T Add<T>(T ui) where T : BattleUI
    {
        if (BattleCanvas == null)
        {
            Debug.Log("BattleCanvas is null");
            return null;
        }

        ui.Setup(this);
        BattleUiDic.Add(ui);
        
        ui.SetActive(false);
        
        return ui;
    }

    public void ShowPanel(BattleUI ui)
    {
        if (BattleUiDic.Contains(ui))
        {
            ui.SetActive(true);
        }
    }

    public void Hide(BattleUI ui)
    {
        if (BattleUiDic.Contains(ui))
        {
            ui.SetActive(false);
        }
    }

    public override void Init()
    {
        mBattleCanvas = GameObject.Find("BattleUICanvas")?.GetComponent<Canvas>();
        if (mBattleCanvas == null)
        {
            GameObject uiCanvas = new GameObject("BattleUICanvas");
            mBattleCanvas = uiCanvas.AddComponent<Canvas>();
            mBattleCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
            canvasRect.anchorMin = Vector2.zero; // 锚点左下角
            canvasRect.anchorMax = Vector2.one; // 锚点右上角
            canvasRect.offsetMin = Vector2.zero; // 边距归零
            canvasRect.offsetMax = Vector2.zero; // 边距归零
        }

    }

    public override void Update(float deltaTime)
    {
        foreach (var ui in BattleUiDic)
        {
            ui.Update(deltaTime);
        }
    }

    public override void LateUpdate(float deltaTime)
    {
        foreach (var ui in BattleUiDic)
        {
            ui.LateUpdate(deltaTime);
        }
    }

}
