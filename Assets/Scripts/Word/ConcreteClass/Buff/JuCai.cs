using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：聚财（伪buff）
/// </summary>
public class JuCai : AbstractBuff
{
    static public string s_description = "使角色获得的下一个名词翻倍";
    static public string s_wordName = "聚财的";

    List<AbstractVerbs> skills;
    override protected void Awake()
    {

        buffName = "聚财的";
        description = "使角色获得的下一个名词翻倍";
        book = BookNameEnum.CrystalEnergy;
        maxTime = Mathf.Infinity;
        upup = 1;
        isBad = false;
        isAll = false;

        //awake内容，删除了获得buff飘字
        chara = GetComponent<AbstractCharacter>();
        int num = 0;
        AbstractBuff[] buffs = GetComponents<AbstractBuff>();

        for (int i = buffs.Length - 1; i > -1; i--)
        {//倒序,最早的buff先被删除
            if (buffs[i].buffName == buffName)
            {
                num++;
                if (num > upup)
                {

                    Destroy(buffs[i]);
                }
            }
        }
        chara.AddBuff(this);



        //检测角色获得名词情况

        chara.event_AddNoun += WhenGetWord;
    }

    public void WhenGetWord(AbstractItems _item)
    {
        chara.event_AddNoun -= WhenGetWord;
        chara.gameObject.AddComponent(_item.GetType());
        chara.AddNoun(_item);
        Destroy(this);
    }

    public override void Update()
    {
        base.Update();
    }

     private void OnDestroy()
    {

        base.OnDestroy();

    }
}
