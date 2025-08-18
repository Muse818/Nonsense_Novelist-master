using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：冷香丸
/// </summary>
class LengXiangWan : AbstractItems
{
    static public string s_description = "恢复+3，每10s净化1层负面状态";
    static public string s_wordName = "冷香丸";
    static public int s_rarity = 2;
    static public int s_useTimes = 4;
    public override void Awake()
    {
        base.Awake();
        itemID = 2;
        wordName = "冷香丸";
        bookName = BookNameEnum.HongLouMeng;
        rarity = 2;
        useTimes = 4;
        VoiceEnum = MaterialVoiceEnum.materialNull;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<XuWu_YunSu>();

        description = "恢复+3，每10s净化1层负面状态";

        nowTime = 0;
    }

    WaitForSeconds waitTime = new WaitForSeconds(10);
    Coroutine cor = null;
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "XuWu_YunSu";
        return _s;
    }


    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.cure += 3;
        if (cor != null)
            StopCoroutine(cor);
        cor = StartCoroutine(Wait());
       

    }
    IEnumerator Wait()
    {
        while (this.TryGetComponent<AbstractCharacter>(out var _c))
        {
            yield return waitTime;
            _c.DeleteBadBuff(1);
        }
    }


    float nowTime;
    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        base.End();
       aim.cure -= 3;
    }
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "白牡丹花蕊、白荷花花蕊、白芙蓉花蕊、白梅花花蕊......等十年未必都这样巧能做出这冷香丸呢！”" + character.wordName + "说道。";

    }
}
