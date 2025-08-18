using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：晕眩
/// </summary>
public class Dizzy : AbstractBuff
{
    static public string s_description = "无法行动，能量增长停止";
    static public string s_wordName = "晕眩";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "晕眩";
        description = "无法行动，能量增长停止";
        book = BookNameEnum.allBooks;  
        isBad = true;
        maxTime = 2;

        base.Awake();

        record = chara.energy;
        chara.dizzyTime = maxTime;
        if (record > 0.9f) record = 0.9f;

        chara.teXiao.PlayTeXiao("dizzy");


    }

   
    

    public override void Update()
    {
        base.Update();
   
        chara.energy = 0;//能量增长停止
      
    }
}
