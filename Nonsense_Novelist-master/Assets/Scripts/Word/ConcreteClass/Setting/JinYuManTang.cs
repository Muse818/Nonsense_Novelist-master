using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：金玉满堂
/// </summary>
public class JinYuManTang : AbstractSetting
{
    AbstractCharacter chara;
    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.QiaoSi;
        settingName = "金玉满堂";
        res_name = "jinyumantang";
        info = "王熙凤获得的名词和形容词，有25%概率视作获得两次";
        lables = new List<string> { "角色"};

        hasAdd = false;

 


    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<WangXiFeng>();
        if (chara != null)
        {
            chara.event_AddAdj += EffectA;
            chara.event_AddNoun += EffectB;
        }
        hasAdd = true;
    }
    void EffectA(AbstractAdjectives _av)
    {
        //她拥有形容词中的一个，减少1能量上限，最少为1
        int number = Random.Range(1, 5);
        if (number == 1) { chara.gameObject.AddComponent(_av.GetType()); }
        else return;

    }
    void EffectB(AbstractItems _av)
    {
        //她拥有名词中的一个，减少1能量上限，最少为1
        int number = Random.Range(1, 5);
        if (number == 1) { chara.gameObject.AddComponent(_av.GetType()); }
        else return;

    }
    private void OnDestroy()
    {
        if (!hasAdd) return;
        chara.event_AddAdj -= EffectA;
        chara.event_AddNoun -= EffectB;
    }
}
