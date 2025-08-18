using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UI_LAYER
{
    TOP,
    MID,
    DOWN,
}

public class UIBase
{
    protected CanvasGroup mCanvasGroup;
    public CanvasGroup CanvasGroup => mCanvasGroup;

    protected UI_LAYER mLayer;
    public UI_LAYER layer;

    protected GameObject mUIPanel;

    public GameObject UIPanel
    {
        get
        {
            if (mUIPanel == null)
            {
                CreateUIPanel();
            }

            return mUIPanel;
        }
    }

    public UIBase() {}

    protected virtual void CreateUIPanel() {}

    public virtual void Init() {}

    public virtual void Update(float deltaTime) {}

    public virtual void LateUpdate(float deltaTime) {}

    public void SetActive(bool active)
    {
        if (UIPanel.activeSelf == active)
            return;
        
        UIPanel.SetActive(active);
        if (active)
        {
            OnEnabled();
        }
        else
        {
            OnDisabled();
        }
    }
    
    protected virtual void OnEnabled() {}
    protected virtual void OnDisabled() {}
}
