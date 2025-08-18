using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff：共振
/// </summary>
public class GongZhen : AbstractBuff
{

    static public string s_description = "场上每有4个“共振”，四维+1";
    static public string s_wordName = "共振";
    int count=0;
    /// <summary>共振角色数量</summary>
    static int num;
    /// <summary>上次增益记录</summary>
    private int record;
    override protected void Awake()
    {
      
        buffName = "共振";
        book = BookNameEnum.CrystalEnergy;
        description = "场上每有4个“共振”，四维+1";

        base.Awake();

        //if (this.GetComponents<GongZhen>().Length == 1)
        //{
        //    num++;
        //}
        //GongZhen[] all = CharacterManager.father.GetComponentsInChildren<GongZhen>();
        //foreach (GongZhen gz in all)
        //{
        //    gz.NumChanged();
        //}
        GZCountChange();
    }
    GameObject allCha;
    GongZhen[] allGZ;
    private void GZCountChange()
    {
        //每个共振增加、删除时，所有共振都执行一次数量检测+值变化
        allCha = CharacterManager.instance.gameObject;
        allGZ = allCha.GetComponentsInChildren<GongZhen>();
        num = Mathf.FloorToInt(allGZ.Length / 4);
       // print("现在场上有" + allGZ.Length + "个共振，也就是" + num + "的效果");
        foreach (var _gz in allGZ)
        {
            _gz.NumChanged(num);
        }
    }
    public void NumChanged(int _num)
    {
        num = _num;
        if (Mathf.Abs(num - record) >= 1)
        {
           
            chara.atk += (num - record);
            chara.def += (num - record);
            chara.psy += (num - record);
            chara.san += (num - record);
            record = num;
        }
       
    }


    private void OnDestroy()
    {
        base.OnDestroy();
        if (GetComponent<AbstractCharacter>() == null) return;
        GZCountChange();
        //chara.atk -= record;
        //chara.def -= record;
        //chara.psy -= record;
        //chara.san -= record;
        chara.def -= 1 * num;
        chara.san -= 1 * num;
        chara.psy -= 1 * num;
        chara.atk -= 1 * num;

        //if(this.GetComponents<GongZhen>().Length==1)//表示就剩自己
        //{
        //    num--;
        //}
        //GongZhen[] all = CharacterManager.father.GetComponentsInChildren<GongZhen>();
        //foreach (GongZhen gz in all)
        //{
        //    if(gz!=this)
        //    gz.NumChanged();
        //}
    }
}
