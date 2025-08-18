using System.Diagnostics;
using JetBrains.Annotations;

public class GamePhase_Rest : GamePhase
{
    public class RestData
    {
        public int mEventCount;
        public int mEventShowTime;
    }

    public RestData mRestData;

    public GamePhase_Rest(RestData restData)
    {
        mRestData = restData;
    }

    public override void Start()
    {
        Enter();
    }

    public override void Enter()
    {
        // 关闭角色战斗状态

        //清理场上的怪物

        //复活所有我方角色

        //掉血

        // 切换战斗UI
        Battle.PinBallLauncher.CanShootSwitch(false);
        Battle.BattlePhase.ActivePhaseUI(BattlePhaseType.Rest);

        Battle.OnEnterResetPhase();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        //按照时间生成事件
    }

    public override bool DetectPhaseEnd()
    {
        return false;

    }

    public override void Exit()
    {
        Battle.OnExitRestPhase();

        // 关闭角色战斗状态
        foreach (var chara in Battle.CampManager.GetCampMember(BattleCamp.Camp1))
        {

        }

        foreach (var chara in Battle.CampManager.GetCampMember(BattleCamp.Camp2))
        {

        }

        //清理场上的怪物
        if (Battle.CampManager.GetCampMember(BattleCamp.Boss).Count > 0)
        {
            foreach (var chara in Battle.CampManager.GetCampMember(BattleCamp.Boss))
            {

            }
        }
        //复活所有我方角色

        //掉血

        // 切换战斗UI


    }

}
