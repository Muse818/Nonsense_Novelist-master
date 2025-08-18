using Spine;
using UnityEngine;
using UnityEngine.UI;
public class BattlePhaseUI_End : BattleUI
{
    public Button okBtn;
    public BattlePhaseUI_End()
    {

    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Load<GameObject>("UI/Battle/phase_end");
    }

    public override void Init()
    {
        base.Init();
        okBtn = UIPanel.transform.Find("Panel/Okbutton").GetComponent<Button>();
        okBtn.onClick.AddListener(OnClickStartButton);
    }

    public void OnClickStartButton()
    {
        okBtn.transform.parent.parent.gameObject.SetActive(false);
    }
    public override void Update(float deltaTime)
    {

    }

    public void RefreshSlider(float amount)
    { }
}