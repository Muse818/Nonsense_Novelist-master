using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象动词类（技能）
/// </summary>
abstract public class AbstractVerbs : AbstractWord0 ,ICD
{
    /// <summary>技能序号</summary>
    public int skillID;

    public AbstractCharacter character;


    /// <summary>施法者特效</summary>
    public Animation userAnim;
    /// <summary>作用者特效</summary>
    public Animation aimAnim;
    /// <summary>弹道特效</summary>
    public Animation bulletAnim;

    /// <summary>技能类型 </summary>
    public AbstractSkillMode skillMode;

    /// <summary>射程（已弃用） </summary>
    public int attackDistance=100;
    /// <summary>技能效果持续时长 </summary>
    public float skillEffectsTime;
    /// <summary>是否正在使用该技能 </summary>
    public bool isUsing;

    public bool onChara = false;
    /// <summary>当前能量(每个技能有自己的能量值)</summary>
    private int cd;
    public int CD
    { 
        get
        {
            return cd;
        }
        set
        {
            cd = value;
        }
    }
    /// <summary>释放一次所需能量</summary>
    public int needCD=3;
    /// <summary>施法时长：前摇，后摇（已施法时间变量现场声明）（已弃用）</summary>
    public float prepareTime,afterTime;
    /// <summary>特殊效果存储引用</summary>
    protected List<AbstractBuff> buffs=new List<AbstractBuff>();

    public virtual void Awake()
    {
        if(OnAwake!=null) 
        OnAwake();

        character=this.GetComponent<AbstractCharacter>();
        if (character != null)
        {
            onChara = true;
            character.OnEnergyFull += CdAdd;
        }
        if (this.gameObject.name=="word")
        {
            onChara = true;
        }
        wordKind = WordKindEnum.verb;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots.Add(gameObject.AddComponent<Common>());

    }
    public delegate void AwakeHandler();
    static public event AwakeHandler OnAwake;

    public virtual void CdAdd(AbstractCharacter ac)
    {
        CD++;


        if (CD+1>=needCD) 
            character.canUseSkills++;
    }

    /// <summary>
    /// 技能效果(特殊效果）
    /// </summary>
    virtual public void BasicAbility(AbstractCharacter useCharacter)
    {

    }
  
    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="camp">使用者阵营</param>
    virtual public void UseVerb(AbstractCharacter useCharacter)
    {
        isUsing = true;
        stateInfo=useCharacter.charaAnim.anim.GetCurrentAnimatorStateInfo(0);
        character.charaAnim.Play(AnimEnum.attack);
        character.CreateFloatWord(this.wordName, FloatWordColor.physics, false);
        useCharacter.UseVeb_Chara(this);
    }
    virtual public void CDZero()
    {
        CD = 0;
    }
    private AnimatorStateInfo stateInfo;

    virtual public void FixedUpdate()
    {
        if(isUsing /*&& stateInfo.normalizedTime >= 0.9f*/)//播放完特效即为使用完毕
        {
            isUsing=false;
        }
    }

    /// <summary>
    /// 冷却（UseVerbs将cd重置为0）
    /// </summary>
    /// <returns>是否冷却完毕</returns>
    virtual public bool CalculateCD()
    {
        if (needCD == 0) return false;

        if (CD >= needCD)
        {
            return true;
        }
        else
        {
            //print(wordName+"--CalculateCD false!CD:" + CD + "needcd:" + needCD);
             return false;
        }
           
    }

    virtual public void OnDestroy()
    {
        if (onChara)
        {
            GameMgr.instance.DetectVerb(this.GetType());
        }
        foreach (AbstractBuff buff in buffs)
        {
            Destroy(buff);
        }
    }


}
