using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;

public class BattleUnit : BattleObject
{
    protected bool mAlive = true;
    public bool IsAlive => mAlive;

    protected bool mStart = false;
    public bool IsStart => mStart;
    
    protected bool mRemoved = false;
    private float mTime = 0;
    public BattleUnit ServantOwner { get; set; }

    protected float mHp = 0;
    public float Hp => mHp;
    public float MaxHp => GetAttributeValue(AttributeType.MaxHp);

    
    public float HpPercent
    {
        get
        {
            var maxHp = MaxHp;
            return maxHp > 0 ? mHp / maxHp : 0;
        }
    }

    // camp would be enum
    public BattleCamp Camp
    {
        get => mUnitInstance.mCamp;
        set
        {
            var oldCamp = mUnitInstance.mCamp;
            mUnitInstance.mCamp = value;
            
            if (oldCamp != mUnitInstance.mCamp)
            {
                EventManager.Invoke(EventEnum.UnitChangeCamp, this, oldCamp);
            }
        }
    }

    protected BattleUnitPos mPos = 0;
    public BattleUnitPos Pos
    {
        get => mPos;
        set => mPos = value;
    }

    public Vector3 ViewPos => UnitView != null ? UnitView.Pos : Vector3.zero;

    protected UnitSlot mSlot = null;
    public UnitSlot Slot
    {
        get => mSlot;
        set
        {
            if (mSlot != null && value != null)
                return;
            
            mSlot = value;

            if (mSlot != null)
            {
                if (Data.mInitType == BattleUnitType.Character)
                {
                    BattleCamp oldCamp = Camp;
                    BattleCamp newCamp = mSlot.SpawnCamp;
                    if (oldCamp != newCamp)
                    {
                        Camp = newCamp;
                        if (ServantsAgent.IsValid())
                        {
                            foreach (var it in ServantsAgent.Servants)
                            {
                                it.Camp = newCamp;
                            }
                        }
                    }
                }
            }
            
            if (UnitView != null)
            {
                UnitView.UpdateSlot();
            }
        }
    }

    protected UnitInstance mUnitInstance;
    public UnitInstance UnitInstance => mUnitInstance;

    protected BattleUnitTable.Data mData = null;
    public BattleUnitTable.Data Data => mData;

    #region UnitComponent
    protected List<UnitComponent> mComponents = new List<UnitComponent>();
    public List<UnitComponent> Components => mComponents;

    protected AIController mAIAgent = null;
    public AIController AIAgent => mAIAgent;

    protected AbilityAgent mAbilityAgent = null;
    public AbilityAgent AbilityAgent => mAbilityAgent;

    protected EffectAgent mEffectAgent = null;
    public EffectAgent EffectAgent => mEffectAgent;
    
    protected WordComponent mWordComponent = null;
    public WordComponent WordComponent => mWordComponent;
    
    protected ServantsAgent mServantsAgent = null;
    public ServantsAgent ServantsAgent => mServantsAgent;
    #endregion

    protected StatusManager mStatus = new StatusManager();
    public StatusManager Status => mStatus;

    protected AttributeSet mAttributeSet = new AttributeSet();
    public AttributeSet AttributeSet => mAttributeSet;
    
    public List<BattleUnit> Allies => Battle.CampManager.GetAllies(this);
    public List<BattleUnit> Enemies => Battle.CampManager.GetEnemies(this);

    protected UnitViewBase mView = null;

    public UnitViewBase UnitView
    {
        set => mView = value;
        get => mView;
    }
    protected BattleUnitSelfUI mInfoUI= null;

    public BattleUnitSelfUI infoUI
    {
        set => mInfoUI = value;
        get => mInfoUI;
    }


    public BattleUnit(BattleUnitTable.Data data, UnitInstance unitInstance)
    {
        mData = data;
        mUnitInstance = unitInstance;
    }

    // init with no battle
    public virtual void Init()
    {
        InitAttributes();
        AddComponents();
    }

