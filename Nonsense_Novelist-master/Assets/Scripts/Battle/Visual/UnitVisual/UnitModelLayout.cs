using System;
using Spine.Unity;
using UnityEngine;

public enum UnitAnimType
{
    None = 0,
    Anim,
    Spine,
}

public class UnitModelLayout : MonoBehaviour
{
    protected Animator mAnimator = null;
    protected AnimEventReceiver mAnimEvents = null;
    protected SkeletonAnimation mSpineAnimator = null;
    protected AudioSource mAudioSource = null;
    
    public Animator Animator => mAnimator;
    public AnimEventReceiver AnimEvents => mAnimEvents;
    public AudioSource AudioSource => mAudioSource;

    
    protected Transform mWeaponPart;
    public Transform WeaponPart => mWeaponPart;

    protected UnitViewBase mOwner = null;
    
    // 角色的各个骨骼的transform
    
    public void Setup(UnitViewBase unitObj)
    {
        mOwner = unitObj;
        
        if (unitObj.mAnimType == UnitAnimType.Anim)
        {
            mAnimator = GetComponent<Animator>();
            mAnimator.runtimeAnimatorController = unitObj.Asset.animatorController;
        }
        else
        {
            mSpineAnimator = GetComponent<SkeletonAnimation>();
        }
        
        
        mAnimEvents = GetComponent<AnimEventReceiver>();
        mAudioSource = GetComponent<AudioSource>();

        mWeaponPart = unitObj.transform.Find("WeaponMuzzle");
        if (mWeaponPart == null)
        {
            mWeaponPart = unitObj.Root;
        }
    }

    public void PlayAnimation(string animName, bool isLoop = false)
    {
        if (Animator != null)
        {
            Animator.Play(animName, 0, 0.0f);
        }
        else if (mSpineAnimator != null)
        {
            mSpineAnimator.state.SetAnimation(0, animName, isLoop);
        }
    }

    private void Update()
    {
    }
}