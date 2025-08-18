using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff：疯狂
/// </summary>
public class FengKuang : AbstractBuff
{
    static public string s_description = "攻击时不分敌我，无法攻击自己；\n包括奶妈也会随机进行治疗";
    static public string s_wordName = "疯狂";

    List<AbstractVerbs> skills;
    override protected void Awake()
    {

        buffName = "疯狂";
        description = "攻击时不分敌我，无法攻击自己；\n包括奶妈也会随机进行治疗";
        book = BookNameEnum.ZooManual;
        maxTime = 2;
        upup = 1;
        isBad = true;
        isAll = false;
        base.Awake();

        //将此角色的目标设置为随机
        chara.SetAimRandom(true);
    }
    public override void Update()
    {
        base.Update();
        
    }
    private void OnDestroy()
    {
      
        chara.SetAimRandom(true);
        base.OnDestroy();

    }
}
