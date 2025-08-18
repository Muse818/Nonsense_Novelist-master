using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【大技能】：谋定后动
/// </summary>
public class MouDingHouDong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "谋定后动";
        res_name = "moudinghoudong";
        info = "角色持有的动词中，能量最高的动词，能量+4，释放后追击一次相同技能";
        lables = new List<string> { "蓄能" };
        hasAdd = false;
      
    }
    public override void Init()
    {
        if (hasAdd) return;

        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_AddVerb += Effect;
        }
        hasAdd = true;
    }    
    void Effect(AbstractVerbs _av)
    {
        AbstractVerbs absv=null;
        foreach (var it in _av.GetComponent<AbstractCharacter>().GetComponents<AbstractVerbs>())//角色每个动词
        {
            int cd = 0;
            if (it.needCD > cd)
            {
                cd = it.needCD;
                absv = it;
            }
        }
        absv.CD += 4;
        absv.UseVerb(_av.GetComponent<AbstractCharacter>());//动词释放（在动词释放的时候再追击）
        
    }

    private void OnDestroy()
    {
        if (!hasAdd) return; 
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_AddVerb += Effect;
        }
    }
}