    public void InitAttributes()
    {
        AttributeSet.Define(AttributeType.MaxHp, mData.mMaxHp);
        AttributeSet.Define(AttributeType.Attack, mData.mAttack);
        AttributeSet.Define(AttributeType.Def, mData.mDefense);
        AttributeSet.Define(AttributeType.Psy, mData.mPsy);
        AttributeSet.Define(AttributeType.San, mData.mSan);
        AttributeSet.Define(AttributeType.Mag, mData.mMag);
        AttributeSet.Define(AttributeType.AttackSpeed, 1.0f);
        AttributeSet.Define(AttributeType.RecoverHp, mData.mRecoverHp);
        AttributeSet.Define(AttributeType.VerbDamageCoefficient, mData.mVerbDamageCoefficient);
        AttributeSet.Define(AttributeType.VerbDamageMod, mData.mVerbDamageMod);
        AttributeSet.Define(AttributeType.EffectDamageCoefficient,mData.mEffectDamageCoefficient);
        AttributeSet.Define(AttributeType.NormalAttackDamage,mData.mNormalAttackDamageCoefficient);
        AttributeSet.Define(AttributeType.DebuffUp, mData.mDebuffDurationUp);
        AttributeSet.Define(AttributeType.HealUp, mData.mHealUp);
        AttributeSet.Define(AttributeType.TakeHealUp, mData.mTakeHealUp);
        AttributeSet.Define(AttributeType.SuckBlood, mData.mSuckBloodCoefficient);
        AttributeSet.Define(AttributeType.Sdu, mData.mSdu);
        AttributeSet.Define(AttributeType.Luc, mData.mLuc);
        AttributeSet.Define(AttributeType.TauntLevel, mData.mTauntLevel);
        AttributeSet.Define(AttributeType.NounSlotNum,BattleConfig.mData.unit.nounInitSlotNum);
        AttributeSet.Define(AttributeType.VerbSlotNum, BattleConfig.mData.unit.verbDefaultSlotNum);
        AttributeSet.Define(AttributeType.ServantSlotNum, BattleConfig.mData.unit.servantDefaultSlotNum);
        AttributeSet.Define(AttributeType.ServantAttrBouns, mData.mServantAttrBouns);
        AttributeSet.Define(AttributeType.ServantAttackSpeed, mData.mServantAttackSpeed);
        AttributeSet.Define(AttributeType.PowerRecoverSpeed, mData.mPowerRecoverSpeed);
        AttributeSet.Define(AttributeType.SingleMaxPowerDown, mData.mSingleMaxPowerDown);
        AttributeSet.Define(AttributeType.AllMaxPowerDown, mData.mAllMaxPowerDown);
        if (Data.mInitType != BattleUnitType.Servant)
        {
            AttributeSet.Define(AttributeType.Soc, mData.mSoc);
            AttributeSet.Define(AttributeType.Pet, mData.mPet);
        }
    }

    protected void AddComponents()
    {
        mWordComponent = new WordComponent();
        RegisterComponent(mWordComponent);
        
        mAbilityAgent = new AbilityAgent();
        RegisterComponent(mAbilityAgent);

        mEffectAgent = new EffectAgent();
        RegisterComponent(mEffectAgent);
        
        mAIAgent = new AIController();
        RegisterComponent(mAIAgent);

		if (Data.mInitType != BattleUnitType.Servant)
        {
            mServantsAgent = new ServantsAgent();
            RegisterComponent(mServantsAgent);
        }
    }

    protected void RegisterComponent(UnitComponent component)
    {
        if (component.IsRegistered)
            return;

        component.Owner = this;
        mComponents.Add(component);

        if (IsStart)
            component.Start();
    }

    public float GetAttributeValue(AttributeType type)
    {
        return AttributeSet.Get(type);
    }
    public override void Start()
    {
        mHp = GetAttributeValue(AttributeType.MaxHp);

        foreach (var comp in Components)
        {
            comp.Start();
        }
        mStart = true;
    }

    public override void Update(float deltaSec)
    {
        if (!Battle.BattlePhase.IsCampFight(Camp))
            return;
        
        foreach (var comp in Components.Where(comp => comp.Enabled))
        {
            comp.Update(deltaSec);
        }
        if (IsAlive)
        {
            mTime += deltaSec;
            if(mTime >= 2f)
            {
                RecoverHp();
                mTime = 0;
            }
        }
    }

