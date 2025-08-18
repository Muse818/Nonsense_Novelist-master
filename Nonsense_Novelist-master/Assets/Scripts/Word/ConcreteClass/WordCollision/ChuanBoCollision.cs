using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 碰撞机制：夸张 传播
/// </summary>
public class ChuanBoCollision : WordCollisionShoot
{

    static public string s_description = "命中后，复制词条效果到相邻格子的角色";
    static public string s_wordName = "夸张 传播";


    /// <summary>碰撞次数 </summary>
    private int count = 0;
    Color color = Color.black;
    public override void Awake()
    {
        base.Awake();
        //给absWord赋值
        absWord = Shoot.abs;
    }
    bool hasBeenTrigger = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CharacterManager.instance.pause)
            return;
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenTrigger)
            return;
        ////给absWord赋值
        //absWord = Shoot.abs;
        if (CharacterManager.instance.pause)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
           
            hasBeenTrigger = true;
             //获取角色与相邻角色
             AbstractCharacter character = collision.gameObject.GetComponent<AbstractCharacter>();

            if (character.myState.nowState == character.myState.allState.Find(p => p.id == AI.StateID.dead)) return;


            AbstractCharacter[] nearCharacter = CharacterManager.instance.GetNearBy_C(character.situation);

            character.CreateFloatWord(absWord.wordName, FloatWordColor.getWord, false);
            //判断该词条是形容词/动词/名词
            //先把absWord脚本挂在角色身上，然后调用角色身上的useAdj
            if (absWord.wordKind == WordKindEnum.verb)
            {
                AbstractVerbs b = this.GetComponent<AbstractVerbs>();

                character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);

                //相邻位赋予
                foreach (var cha in nearCharacter)
                {
                    if (cha != null)
                    {
                        character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);
                        cha.CreateFloatWord(absWord.wordName, FloatWordColor.getWord, false);
                    }
                }

                Destroy(this.gameObject);

            }
            else if (absWord.wordKind == WordKindEnum.adj)
            {
                AbstractAdjectives adj = collision.gameObject.AddComponent(absWord.GetType()) as AbstractAdjectives;
                adj.UseAdj(collision.gameObject.GetComponent<AbstractCharacter>());

                //相邻位赋予
                foreach (var cha in nearCharacter)
                {
                    if (cha != null)
                    {
                        
                        AbstractAdjectives _adj = cha.gameObject.AddComponent(absWord.GetType()) as AbstractAdjectives;
                        cha.CreateFloatWord(absWord.wordName, FloatWordColor.getWord, false);
                        _adj.UseAdj(cha);
                    }
                }

                Destroy(this.gameObject);
            }
            else if (absWord.wordKind == WordKindEnum.noun)
            {
                AbstractItems noun = collision.gameObject.AddComponent(absWord.GetType()) as AbstractItems;
                noun.UseItem(collision.gameObject.GetComponent<AbstractCharacter>());

                //相邻位赋予
                foreach (var cha in nearCharacter)
                {
                    if (cha != null)
                    {
                        AbstractItems _noun = collision.gameObject.AddComponent(absWord.GetType()) as AbstractItems;
                        cha.CreateFloatWord(absWord.wordName, FloatWordColor.getWord, false);
                        noun.UseItem(cha);
                    }
                }

                Destroy(this.gameObject);
            }
        }
    }
}
interface IChuanBo
{
    public void ChuanBo(bool value);
}
