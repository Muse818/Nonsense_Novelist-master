//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：姬妮肽霉
/// </summary>
public class JiNiTaiMei : AbstractSetting
{

    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "姬妮肽霉";
        res_name = "jinitaimei";
        info = "贝洛姬·姬妮每次释放动词，治疗一名友方0.2*意志";
        lables = new List<string> { "角色", "高频" };
        hasAdd = false;

    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<BeiLuoJi>();
        if (chara != null)
        {
            chara.event_UseVerb += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractVerbs verb)
    {
        //获取友方所有角色（包含自己）
        List<AbstractCharacter> a = CharacterManager.instance.GetFriend(chara.Camp);
        int i = Random.Range(0, a.Count);
        int loopCount = 0;
        while (a[i].wordName == "贝洛姬·姬妮"&&loopCount<50)
        {
            int j = Random.Range(0, a.Count);
            i = j;
            loopCount++;
        }
        //若只有贝洛姬自己，则跳出循环默认回复自己血量
        a[i].BeCure(02 * a[i].san * a[i].sanMul, true, 0, chara);
    }
    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_UseVerb -= Effect;
    }
}
