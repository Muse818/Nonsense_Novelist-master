using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff：亢奋
/// </summary>
public class KangFen : AbstractBuff
{
    static public string s_description = "消除自身，给缺少5能量的动词充能1点";
    static public string s_wordName = "亢奋";

    List<AbstractVerbs> skills;
    override protected void Awake()
    {
        
        buffName = "亢奋";
        description = "消除自身，给缺少5能量的动词充能1点";
        book = BookNameEnum.ZooManual;
        maxTime = 2;
        base.Awake();
        //缺少能量的补充
        skills = this.GetComponent<AbstractCharacter>().skills;

        chara.teXiao.PlayTeXiao("kangFeng");
 
    }
    public int nl=1;
    public override void Update()
    {
        base.Update();
        if (skills.Count == 0) return;

        foreach (var _skill in skills)
        {
            if (_skill.needCD - _skill.CD >= 5)
            {
                print("_skill" + _skill.wordName);
                _skill.CD += nl;
                Destroy(this);
            }
        }
    }
    private void OnDestroy()
    {
        base.OnDestroy();
    }
}
