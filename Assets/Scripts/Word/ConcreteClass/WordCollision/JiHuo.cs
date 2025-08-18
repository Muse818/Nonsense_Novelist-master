using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 碰撞机制：暗喻 激活
/// </summary>
public class JiHuo : WordCollisionShoot
{

    static public string s_description = "与墙壁碰撞3次后，揭示暗喻";
    static public string s_wordName = "暗喻 激活";

    /// <summary>碰撞次数 </summary>
    private int count = 0;
    Color color = Color.green+ Color.white * 0.6f;
    Color colorWhite = new Color(1, 1, 1, 0);

    public override void Awake()
    {
        base.Awake();
        //absWord = Shoot.abs;
        this.GetComponent<SpriteRenderer>().color  += new Color(color.r, color.g, color.b, 0); ;
    }
    bool hasBeenTrigger = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CharacterManager.instance.pause)
            return;
        if (count > 3)
            return;
        if (collision.transform.tag == "wall")
        {
            this.GetComponent<SpriteRenderer>().color -= colorWhite * 0.3f;
            count++;
            Debug.Log("（激活）碰撞次数现在是"+count);
        }
         
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //给absWord赋值
        //absWord = Shoot.abs;
        if (CharacterManager.instance.pause)
            return; 
        if (hasBeenTrigger)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            hasBeenTrigger = true;
            AbstractCharacter character = collision.gameObject.GetComponent<AbstractCharacter>();


            if (character.myState.nowState == character.myState.allState.Find(p => p.id == AI.StateID.dead)) return;


            character.CreateFloatWord(absWord.wordName, FloatWordColor.getWord, false);
            //判断该词条是形容词/动词/名词
            //先把absWord脚本挂在角色身上，然后调用角色身上的useAdj
            if (absWord.wordKind == WordKindEnum.verb)
            {
                AbstractVerbs b = this.GetComponent<AbstractVerbs>();
                character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);
                Destroy(this.gameObject);

            }
            else if (absWord.wordKind == WordKindEnum.adj)
            {
                AbstractAdjectives adj = collision.gameObject.AddComponent(absWord.GetType()) as AbstractAdjectives;
                (adj as IJiHuo).JiHuo(count >= 3);
                adj.UseAdj(collision.gameObject.GetComponent<AbstractCharacter>());
                Destroy(this.gameObject);
            }
            else if (absWord.wordKind == WordKindEnum.noun)
            {
                AbstractItems adj = collision.gameObject.AddComponent(absWord.GetType()) as AbstractItems;
                (adj as IJiHuo).JiHuo(count>=3);
                adj.UseItem(collision.gameObject.GetComponent<AbstractCharacter>());
                Destroy(this.gameObject);
            }
        }
    }
}
interface IJiHuo
{
    public void JiHuo(bool value);
}
