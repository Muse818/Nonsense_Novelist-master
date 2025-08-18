using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单体寻找
/// </summary>
public class SingleSelector : IAttackRange
{
    private Situation[] firstResult, secondResult;
    private List<AbstractCharacter> result;

    
    public AbstractCharacter[] CaculateRange(int attackDistance, Situation situation, NeedCampEnum needCamp,bool isJiangXu)
    {
        //射程筛
        firstResult= CollectionHelper.FindAll<Situation>(Situation.allSituation,p=>p.GetComponentInChildren<AbstractCharacter>()!=null && Situation.Distance(situation,p) <= attackDistance);


        //阵营筛
        CampEnum myCamp;
        if (situation == null) { Debug.Log("situation== null"); return null; }
       
        myCamp = situation.transform.GetChild(0).GetComponent<AbstractCharacter>().Camp;



        secondResult= CollectionHelper.FindAll<Situation>(firstResult, p => isAim(myCamp, p.GetComponentInChildren<AbstractCharacter>().Camp, needCamp));

        //降序升序
        if(isJiangXu) CollectionHelper.OrderByDescending(secondResult, p => Situation.Distance(situation, p));
        else CollectionHelper.OrderBy(secondResult, p => Situation.Distance(situation, p));
       


        //转角色
        result = new List<AbstractCharacter>();

        foreach (Situation s in secondResult)
        {
            result.Add(s.GetComponentInChildren<AbstractCharacter>());
        }
       
        return result.ToArray();
    }


    /// <summary>
    /// 随机版本
    /// </summary>
    /// <param name="attackDistance"></param>
    /// <param name="situation"></param>
    /// <returns></returns>
    public AbstractCharacter[] CaculateRange(int attackDistance, Situation situation, bool _ignoreBoss)
    {
        if(_ignoreBoss)//排除boss
            firstResult = CollectionHelper.FindAll<Situation>(Situation.allSituation, p => (p.GetComponentInChildren<AbstractCharacter>() != null)&&(p.GetComponent<Situation>().number!= 4.5f));
        else
            firstResult = CollectionHelper.FindAll<Situation>(Situation.allSituation, p => p.GetComponentInChildren<AbstractCharacter>() != null);

        //转角色
        result = new List<AbstractCharacter>();


        foreach (Situation s in firstResult)
        {
            result.Add(s.GetComponentInChildren<AbstractCharacter>());
        }
       
        return result.ToArray();
    }

    private bool isAim(CampEnum myCamp, CampEnum aimCamp, NeedCampEnum needCamp)
    {
        if(needCamp==NeedCampEnum.all)
            return true;
        if((needCamp==NeedCampEnum.friend )&& myCamp==aimCamp)
            return true;
        if((needCamp==NeedCampEnum.enemy) && myCamp!=aimCamp)
            return true;

        return false;
    }
}
