using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÔÈËÙ
/// </summary>
public class YunSu : WordCollisionShoot
{

    public override void Awake()
    {
        base.Awake();
        this.gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 0;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (CharacterManager.instance.pause)
            return;
        //¸øabsWord¸³Öµ
        //absWord = Shoot.abs;
        base.OnTriggerEnter2D(collision);
    }
}
