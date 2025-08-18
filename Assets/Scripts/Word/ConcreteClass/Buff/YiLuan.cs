using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：意乱
/// </summary>
public class YiLuan : AbstractBuff
{
    static public string s_description = "<sprite name=\"san\">-20%，期间有概率被情迷";
    static public string s_wordName = "意乱";
    override protected void Awake()
    {


        buffName = "意乱";
        description = "<sprite name=\"san\">-20%，期间有概率被情迷";
        book = BookNameEnum.allBooks;
        isBad = true;
        isAll = false;
        upup = 2;

        base.Awake();

        chara.sanMul -= 0.3f;
        //持有意乱状态时每3s进行一次检测，20%概率陷入3s”情迷“，若为2层意乱，则情迷概率为40%
        if (coroutine != null)
        {
            StopCoroutine(coroutine); coroutine = null;
        }
            
        coroutine = StartCoroutine(Detact());

    }
    Coroutine coroutine;
    WaitForSeconds waitTwoS = new WaitForSeconds(3);
    IEnumerator Detact()
    {
        while (true)
        {
            int _random=0;
            var _count = chara.gameObject.GetComponents<YiLuan>();
            if (_count.Length > 1)//则第一个起效
            {
                if (_count[0] == this)
                {
                    _random = Random.Range(0, 100);
                    if (_random < 40)
                    {
                       var _b=chara.gameObject.AddComponent<QingMi>();
                        _b.maxTime = 3;
                    }
                }
            }
            else //如果身上只有一层
            {
                _random = Random.Range(0, 100);
                if (_random < 20)
                {
                    var _b = chara.gameObject.AddComponent<QingMi>();
                    _b.maxTime = 3;
                }
            }
            yield return waitTwoS;
        }
    }

    public override void Update()
    {
        if (CharacterManager.instance.pause) return;
        maxTime -= Time.deltaTime;
        if (maxTime < 0)
        {
            Destroy(this);
            if (coroutine != null)
            {
                StopCoroutine(coroutine); coroutine = null;
            }

        }
    }

    public override void OnDestroy()
    {
        chara.sanMul += 0.3f;
        if (coroutine != null)
        {
            StopCoroutine(coroutine); coroutine = null;
        }
         base.OnDestroy();
    }
}
