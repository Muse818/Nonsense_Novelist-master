using UnityEngine;

///<summary>
///动画行为类
///</summary>
class AnimationActor
{
    private Animation anim;
    public AnimationActor(Animation anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animName">动画片段名称</param>
    public void Play(string animName)
    {
        anim.Play(animName);
    }
    /// <summary>
    /// 判断动画是否播放
    /// </summary>
    /// <param name="animName">动画片段名称</param>
    /// <returns></returns>
    public bool IsPlaying(string animName)
    {
        return anim.IsPlaying(animName);
    }
}
