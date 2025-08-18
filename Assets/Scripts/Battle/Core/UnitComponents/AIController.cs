

using System.Collections.Generic;
using UnityEngine;

public enum EUnitState
{
    None = 0,
    Idle,
    Attack,
    Ultra,
}

public class AIController : UnitComponent
{
    protected EUnitState mState = EUnitState.None;
    public EUnitState State => mState;

    protected AbilityBase mAttackAbility = null; // 攻击技能
    protected AbilityBase mUltraAbility = null; // 特殊技能
    
    protected BattleUnit mTarget = null;
    public BattleUnit Target => mTarget;

    protected bool hasBindAnimFunc = false;

    public void RegisterAttackAbility(AbilityBase abi)
    {
        if (mAttackAbility.IsValid())
        {
            Debug.LogError("repeat register attack ability");
            return;
        }

        mAttackAbility = abi;
    }

    public override void Start()
    {
        mState = EUnitState.Idle;
    }

    public override void Update(float deltaTime)
    {
        switch (State)
        {
            case EUnitState.Idle:
                TickIdle(deltaTime);
                break;

            case EUnitState.Attack:
                TickAttack(deltaTime);
                break;

            case EUnitState.Ultra:
                TickUltra(deltaTime);
                break;

            default:
                break;
        }
    }

    protected void TickIdle(float deltaTime)
    {
        if (!mOwner.Battle.BattlePhase.IsCombat)
            return;
        
        foreach (var abi in mOwner.AbilityAgent.Abilities)
        {
            if (!abi.IsUltra)
                continue;

            if (!abi.CanActivate())
                continue;

            var target = abi.PickTarget();
            if (target == null)
                continue;

            abi.SetTarget(target);
            if (abi.TryActivate(true))
            {
                mUltraAbility = abi;
                mTarget = target;
                EnterState(EUnitState.Ultra);
                return;
            }
        }

        if (mAttackAbility.IsValid() && mAttackAbility.CanActivate())
        {
            var target = mAttackAbility.PickTarget();

            if (target != null)
            {
                mTarget = target;
                mAttackAbility.SetTarget(mTarget);
                if (mAttackAbility.TryActivate(true))
                {
                    EnterState(EUnitState.Attack);
                }
            }
        }
    }

    protected void TickAttack(float deltaTime)
    {
        bool needStop = !CanKeepAttack();

        if (!needStop)
        {
            if (!IsValidTarget(mTarget))
            {
                needStop = true;
            }
            else if (!mAttackAbility.IsValid() || !mAttackAbility.IsActivated)
            {
                needStop = true;
            }
        }

        if (needStop)
        {
            EnterState(EUnitState.Idle);
        }
    }
    
    protected void TickUltra(float deltaTime)
    {
        bool needStop = !CanKeepAttack();

        if (!needStop)
        {
            if (!IsValidTarget(mTarget))
            {
                needStop = true;
            }
            else if (!mUltraAbility.IsValid() || !mUltraAbility.IsActivated)
            {
                needStop = true;
            }
        }

        if (needStop)
        {
            EnterState(EUnitState.Idle);
        }
    }

    protected bool CanKeepAttack()
    {
        return !mOwner.Status.InStatus(Status.Stun);
    }

    protected bool IsValidTarget(BattleUnit target)
    {
        if (!mTarget.IsValid() || !mTarget.IsAlive)
            return false;

        return true;
    }

    protected void BindAnimFunc()
    {
        if (hasBindAnimFunc)
            return;
        
        if (mOwner.UnitView)
        {
            mOwner.UnitView.ModelLayout.AnimEvents.OnActBegin += OnActBegin;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActTrigger += OnActTrigger;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActEnd += OnActEnd;
        }
        
        hasBindAnimFunc = true;
    }

    protected void UnbindAnimFunc()
    {
        if (!hasBindAnimFunc)
            return;
        
        if (mOwner.UnitView)
        {
            mOwner.UnitView.ModelLayout.AnimEvents.OnActBegin -= OnActBegin;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActTrigger -= OnActTrigger;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActEnd -= OnActEnd;
        }

        hasBindAnimFunc = false;
    }
    
    protected void OnActBegin()
    {
        
    }
    
    protected void OnActTrigger()
    {
        switch (State)
        {
            case EUnitState.Attack:
                if (mAttackAbility.IsValid())
                {
                    mAttackAbility.OnAnimTrigger();
                }
                break;

            case EUnitState.Ultra:
                if (mUltraAbility.IsValid())
                {
                    mUltraAbility.OnAnimTrigger();
                }
                break;
        }
    }
    
    protected void OnActEnd()
    {
        EnterState(EUnitState.Idle);
    }
    
    public void EnterState(EUnitState state)
    {
        if (mState == state)
            return;

        switch (mState)
        {
            case EUnitState.Attack:
            {
                if (mAttackAbility.IsValid())
                {
                    mAttackAbility.SetTarget(null);
                    mAttackAbility.TryDeactivate();
                }
                
                mTarget = null;
                
                UnbindAnimFunc();
                
                break;
            }

            case EUnitState.Ultra:
            {
                if (mUltraAbility.IsValid())
                {
                    mUltraAbility.SetTarget(null);
                    mUltraAbility.TryDeactivate();
                }
                
                mTarget = null;
                
                UnbindAnimFunc();
                
                break;
            }
        }
        
        mState = state;

        switch (mState)
        {
            case EUnitState.Idle:
            {
                if (mOwner.UnitView)
                    mOwner.UnitView.ModelLayout.PlayAnimation("idle", true);

                break;
            }

            case EUnitState.Attack:
            {
                BindAnimFunc();
                
                if (mOwner.UnitView)
                    mOwner.UnitView.ModelLayout.PlayAnimation(mAttackAbility.AnimName);
                
                break;
            }

            case EUnitState.Ultra:
            {
                BindAnimFunc();
                
                if (mUltraAbility.IsValid() && !mUltraAbility.AnimName.Equals("None"))
                    mOwner.UnitView.ModelLayout.PlayAnimation(mUltraAbility.AnimName);
                
                break;
            }
        }
    }

    public override void OnEnterRestPhase()
    {
        EnterState(EUnitState.Idle);
    }
}