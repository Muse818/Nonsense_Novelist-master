using UnityEngine;
using UnityEngine.UI;

///<summary>
///血条UI
///血条和蓝条的制作方法
///在角色预制体下面建立两个空物体HP和SP，设置HP和SP的位置
///然后将脚本healthbar和SPbar拖拽给角色预制体
///</summary>
class HealthBar : MonoBehaviour
{
    /// <summary>血条预制体 </summary>
    public GameObject healthBarPrefab;
    /// <summary>角色头顶血条的位置 </summary>
    private Transform hpBarPoint;
    /// <summary>剩余血量数值 </summary>
    private Image healthSlider;
    /// <summary>角色位置 </summary>
    private GameObject UIbar;
    /// <summary>获取该角色 </summary>
    private AbstractCharacter charaComponent;
    /// <summary>条位置 </summary>
    private Transform[] barPoint;
    public static bool isDead = false;

    public void Start()
    {
        charaComponent = gameObject.GetComponent<AbstractCharacter>();
        barPoint = gameObject.GetComponentsInChildren<Transform>();
        hpBarPoint = barPoint[1];
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "UIcanvas")
            {
                UIbar = Instantiate(healthBarPrefab, canvas.transform);
                healthSlider = UIbar.transform.GetChild(0).GetComponent<Image>();
            }
        }
    }
    public void FixedUpdate()
    {
        UpdateHealthBar(charaComponent.hp, charaComponent.maxHp);
        if (UIbar != null)
        {
            UIbar.transform.position = hpBarPoint.position;
        }
    }
    /// <summary>
    /// 更新血量数值
    /// </summary>
    /// <param name="currentHP">当前血量</param>
    /// <param name="maxHP">总血量</param>
    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        if (currentHP <= 0) {
            Destroy(UIbar.gameObject);
            isDead = true;
        }       
        float sliderPercent = (float)currentHP / maxHP;
        if(healthSlider!=null)
        healthSlider.fillAmount = sliderPercent;
    }
}
