
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum BattlePhaseType
{
    Rest = 0, //休息回合
    Lve = 1, //左对抗怪物
    Rve = 2, //右对抗怪物
    Pve = 3, //左右一起对抗怪物
    Pvp = 4 //左右互博
}

public class BattlePhase : BattleModule
{
    protected List<PhaseTable.Data> mPhases = new List<PhaseTable.Data>();
    protected int mStageIndex = -1; //索引从负1开始，第一个回合也使用EnterNextStage
    protected GamePhase mCurrentPhase;

    protected BattlePhaseUI_Rest restUI;
    protected GameObject restUIObj;
    protected BattlePhaseUI_Battle battleUI;
    protected GameObject battleUIObj;
    

    public int StageIndex => mStageIndex;
    
    public PhaseTable.Data CurrentPhase
    {
        get
        {
            if (mStageIndex >= 0 && mStageIndex < mPhases.Count)
            {
                return mPhases[mStageIndex];
            }

            return null;
        }
    }
    
    public BattlePhaseType CurrentPhaseType
    {
        get
        {
            if (mStageIndex >= 0 && mStageIndex < mPhases.Count)
            {
                return mPhases[mStageIndex].mType;
            }
            
            var phase = CurrentPhase;
            if (phase != null)
            {
                return phase.mType;
            }
            
            return BattlePhaseType.Rest;
            
        }
    }
    
    public bool IsCombat => CurrentPhaseType != BattlePhaseType.Rest;

    protected Dictionary<BattleCamp, List<BattleCamp>> mCampEnemies = new Dictionary<BattleCamp, List<BattleCamp>>();
    public Dictionary<BattleCamp, List<BattleCamp>> CampEnemies => mCampEnemies;

    public override void Init()
    {
        //从表格中读取数据，存储在Mphase中
        int temp = 0;
        foreach (var data in PhaseTable.DataList)
        {
            if (data.Value.mChapter == 1) //读取和当前章节一致的回合。还没写章节相关的东西，这里默认1
            {
                mPhases.Add(data.Value);
                temp++;
            }
        }
        
        mCampEnemies.Add(BattleCamp.Camp1, new List<BattleCamp>());
        mCampEnemies.Add(BattleCamp.Camp2, new List<BattleCamp>());
        mCampEnemies.Add(BattleCamp.Boss, new List<BattleCamp>());
    }

    public override void Begin()
    {
        InitPhaseUI();
    }

    public override void PostBegin()
    {
        //进入第一个回合
        EnterNextStage();
    }

    protected void InitPhaseUI()
    {
        if (restUI == null)
        {
            restUI = Battle.BattleUI.Add(new BattlePhaseUI_Rest());
            restUI.Battle = Battle;
        }

        if (battleUI == null)
        {
            battleUI = Battle.BattleUI.Add(new BattlePhaseUI_Battle());
        }

        Battle.BattleUI.Hide(battleUI);
        Battle.BattleUI.Hide(restUI);
    }

    public void ActivePhaseUI(BattlePhaseType type)
    {
        Battle.BattleUI.Hide(battleUI);
        Battle.BattleUI.Hide(restUI);
        switch (type)
        {
            case BattlePhaseType.Rest:
            {
                Battle.BattleUI.ShowPanel(restUI);
            }
                break;
            case BattlePhaseType.Pve:
            case BattlePhaseType.Pvp:
            case BattlePhaseType.Rve:
            case BattlePhaseType.Lve:
            {
                Battle.BattleUI.ShowPanel(battleUI);
            }
                break;
        }
    }

    public GamePhase RegisterPhase(PhaseTable.Data data)
    {
        GamePhase newPhase;
        switch (data.mType)
        {
            case BattlePhaseType.Lve:
            case BattlePhaseType.Pve:
            case BattlePhaseType.Pvp:
            case BattlePhaseType.Rve:
            {
                newPhase = NewBattlePhase(data.mType);
            }
                break;
            case BattlePhaseType.Rest:
            default:
            {
                var restData = new GamePhase_Rest.RestData();
                restData.mEventCount = 3;
                restData.mEventShowTime = 3;
                newPhase = new GamePhase_Rest(restData);
            }
                break;
        }
        
        newPhase.Battle = Battle;
        newPhase.IsRegistered = true;

        return newPhase;
    }

    protected GamePhase_Combat NewBattlePhase(BattlePhaseType type)
    {
        var fightCamp = new List<List<BattleCamp>>();
        
        var camp1List = mCampEnemies[BattleCamp.Camp1];
        var camp2List = mCampEnemies[BattleCamp.Camp2];
        var bossList = mCampEnemies[BattleCamp.Boss];
        
        camp1List.Clear();
        camp2List.Clear();
        bossList.Clear();
        
        switch (type)
        {
            case BattlePhaseType.Lve:
            {
                camp1List.Add(BattleCamp.Boss);
                bossList.Add(BattleCamp.Camp1);
                
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Camp1 });
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Boss });
                break;
            }
            case BattlePhaseType.Pve:
            {
                bossList.Add(BattleCamp.Camp1);
                bossList.Add(BattleCamp.Camp2);
                camp1List.Add(BattleCamp.Boss);
                camp2List.Add(BattleCamp.Boss);
                
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Camp1, BattleCamp.Camp2 });
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Boss });
                break;
            }
            case BattlePhaseType.Pvp:
            {
                camp2List.Add(BattleCamp.Camp1);
                camp1List.Add(BattleCamp.Camp2);
                
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Camp1 });
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Camp2 });
                break;
            }
            case BattlePhaseType.Rve:
            {
                camp2List.Add(BattleCamp.Boss);
                bossList.Add(BattleCamp.Camp2);
                
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Camp2 });
                fightCamp.Add(new List<BattleCamp>() { BattleCamp.Boss });
                break;
            }
        }

        return new GamePhase_Combat(fightCamp, 0);
    }

    public void EnterNextStage()
    {
        if (mCurrentPhase != null)
        {
            mCurrentPhase.Exit();
            mCurrentPhase = null;
        }

        // 进入下一阶段
        mStageIndex++;
        if (mStageIndex >= mPhases.Count)
        {
            Debug.Log("所有阶段已完成！");
            Battle.EndBattle(BattleCamp.None);
            return;
        }

        // 根据类型创建阶段对象
        var nextData = mPhases[mStageIndex];
        mCurrentPhase = RegisterPhase(nextData);
        mCurrentPhase.Start();
        
        // 存档
    }

    public override void Update(float deltaSec)
    {
        if (mCurrentPhase != null)
        {
            if (!mCurrentPhase.DetectPhaseEnd())
            {
                mCurrentPhase.Update(deltaSec);
            }
            else
            {
                mCurrentPhase.Exit();
                mCurrentPhase = null;
            }
        }

        if (mCurrentPhase == null)
        {
            EnterNextStage();
        }
    }
}