    public override void LateUpdate(float deltaSec)
    {
        if (!Battle.BattlePhase.IsCampFight(Camp))
            return;
        
        foreach (var comp in Components.Where(comp => comp.Enabled))
        {
            comp.LateUpdate(deltaSec);
        }

        float oldHpPercent = HpPercent;

        mAttributeSet.ApplyMod();
        mStatus.ApplyMod();

        mHp = GetAttributeValue(AttributeType.MaxHp) * oldHpPercent;
    }
    
    public override void OnEnterCombatPhase()
    {
        foreach (var comp in Components)
        {
            comp.OnEnterCombatPhase();
        }

        if (UnitView)
            UnitView.OnEnterCombatPhase();
    }
    
    public override void OnExitCombatPhase()
    {
        if (IsAlive)
        {
            mHp = MaxHp;
        }
        else
        {
            Revive();
        }
        
        foreach (var comp in Components)
        {
            comp.OnExitCombatPhase();
        }
        
        if (UnitView)
            UnitView.OnExitCombatPhase();
    }

    public override void OnEnterRestPhase()
    {
        foreach (var comp in Components)
        {
            comp.OnEnterRestPhase();
        }
    }
    
    public override void OnExitRestPhase()
    {
        foreach (var comp in Components)
        {
            comp.OnExitRestPhase();
        }
    }

    public void AddMod(AttributeType type, float mod)
    {
        AttributeSet.AddMod(type, mod);
    }

    public void AddPercentMod(AttributeType type, float mod)
    {
        AttributeSet.AddPercentMod(type, mod);
    }

    public void ModifyBase(AttributeType type, float mod, bool isPercent = false)
    {
        if (type == AttributeType.MaxHp)
        {
            AttributeSet.ModifyBase(type, mod, isPercent);
        }
        else
        {
            AttributeSet.ModifyBase(type, mod, isPercent);
        }
    }
   public void RecoverHp()
    {
        if (mData.mRecoverHp == 0)
            return;
        mHp += mData.mRecoverHp;
        mHp = Mathf.Min(mHp, MaxHp);
    }
    public float GetAttackedProbability()
    {
        var sum = 0;
        foreach (var unit in Battle.CampManager.GetCampMember(this.Camp))
        {
            sum += (int)Math.Pow( 3 + unit.GetAttributeValue(AttributeType.TauntLevel),2);
        }
        return  (int)Math.Pow(3 + GetAttributeValue(AttributeType.TauntLevel), 2)/sum;
    }

    #region DamageProcess

    /// <summary>
    /// 造成伤害计算预处理
    /// </summary>
    public void PreDealDamageCalc(DealDamageCalc damageCalc)
    {
        if ((damageCalc.mFlag & DealDamageFlag.Fixed) == 0)
        {
            foreach (var abi in AbilityAgent.Abilities)
            {
                if (abi == damageCalc.mAbility)
                {
                    abi.OnPreDealDamageCalc(damageCalc);
                }
                else
                {
                    abi.OnPreDealDamageCalcOtherAbility(damageCalc);
                }
            }
        }
    }

    /// <summary>
    /// 受到伤害计算预处理
    /// </summary>
    public TakeDamageCalc PreTakingDamageCalc(BattleUnit instigator, DealDamageCalc damageCalc)
    {
        TakeDamageCalc takeDamageCalc = BattleHelper.ReusableTakeDamageCalc;

        //todo: fill take damage calc info
        takeDamageCalc.mInstigator = instigator;
        takeDamageCalc.mAbility = damageCalc.mAbility;
        takeDamageCalc.mFlag = damageCalc.mFlag;
        takeDamageCalc.mMagic = damageCalc.mMagic;
        takeDamageCalc.mDefense = GetAttributeValue(AttributeType.Def);
        takeDamageCalc.mResistance = GetAttributeValue(AttributeType.Def);


        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnPreTakeDamageCalc(takeDamageCalc);
        }

        // process ally
        var allies = Allies;
        foreach (var p in allies)
        {
            foreach (var abi in p.AbilityAgent.Abilities)
            {
                abi.OnAllyPreTakeDamageCalc(takeDamageCalc);
            }
        }

