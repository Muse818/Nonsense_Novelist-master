using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色动画组件（不要用Play用这个）
/// </summary>
public class CharaAnim : MonoBehaviour
{
    /// <summary>实际动画组件</summary>
    public Animator anim;
    /// <summary>当前动画名</summary>
    public AnimEnum currentAnim;
    void Start()
    {
        currentAnim=AnimEnum.idle;
    }

    private string newAnimName;//新动画字符串(仅用于↓）
    private string currentAnimName;//当前动画字符串(仅用于↓）
    /// <summary>
    /// 播放动画用这个
    /// </summary>
    /// <param name="paramName">新动画枚举</param>
    public void Play(AnimEnum newAnimEnum)
    {
        newAnimName = Enum.GetName(typeof(AnimEnum),newAnimEnum);
        currentAnimName = Enum.GetName(typeof(AnimEnum), currentAnim);
        anim.SetBool(currentAnimName, false); 
        anim.SetBool(newAnimName, true);
        currentAnim = newAnimEnum;
    }
    /// <summary>
    /// 判断是否播放完毕
    /// </summary>
    /// <param name="animEnum"></param>
    /// <returns></returns>
    public bool IsEnd(AnimEnum animEnum)
    {
        AnimatorStateInfo animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        //播放的不是该动画
        if(!animatorInfo.IsName(Enum.GetName(typeof(AnimEnum), animEnum)))
        {
            return true;
        }
        //播放超过1次
        else if ((animatorInfo.normalizedTime > 1.0f) )
        {
            return true;
        }
        else
            return false;
    }


    public void SetSpeed(AnimEnum animEnum, float speed)
    {
            anim.speed = 1 * speed;

    }
}