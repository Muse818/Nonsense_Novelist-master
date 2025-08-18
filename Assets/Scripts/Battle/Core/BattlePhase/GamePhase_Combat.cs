using System.Collections.Generic;
using System.Linq;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.UI.CanvasScaler;

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

    public override bool DetectPvbIsEnd()
    {
        return CheckPvbIsEnd();
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
                unit.Remove();
            }
        }

        //复活所有我方角色

        //掉血
        LossCampHp();

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
    private CombatReport GetCombatReport()
    {
        int alivenum = 0;
        BattleCamp winner=BattleCamp.None;
        bool mIsHasWinner=false;
        CombatReport report = new CombatReport();
        List<BattleCamp> battleCamps = new List<BattleCamp>();
        if (Battle.BattlePhase.CurrentPhaseType == BattlePhaseType.Pvp)
        {
            mIsHasWinner = true;
            foreach (var camps in mFightCamp)
            {
                foreach (var camp in camps)
                {
                    foreach (var  unit in Battle.CampManager.GetCampMember(camp))
                    {
                        if (unit.IsAlive)
                        {
                            winner=camp;
                            alivenum ++;
                        }
                    }
                }
            }
            battleCamps.Add(winner);
        }
        else
        {
            if (CheckBossCampDead())
            {
                if (mFightCamp.Count == 2)
                {
                    mIsHasWinner = true;
                    winner=mFightCamp[0].First();
                    battleCamps. Add(winner);
                }
                else
                {
                    mIsHasWinner = true;
                    foreach (var camp in mFightCamp[0])
                    {
                        foreach (var unit in Battle.CampManager.GetCampMember(camp))
                        {
                            if (unit.IsAlive)
                            {
                                winner = camp;
                                alivenum++;
                            }
                        }
                        if(alivenum > 0)
                        {
                            battleCamps.Add(winner);
                        }
                    }
                }
            }
            else
            {
                mIsHasWinner = true;
                winner = BattleCamp.Boss;
                battleCamps.Add(winner);
                foreach (var unit in Battle.CampManager.GetCampMember(BattleCamp.Boss))
                {
                    if (unit.IsAlive)
                    {
                        alivenum++;
                    }
                }
            }
        }
        report.mChapter = 1; //先默认关卡为1
        report.mPhaseType=Battle.BattlePhase.CurrentPhaseType;
        report.mIsHasWinner= mIsHasWinner;
        report.mWinCamp=battleCamps;
        report.mActiveNum = alivenum;
        return report;
    }
    private bool CheckBossCampDead()
    {
        int alivenum = 0;
        foreach (var unit in Battle.CampManager.GetCampMember(BattleCamp.Boss))
        {
            if (unit.IsAlive)
            {
                alivenum++;
            }
        }
        if(alivenum > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
       
    }
    private void LossCampHp()
    {
        float con = 0;
        float weight = 0;
        foreach (var data in PhaseTable.DataList)
        {
            if (data.Value.mChapter == 1) //读取和当前章节一致的回合。还没写章节相关的东西，这里默认1
            {
                con = data.Value.mConstant;
                weight = data.Value.mWeight;
                break;
            }
        }
        CombatReport report = GetCombatReport();
        float lossHpNum =con+report.mActiveNum*weight;
        List<BattleCamp> loss = new List<BattleCamp>();
        if (Battle.BattlePhase.CurrentPhaseType == BattlePhaseType.Pvp)
        {
            foreach (var camps in mFightCamp)
            {
                foreach (var camp in camps.Where(camp => camp != report.mWinCamp.First()))
                {
                    loss.Add(camp);
                }
            }
        }
        else
        {
            if (CheckBossCampDead())
            {
                if (report.mIsHasWinner && report.mWinCamp.Count == 1)
                {
                    foreach (var camps in mFightCamp)
                    {
                        foreach (var camp in camps.Where(camp => camp != BattleCamp.Boss && camp != report.mWinCamp.First()))
                        {
                            loss.Add(camp);
                        }
                    }
                }
            }
            else
            {
                foreach (var camps in mFightCamp)
                {
                    foreach (var camp in camps.Where(camp => camp != BattleCamp.Boss))
                    {
                        loss.Add(camp);
                    }
                }
            }
        }
        if(loss.Count > 0)
        {
            foreach (var camp in loss)
            {
                Battle.CampManager.UpdateCampHp(camp, lossHpNum);
            }
        }

    }
    private void LossCampHp(BattleCamp deadCamp, int activeNum)
    {
        float con = 0;
        float weight = 0;
        foreach (var data in PhaseTable.DataList)
        {
            if (data.Value.mChapter == 1) //读取和当前章节一致的回合。还没写章节相关的东西，这里默认1
            {
                con = data.Value.mConstant;
                weight = data.Value.mWeight;
                break;
            }
        }
        float lossHpNum = con + activeNum * weight;
        Battle.CampManager.UpdateCampHp(deadCamp, lossHpNum);
    }
    private BattleCamp GetDeadCamp()
    {
        BattleCamp deadCamp=BattleCamp.None;
        int aliveNum = 0;
        //Dictionary<BattleCamp,int> campdata =new Dictionary<BattleCamp, int>();
        foreach (var camps in mFightCamp)
        {
            foreach (var camp in camps)
            {
                foreach (var unit in Battle.CampManager.GetCampMember(camp))
                {
                    if (unit.IsAlive)
                    {
                        aliveNum++;
                    }
                }
                //campdata.Add(camp, aliveNum);
                if (aliveNum ==0)
                {
                    deadCamp = camp;
                    break;
                }
            }
            break;
        }
        return deadCamp; 
    }
    private bool CheckPvbIsEnd()
    {
        int aliveNum = 0;
        BattleCamp deadCamp = GetDeadCamp();
        if (deadCamp == BattleCamp.Boss)
        {
            return true;
        }
        else
        {
            foreach (var camps in mFightCamp)
            {
                foreach (var camp in camps.Where(camp => camp != BattleCamp.Boss && camp != deadCamp))
                {
                    foreach (var unit in Battle.CampManager.GetCampMember(camp))
                    {
                        if (unit.IsAlive)
                        {
                            aliveNum++;
                        }
                    }
                }
            }
            if(aliveNum == 0)
            {
                return true;
            }
            else
            {
                LossCampHp(deadCamp, aliveNum);
                return false;
            }
        }
    }
    #endregion

}
