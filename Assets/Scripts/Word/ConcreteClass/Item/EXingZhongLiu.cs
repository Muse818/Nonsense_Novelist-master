using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：恶性肿瘤
/// </summary>
class EXingZhongLiu : AbstractItems
{
    static public string s_description = "每10s随机获得1种减益状态，持续5s";
    static public string s_wordName = "恶性肿瘤";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();
        itemID = 17;
        wordName = "恶性肿瘤";
        bookName = BookNameEnum.FluStudy;
        description = "每10s随机获得1种减益状态，持续5s";

        VoiceEnum = MaterialVoiceEnum.Ceram;
        useTimes = 6;
        rarity = 1;

    
    }


    float record;
    WaitForSeconds waitTime = new WaitForSeconds(10);
    Coroutine cor = null;

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        if (cor != null) StopAllCoroutines();
        cor = StartCoroutine(Wait());
        
    }



    IEnumerator Wait()
    {
        while (TryGetComponent<AbstractCharacter>(out var _c))
        {
            yield return waitTime;
            _c.AddRandomBuff(true, 1, 5);
        }
    }

    public override void End()
    {
        if (cor != null) StopAllCoroutines();
        cor = StartCoroutine(Wait());
        base.End();
  
    }

    
}
