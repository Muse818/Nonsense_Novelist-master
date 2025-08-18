using System.Collections.Generic;
using System.Linq;

public class GamePhase_Combat : GamePhase
{
    protected float mMonsterDelayTime = 0;
    protected List<List<BattleCamp>> mFightCamp;

    public GamePhase_Combat(List<List<BattleCamp>> fightCamp, float monsterDelayTime)
    {
        mFightCamp = fightCamp;
        mMonsterDelayTime = monsterDelayTime;
    }

    public override void Start()
    {
        Enter();
    }

    public override void Enter()
    {
        //生成危机
        
        if (Battle.BattlePhase.CurrentPhaseType != BattlePhaseType.Pvp)
        {
            Battle.Stage.SpawnUnit(Battle.UnitDeck.RandomUnit(BattleUnitType.Monster, Battle.BattlePhase.CurrentPhase.mLevel), UnitSlotType.Boss);
        }

        // 切换战斗UI
        Battle.PinBallLauncher.CanShootSwitch(true);
        Battle.BattlePhase.ActivePhaseUI(BattlePhaseType.Pve);
        
        Battle.OnEnterCombatPhase();
    }



    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        //按照时间生成事件
    }

    public override bool DetectPhaseEnd()
    {
        //检测是否有哪个阵营全部死亡
        return CheckCampDead();
    }

    public override void Exit()
    {
        // 关闭角色战斗状态

        //清理场上的怪物
        {
            List<BattleUnit> monsters = new List<BattleUnit>();
            monsters.AddRange(Battle.CampManager.GetCampMember(BattleCamp.Boss));
            foreach (var unit in monsters)
            {
                unit.Die(null);
            }
        }

        //复活所有我方角色

        //掉血

        // 切换战斗UI
        Battle.OnExitCombatPhase();
    }

    #region Helper Methods

    private bool CheckCampDead()
    {
        foreach (var camps in mFightCamp)
        {
            bool bHasAlive = false;
            foreach (var unitList in camps.Select(camp => Battle.CampManager.GetCampMember(camp)))
            {
                foreach (var unit in unitList)
                {
                    if (unit.IsAlive)
                    {
                        bHasAlive = true;
                        break;
                    }
                }
            }

            if (!bHasAlive)
                return true;
        }

        return false;
    }

    #endregion

}