        return takeDamageCalc;
    }

    public void PreDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            if (report.mMeta.mAbility == abi)
            {
                abi.OnPreDealDamage(report);
            }
            else
            {
                abi.OnPreDealDamageOtherAbility(report);
            }
        }
    }

    public void PreTakeDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnPreTakeDamage(report);
        }
    }

    public void PostDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            if (abi == report.mMeta.mAbility)
            {
                abi.OnPostDealDamage(report);
            }
            else
            {
                abi.OnPostDealDamageOtherAbility(report);
            }
        }
    }

    public void PostTakeDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnPostTakeDamage(report);
        }
        
        EventManager.Invoke(EventEnum.UnitTakeDamage, this);
    }

    public void AllyDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnAllyDealDamage(report);
        }
    }

    public void AllyTakeDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnAllyTakeDamage(report);
        }
    }

    public void EnemyDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnEnemyDealDamage(report);
        }
    }

    public void EnemyTakeDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnEnemyTakeDamage(report);
        }
    }

    public float ApplyDamage(float damageValue, bool cannotKill, out bool kill)
    {
        float number = 0;

        kill = false;

        if (damageValue > 0)
        {
            float currentMaxHp = GetAttributeValue(AttributeType.MaxHp);
            float max = cannotKill ? currentMaxHp - 1 : currentMaxHp;
            number = Mathf.Min(damageValue, max);

            mHp -= number;
            if (mHp < 0.99f && Battle.Mode != BattleMode.TestShoot)
            {
                kill = true;
            }
        }

        return number;
    }

    public void Die(DamageReport report)
    {
        if (!mAlive)
            return;

        mHp = 0f;
        mAlive = false;
        
        foreach (var comp in Components)
        {
            comp.OnSelfDeath(report);
        }
        EventManager.Invoke(EventEnum.UnitDie, this);
        EventManager.Invoke(EventEnum.UnitDie, this,report);

        if (UnitView)
        {
            UnitView.OnUnitDie();
        }
        
        Battle.BattleUI.Hide(mInfoUI);

        if (Data.mInitType != BattleUnitType.Character)
        {
            Remove();
        }
    }

    public void Revive()
    {
        if (mAlive)
            return;
        
        mAlive = true;

        mHp = MaxHp;

        foreach (var comp in Components)
        {
            comp.OnSelfRevive();
        }
        
        if (UnitView)
        {
            UnitView.OnUnitRevive();
        }
        
        Battle.BattleUI.ShowPanel(mInfoUI);
    }

    public void Remove()
    {
        if (mRemoved)
            return;

        mRemoved = true;
        
        if (mSlot)
            mSlot.Remove();
        
        EventManager.Invoke(EventEnum.UnitRemove, this);
        
        if (UnitView)
        {
            UnitView.OnUnitRemove();
            UnitView = null;
        }
        
        Battle.ObjectManager.RemoveObject(this);
    }
    public void LossHp(DamageReport report)
    {
        if(!mAlive) return;

        EventManager.Invoke(EventEnum.UnitTakeDamage, this);
        
    }

    #endregion

    #region EffectProcess

    public void OnSelfApplyEffect(BattleEffect be)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnSelfApplyEffect(be);
        }
    }

    public void OnSelfApplyHealEffect(BattleEffect be)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnSelfApplyHealEffect(be);
        }
    }
    public float ApplyHeal(float healValue)
    {
        float number = 0;

        if (healValue > 0)
        {
            float currentMaxHp = GetAttributeValue(AttributeType.MaxHp);
            number = Mathf.Min(healValue, currentMaxHp - mHp);

            mHp += number;
        }

        return number;
    }
    public float ApplyHeal(AttributeType attributeType, float healValue)
    {
        float number = 0;
        var attr=AttributeSet.GetAttribute(attributeType);
        if (healValue > 0)
        {
            float currentvalue = GetAttributeValue(attributeType);
            if (mData.mTakeHealUp != 0)
            {
                number = Mathf.Min(healValue*(1+mData.mTakeHealUp), attr.mBaseValue - currentvalue);
            }
            else
            {
                number = Mathf.Min(healValue, attr.mBaseValue - currentvalue);
            }
            attr.mValue += number;
        }

        return number;
    }
    #endregion

}