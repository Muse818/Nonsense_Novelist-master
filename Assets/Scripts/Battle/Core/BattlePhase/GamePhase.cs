
public abstract class GamePhase
{
    protected int mID = 0;
    public int ID
    {
        set => mID = value;
        get => mID;
    }

    protected bool mRegistered = false;
    public bool IsRegistered
    {
        set => mRegistered = value;
        get => mRegistered;
    }

    protected bool mTickEnable = true;

    public BattleBase Battle { get; set; }

    public bool IsTickEnable
    {
        set => mTickEnable = value;
        get => mTickEnable;
    }
    

    // 阶段初始化
    public abstract void Start();
    public abstract void Enter();

    // 每帧更新
    public virtual void Update(float deltaTime) { }
    
    public abstract void Exit();

    // 检测阶段是否结束
    public abstract bool DetectPhaseEnd();
    public abstract bool DetectPvbIsEnd();

}
