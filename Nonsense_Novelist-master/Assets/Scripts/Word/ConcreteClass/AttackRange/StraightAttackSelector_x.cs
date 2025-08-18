using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 直线攻击(弃用）
/// </summary>
class StraightAttackSelector_x 
{
    /// <summary>
    /// 计算影响区域
    /// </summary>
    /// <param name="attackDistance">射程</param>
    /// <param name="ownTrans">施法者位置</param>
    /// <param name="extra">无额外值</param>
    /// <returns>返回区域内目标数组</returns>
    public GameObject[] AttackRange(int attackDistance, Transform ownTrans, float extra = 1)
    {
        //发出射线，找出所有角色碰撞体
        RaycastHit2D[] hit = Physics2D.RaycastAll(ownTrans.position, ownTrans.forward, attackDistance, 1 << LayerMask.NameToLayer("Character"));

        if (hit == null || hit.Length == 0)
            return null;

        //取GameObject
        GameObject[] result = CollectionHelper.Select<RaycastHit2D, GameObject>(hit, p => p.collider.gameObject);

        //筛选目标
        result = CollectionHelper.FindAll<GameObject>(result,
            p => p.GetComponent<AbstractCharacter>().hp > 0);//存活

        //将所有目标按距离升序
        CollectionHelper.OrderBy<GameObject, float>(result, p => Vector3.Distance(ownTrans.position, p.transform.position));

        return result;

    }
}
