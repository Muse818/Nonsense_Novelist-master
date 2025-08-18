using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhaseUI_Rest : BattleUI
{
    protected Button startButton;
    public Transform mCharaPos = null;
    public GameObject mUnitCandiTemp = null;
    protected Text mText = null;

    protected List<UnitCandidateOperator> mCandidates = new List<UnitCandidateOperator>();

    public BattlePhaseUI_Rest()
    {

    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Load<GameObject>("UI/Battle/phase_rest");
        mUnitCandiTemp = AssetManager.Load<GameObject>("UI/Battle/UnitCandidate");
    }

    public override void Init()
    {
        base.Init();

        if (startButton == null)
        {
            startButton = UIPanel.transform.Find("Panel/startBtn").GetComponent<Button>();
            startButton.onClick.AddListener(OnClickStartButton);
        }

        if (mCharaPos == null)
        {
            mCharaPos = GameObject.Find("Panel/charaPos").transform;
        }

        if (mText == null)
        {
            mText = GameObject.Find("Panel/Text").GetComponent<Text>();
        }
    }

    public void OnClickStartButton()
    {
        foreach (var candi in mCandidates)
        {
            if (candi.enabled)
            {
                mText.color = Color.red;
                mText.text = "仍有角色未就位";
                return;
            }
        }

        {
            var camp1 = Battle.CampManager.GetCampMember(BattleCamp.Camp1);
            var camp2 = Battle.CampManager.GetCampMember(BattleCamp.Camp2);
            if (camp1 == null || camp2 == null || camp1.Count == 0 || camp2.Count == 0)
            {
                mText.color = Color.red;
                mText.text = "两方至少要有一名角色";
                return;
            }
        }
        
        Battle.BattlePhase.EnterNextStage();
    }

    public override void Update(float deltaTime)
    {
        
    }

    public void RefreshSlider(float amount)
    {
    }

    protected override void OnEnabled()
    {
        if (Battle.BattlePhase.StageIndex > 0)
        {
            ShuffleCharacter(1);
        }
        else
        {
            ShuffleCharacter(4);
        }
    }

    public void ShuffleCharacter(int count)
    {
        ClearCandidates();
        
        var unitList = Battle.UnitDeck.ShuffleUnit(count);

        for (int i = 0; i < unitList.Count; i++)
        {
            PutToSlot(unitList[i], i);
        }
    }

    public void CreateCharacter(int kind)
    {
        ClearCandidates();
        
        PutToSlot(kind, 1);
    }

    public void PutToSlot(int kind, int i)
    {
        var data = BattleUnitTable.Find(kind);
        if (data == null)
            return;
        
        var asset = AssetManager.Load<BattleUnitSO>("SO/BattleUnit", data.mAsset);

        GameObject candiObj = null;
        GameObject temp = asset.uiPrefab;
        if (asset.uiPrefab != null)
        {
            candiObj = Object.Instantiate(asset.uiPrefab, mCharaPos.GetChild(i), false);
        }
        else
        {
            candiObj = Object.Instantiate(mUnitCandiTemp, mCharaPos.GetChild(i), false);
            candiObj.transform.Find("Mask").GetComponent<Image>().sprite = AssetUtils.ToSprite(asset.sprite);
            candiObj.transform.Find("Icon").GetComponent<Image>().sprite = AssetUtils.ToSprite(asset.sprite);
        }
        
        var candi = candiObj.GetComponent<UnitCandidateOperator>();
        candi.Setup(kind);
        mCandidates.Add(candi);
    }

    public void ClearCandidates()
    {
        foreach (var candi in mCandidates)
        {
            Object.Destroy(candi.gameObject);
        }

        mCandidates.Clear();
    }
}