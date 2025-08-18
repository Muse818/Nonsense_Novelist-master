using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 站位
/// </summary>
public class Situation : MonoBehaviour
{
    /// <summary>几号位（手动输入）</summary>
    public float number;//1-10,boss为5.5（这样计算距离时得到0）

    
    /// <summary>所有站位</summary>
    static public Situation[] allSituation;


    public void Awake()
    {
        if (this.number == 4.5f)
            GetComponent<SpriteRenderer>().enabled = false;
        if (this.number == 4.1f)
            GetComponent<SpriteRenderer>().enabled = false;
        if (this.number == 4.2f)
            GetComponent<SpriteRenderer>().enabled = false;
        if (this.number == 4.3f)
            GetComponent<SpriteRenderer>().enabled = false;
        CaculateAllSituation();
    }

    /// <summary>计算距离</summary>
    static public int Distance(Situation a, Situation b)
    {
        if (a == null|| a.number == 0) 
        {
           return 99900;
        }

      
        if (b== null|| b.number == 0) 
        {
           return 99900;
        }

        return (int)Mathf.Abs(a.number - b.number);
    }

    /// <summary>
    /// 重新计算所有站位
    /// AllCharacter的子物体是所有站位，每个站位的子物体是角色
    /// </summary>
    static public Situation[] CaculateAllSituation()
    {
        if (GameObject.Find("AllCharacter") != null)
        {
            
            allSituation = GameObject.Find("AllCharacter").GetComponentsInChildren<Situation>();
           
            return allSituation;
        }
        return null;
    }


   

}
