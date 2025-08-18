using UnityEngine.UI;
using UnityEngine;

///<summary>
///血条圆圈加载
///</summary>
class HealthCircleBar : MonoBehaviour
{
    /// <summary>战斗场景的林黛玉</summary>
    private AbstractCharacter linDaiYu;
    /// <summary>战斗场景的贾宝玉</summary>
    private AbstractCharacter jiaBaoYu;
    /// <summary>战斗场景的贾母</summary>
    private AbstractCharacter jiaMu;
    /// <summary>战斗场景的刘姥姥</summary>
    private AbstractCharacter liuLaoLao;
    /// <summary>战斗场景的王熙凤</summary>
    private AbstractCharacter wangXiFeng;

    private void Start()
    {
        linDaiYu = GameObject.Find("LinDaiYu").GetComponent<AbstractCharacter>();
        //jiaBaoYu = GameObject.Find("JiaBaoYu").GetComponent<AbstractCharacter>();
        //jiaMu = GameObject.Find("JiaMu").GetComponent<AbstractCharacter>();
        //liuLaoLao = GameObject.Find("LiuLaoLao").GetComponent<AbstractCharacter>();
        wangXiFeng = GameObject.Find("WangXiFeng").GetComponent<AbstractCharacter>();
    }
    private void FixedUpdate()
    {
        if (this.transform.GetChild(0).gameObject.name== "LinDaiYu_Circle"&& linDaiYu != null)
        {
            float hpPercent=(float)linDaiYu.hp / linDaiYu.maxHp;
            this.GetComponent<Image>().fillAmount = hpPercent;
        }
        if (this.transform.GetChild(0).gameObject.name== "JiaBaoYu_Circle" && jiaBaoYu != null)
        {
            float hpPercent=(float)jiaBaoYu.hp / jiaBaoYu.maxHp;
            this.GetComponent<Image>().fillAmount = hpPercent;
        }
        if (this.transform.GetChild(0).gameObject.name== "JiaMu_Circle" && jiaMu != null)
        {
            float hpPercent=(float)jiaMu.hp / jiaMu.maxHp;
            this.GetComponent<Image>().fillAmount = hpPercent;
        }
        if (this.transform.GetChild(0).gameObject.name== "LiuLaoLao_Circle" && liuLaoLao != null)
        {
            float hpPercent=(float)liuLaoLao.hp / liuLaoLao.maxHp;
            this.GetComponent<Image>().fillAmount = hpPercent;
        }
        if (this.transform.GetChild(0).gameObject.name == "WangXiFeng_Circle" && wangXiFeng != null)
        {
            float hpPercent = (float)wangXiFeng.hp / wangXiFeng.maxHp;
            this.GetComponent<Image>().fillAmount = hpPercent;
        }

    }
}
