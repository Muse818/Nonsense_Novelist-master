using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BattleBase : MonoBehaviour
{
    protected BattleState mState = BattleState.None;
    public BattleState State => mState;
    
    public BattleMode Mode { get; set; }

    protected List<BattleModule> mModules = new List<BattleModule>();

    protected BattleClock mClock = null;
    protected BattleStage mStage = null;
    protected BattleGameState mGameState = null;
    protected BattleObjectManager mObjectManager = null;
    protected BattleObjectFactory mObjectFactory = null;
    protected BattleCampManager mCampManager = null;
    protected CardDeckManager mCardDeckManager = null;
    protected PinBallLauncher mPinBallLauncher = null;
    protected BattlePhase mBattlePhase = null;
    protected BattleScene mBattleScene = null;
    protected BattleUIManager mBattleUI = null;
    protected UnitDeckManager mUnitDeck = null;


    public BattleClock Clock => mClock;
    public BattleStage Stage => mStage;
    public BattleGameState GameState => mGameState;
    public BattleObjectManager ObjectManager => mObjectManager;
    public BattleObjectFactory ObjectFactory => mObjectFactory;
    public BattleCampManager CampManager => mCampManager;
    public CardDeckManager CardDeckManager => mCardDeckManager;
    public PinBallLauncher PinBallLauncher => mPinBallLauncher;
    public BattlePhase BattlePhase => mBattlePhase;
    public BattleScene BattleScene => mBattleScene;
    public BattleUIManager BattleUI => mBattleUI;
    public UnitDeckManager UnitDeck => mUnitDeck;


    public float Now => mClock?.ElapsedSec ?? 0;
    protected float mFixedTimeAccumulated = 0f;
    private const float FixedDeltaTime = 1.0f / 60;
    
    public bool IsFinished => mState == BattleState.End;

    public void Init()
    {
        mState = BattleState.None;
        AddModules();
        InitModules();
    }

    public void Begin()
    {
        mState = BattleState.Inprogress;

        foreach (var module in mModules)
        {
            module.Begin();
        }

        foreach (var module in mModules)
        {
            module.PostBegin();
        }
    }

    public void Tick(float deltaSec)
    {
        if (mState != BattleState.Inprogress)
            return;

        foreach (var module in mModules)
        {
            if (module.IsSupportUpdateInRest || BattlePhase.IsCombat)
            {
                module.Update(deltaSec);
            }
        }

        foreach (var module in mModules)
        {
            if (module.IsSupportUpdateInRest || BattlePhase.IsCombat)
            {
                module.LateUpdate(deltaSec);
            }
        }


        mFixedTimeAccumulated += deltaSec;
        while (mFixedTimeAccumulated >= FixedDeltaTime)
        {
            foreach (var module in mModules)
            {
                module.LateFixedUpdate(FixedDeltaTime);
            }

            mFixedTimeAccumulated -= FixedDeltaTime;
        }
    }

    public void Close()
    {
        foreach (var module in mModules)
        {
            module.Dispose();
        }

        mModules.Clear();
    }

    protected virtual void AddModules()
    {
        mBattleUI = new BattleUIManager();
        RegisterModule(mBattleUI);

        mClock = new BattleClock();
        RegisterModule(mClock);

        mStage = new BattleStage();
        RegisterModule(mStage);

        mGameState = new BattleGameState();
        RegisterModule(mGameState);

        mObjectManager = new BattleObjectManager();
        RegisterModule(mObjectManager);

        mObjectFactory = new BattleObjectFactory();
        RegisterModule(mObjectFactory);

        mCampManager = new BattleCampManager();
        RegisterModule(mCampManager);

        mCardDeckManager = new CardDeckManager();
        RegisterModule(mCardDeckManager);

        mBattleScene = new BattleScene();
        RegisterModule(mBattleScene);

        mPinBallLauncher = new PinBallLauncher();
        RegisterModule(mPinBallLauncher);

        mBattlePhase = new BattlePhase();
        RegisterModule(mBattlePhase);

        mUnitDeck = new UnitDeckManager();
        RegisterModule(mUnitDeck);

    }

    protected void RegisterModule(BattleModule module)
    {
        module.Battle = this;
        mModules.Add(module);
    }

    private void InitModules()
    {
        foreach (var module in mModules)
        {
            module.Init();
        }
    }

    public void OnEnterCombatPhase()
    {
        foreach (var module in mModules)
        {
            module.OnEnterCombatPhase();
        }
    }

    public void OnExitCombatPhase()
    {
        foreach (var module in mModules)
        {
            module.OnExitCombatPhase();
        }
    }

    public void OnEnterResetPhase()
    {
        foreach (var module in mModules)
        {
            module.OnEnterResetPhase();
        }
    }

    public void OnExitRestPhase()
    {
        foreach (var module in mModules)
        {
            module.OnExitRestPhase();
        }
    }

    public void EndBattle(BattleCamp winner)
    {
        mState = BattleState.End;
        
        EventManager.Invoke(EventEnum.BattleEnd, winner);
    }
}