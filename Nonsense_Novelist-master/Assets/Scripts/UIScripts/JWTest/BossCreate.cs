using UnityEngine;
/// <summary>
/// 24.1.24策划debug功能测试boss
/// </summary>
public class BossCreate : MonoBehaviour
{
    public GameObject huaiYiZhuYi;
    public void CreateTestBoss()
    {
        GameObject boss = Instantiate(huaiYiZhuYi);
        boss.transform.SetParent(GameObject.Find("Circle4.5").transform);
        boss.transform.localPosition = Vector3.zero;


        //生成调整
        boss.GetComponentInChildren<AI.MyState0>().enabled = true;
        boss.GetComponent<AbstractCharacter>().enabled = true;
        boss.gameObject.AddComponent(typeof(AfterStart));
        Destroy(boss.GetComponent<CharacterMouseDrag>());

        //设置一个随机目标，使其进入攻击状态
        IAttackRange attackRange = new SingleSelector();

        //这一句越级了
        AbstractCharacter[] a = attackRange.CaculateRange(100, boss.GetComponent<AbstractCharacter>().situation, NeedCampEnum.all,false);
        boss.GetComponentInChildren<AI.MyState0>().aim.Add(a[0]);
    }
    public void ClosePanel()
    {
        this.transform.parent.gameObject.SetActive(false);
        Time.timeScale = GameMgr.instance.timeSpeed;

    }
}
