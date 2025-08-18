using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 形容词
/// </summary>
abstract public class AbstractAdjectives : AbstractWord0
{
    /// <summary>技能序号</summary>
    public int adjID;
    /// <summary>目标特效</summary>
    public Animation anim;
    /// <summary>技能类型 </summary>
    public AbstractSkillMode skillMode;
    /// <summary>射程(弃用）</summary>
    public int attackDistance=100;
    /// <summary>技能效果(特殊后续效果）持续时长 </summary>
    public float skillEffectsTime;
    /// <summary>建议只在End()使用，否则别的地方需要判断是否为空 </summary>
    public AbstractCharacter aim;
    /// <summary>特殊效果存储引用</summary>
    protected List<AbstractBuff> buffs=new List<AbstractBuff>();

    public virtual void Awake()
    {
        wordKind = WordKindEnum.adj;

        aim=GetComponent<AbstractCharacter>();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots.Add(gameObject.AddComponent<Common>());

        nowTime = skillEffectsTime;

        if (aim == null) return;
        aim.AddAdj(this);
    }
    /// <summary>
    /// 技能效果(特殊效果）
    /// </summary>
    abstract public void BasicAbility(AbstractCharacter aimCharacter);

    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="aimCharacter">目标</param>
    virtual public void UseAdj(AbstractCharacter aimCharacter)
    {
        AbstractBook.afterFightText += UseText();
    }
    /// <summary>倒计时</summary>
    protected float nowTime;
    virtual protected void Update()
    {
      
        if (this.GetComponent<AbstractCharacter>() == null) return;
        if (CharacterManager.instance.pause == true) return;
        nowTime -= Time.deltaTime;
        if (nowTime < 0)
            Destroy(this);
    }

    virtual public void AddTime(float _time)
    {
        nowTime += _time;
    }

    /// <summary>
    /// 相当于OnDestroy()
    /// </summary>
    virtual public void End()
    {
       
    }

    private void OnDestroy()
    {
        
        foreach (AbstractBuff buff in buffs)
        {
            Destroy(buff);
        }
        if (aim!=null)
        {
            aim.CreateFloatWord("<s>" + this.wordName + "</s>", FloatWordColor.removeWord, false);
            End();
        }
    }

}
