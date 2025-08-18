using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基础碰撞
/// </summary>
public class Common : WordCollisionShoot
{
    private void Awake()
    {
        //absWord = Shoot.abs;
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (CharacterManager.instance.pause)
            return;
        //给absWord赋值
        //absWord = Shoot.abs;

        //如果身上还挂着有其他的机制
        if (this.GetComponentsInChildren<WordCollisionShoot>().Length>1)
            return;


        base.OnTriggerEnter2D(collision);
    }
}
