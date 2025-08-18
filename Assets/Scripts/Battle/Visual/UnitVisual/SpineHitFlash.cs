using Spine.Unity;
using UnityEngine;

public class SpineHitFlash : MonoBehaviour
{
    //[Header("闪红设置")]
    //public float flashDuration = 0.007f;

    private SkeletonAnimation skeletonAnimation;
    private Color originalColor;
    private bool isFlashing = false;
    float hpPercent = 1;

    void Start()
    {
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitTakeDamage,OnTakeDamage);
    }
    public void OnTakeDamage(BattleUnit target)
    {
        if (target.ID == 1)
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            if (skeletonAnimation != null)
            {
                originalColor = skeletonAnimation.Skeleton.GetColor();
            }
            hpPercent = target.HpPercent;
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        if (!isFlashing && skeletonAnimation != null)
        {
            StartCoroutine(SpineFlashRoutine());
        }
    }

    private System.Collections.IEnumerator SpineFlashRoutine()
    {
        isFlashing = true;

        if (hpPercent >= 0.9)
        {
            skeletonAnimation.Skeleton.SetColor(new Color(1f, 0.784f, 0.784f));
        }else if(hpPercent>=0.5&& hpPercent < 0.9)
        {
            skeletonAnimation.Skeleton.SetColor(new Color(1f, 0.5f, 0.5f));
        }
        else
        {
            skeletonAnimation.Skeleton.SetColor(new Color(1f, 0f, 0f));
        }
        // 等待指定时间
        yield return new WaitForSeconds(0.07f);

        // 恢复原始颜色
        skeletonAnimation.Skeleton.SetColor(originalColor);

        isFlashing = false;
    }
}