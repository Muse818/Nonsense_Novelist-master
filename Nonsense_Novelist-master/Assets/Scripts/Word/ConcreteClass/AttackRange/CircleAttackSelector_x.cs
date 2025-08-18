using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 圆形（球形）区域(弃用）
/// </summary>
class CircleAttackSelector_x 
    {
        /// <summary>
        /// 计算影响区域
        /// </summary>
        /// <param name="attackDistance">射程</param>
        /// <param name="ownTrans">施法者位置</param>
        /// <param name="extra">无额外值</param>
        /// <returns>返回区域内目标数组</returns>
        public GameObject[] AttackRange(int attackDistance, Transform ownTrans,float extra=0)
        {
            //发一个球形射线，找出所有角色碰撞体
            Collider2D[] colliders = Physics2D.OverlapCircleAll(ownTrans.position, attackDistance, 1 << LayerMask.NameToLayer("Character"));
            if (colliders == null || colliders.Length == 0)
                return null;

            //取GameObject
            GameObject[] result=CollectionHelper.Select<Collider2D, GameObject>(colliders, p => p.gameObject);

            //筛选目标
            result = CollectionHelper.FindAll<GameObject>(result,
                p => p.GetComponent<AbstractCharacter>().hp > 0);//存活

            //将所有目标按距离升序
            CollectionHelper.OrderBy<GameObject ,float>(result,p => Vector3.Distance(ownTrans.position, p.transform.position));
            
            return result;

        }
    }
