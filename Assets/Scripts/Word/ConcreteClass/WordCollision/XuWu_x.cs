using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///弃用：虚无
/// </summary>
public class XuWu_x : WordCollisionShoot
{
    public override void Awake()
    {
        base.Awake();

    }
    private void Update()
    {
        //时间结束销毁词条
        if (VanishTime(10))
        {
            Destroy(this.gameObject);
        }
    }
    public override bool VanishTime(float time)
    {
        return base.VanishTime(time);
    }
    /// <summary>
    /// 任何一方为trigger则调用该函数
    /// 穿越角色不消失，直至时间结束消失
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            this.GetComponent<Collider2D>().isTrigger = true;

            AbstractCharacter character = collision.gameObject.GetComponent<AbstractCharacter>();
            
            //给absWord赋值
           // absWord = Shoot.abs;
            //判断该词条是形容词/动词/名词
            //先把absWord脚本挂在角色身上，然后调用角色身上的useAdj
            if (absWord.wordKind == WordKindEnum.verb)
            {
                AbstractVerbs b = this.GetComponent<AbstractVerbs>();
                character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);

            }
            else if (absWord.wordKind == WordKindEnum.adj)
            {
                AbstractAdjectives adj = collision.gameObject.AddComponent(absWord.GetType()) as AbstractAdjectives;
                adj.UseAdj(collision.gameObject.GetComponent<AbstractCharacter>());
            }
            else if (absWord.wordKind == WordKindEnum.noun)
            {
                AbstractItems noun = collision.gameObject.AddComponent(absWord.GetType()) as AbstractItems;
                noun.UseItem(collision.gameObject.GetComponent<AbstractCharacter>());
            }
        }
        this.GetComponent<Collider2D>().isTrigger = false;

    }

}
