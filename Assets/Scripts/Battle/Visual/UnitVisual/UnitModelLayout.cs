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
        
        mWeaponPart = unitObj.transform.Find("WeaponMuzzle");
        if (mWeaponPart == null)
        {
            mWeaponPart = unitObj.Root;
        }
        
        mAnimEvents = GetComponent<AnimEventReceiver>();
        mAudioSource = GetComponent<AudioSource>();
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

    public Vector3 GetWeaponPos()
    {
        // if (mOwner.mAnimType == UnitAnimType.Anim)
        // {
        //     Spine.Bone bone = mSpineAnimator.Skeleton.FindBone(mOwner.Asset.weaponSocket);
        //
        //     if (bone != null)
        //     {
        //         return mSpineAnimator.transform.TransformPoint(new Vector3(bone.WorldX, bone.WorldY, 0));
        //     }
        // }
        
        return mWeaponPart.position;
    }

    private void Update()
    {
    }
}