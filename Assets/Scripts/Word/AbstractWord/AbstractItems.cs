using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品（自己处理每秒做的事）
/// </summary>
abstract public class AbstractItems : AbstractWord0
{
    /// <summary>物品序号</summary>
    public int itemID;
    /// <summary>物品对应战场形象</summary>
    public GameObject obj;
    /// <summary>持有方式</summary>
    public HoldEnum holdEnum;
    /// <summary>物品材质，对应音效种类 </summary>
    public MaterialVoiceEnum VoiceEnum;
    /// <summary>建议只在End(),UseVerb()使用，否则别的地方需要判断是否为空 </summary>
    protected AbstractCharacter aim;
    /// <summary>特殊效果存储引用</summary>
    protected List<AbstractBuff> buffs = new List<AbstractBuff>();
    /// <summary>名词</summary>
    public int useTimes = 5;
    virtual public void Awake()
    {
        aim = GetComponent<AbstractCharacter>();
        wordKind = WordKindEnum.noun;
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots.Add(gameObject.AddComponent<Common>());

        if (aim == null) return;
        aim.AddNoun(this);
    }

    /// <summary>
    /// 初始释放
    /// </summary>
    /// <param name="chara">持有者</param>
    virtual public void UseItem(AbstractCharacter chara)
    {
       
    }

    /// <summary>
    /// 相当于Update
    /// </summary>
    /// <param name="chara"></param>
    virtual public void UseVerb()
    {

    }

    private void Update()
    {
        if(aim!=null)
        {
            UseVerb();
        }
    }

    /// <summary>
    /// 相当于OnDestroy()
    /// </summary>
    virtual public void End()
    {
        aim.CreateFloatWord("<s>" + wordName + "</s>", FloatWordColor.removeWord, false);
    }

    private void OnDestroy()
    {
        foreach (AbstractBuff buff in buffs)
        {
            Destroy(buff);
        }
        if (aim != null)
        {
            End();
        }
    }

}
