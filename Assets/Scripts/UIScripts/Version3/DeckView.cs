
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckView : MonoBehaviour
{
    protected class Slot
    {
        public Transform mRoot = null;
        public Slider mSlider = null;
        public Text mName = null;
        public Transform mLockRoot = null;
        public Transform mTextRoot = null;
        public CardDeckManager.LoadSlot mCoreSlot = null;
        public CardDeckManager.EState mState = CardDeckManager.EState.None;

        public void Bind(GameObject slotObj, CardDeckManager.LoadSlot coreSlot)
        {
            mCoreSlot = coreSlot;
            
            mRoot = slotObj.gameObject.transform;
            mSlider = slotObj.transform.Find("Slider").GetComponent<Slider>();
            mLockRoot = slotObj.transform.Find("LockImage");
            mTextRoot = slotObj.transform.Find("BGText");
            mName = mTextRoot.Find("WordName").GetComponent<Text>();
            mRoot.localScale = Vector3.one;
            
            slotObj.SetActive(true);
            
            Update();
        }

        public void SetState(CardDeckManager.EState state)
        {
            if (mState == state)
                return;

            mState = state;
            switch (mState)
            {
                case CardDeckManager.EState.Lock:
                    
                    if (mTextRoot != null)
                        mTextRoot.gameObject.SetActive(false);
                    
                    if (mLockRoot != null)
                        mLockRoot.gameObject.SetActive(true);
                    
                    break;
                
                case CardDeckManager.EState.Loading:
                    
                    if (mTextRoot != null)
                        mTextRoot.gameObject.SetActive(false);
                    
                    if (mLockRoot != null)
                        mLockRoot.gameObject.SetActive(false);
                    
                    break;
                
                case CardDeckManager.EState.Ready:

                    if (mTextRoot != null)
                        mTextRoot.gameObject.SetActive(true);
                    
                    if (mLockRoot != null)
                        mLockRoot.gameObject.SetActive(false);

                    if (mName)
                        mName.text = mCoreSlot.mWord.mName;
                    
                    break;
            }
        }

        public void Update()
        {
            if (mCoreSlot == null)
                return;
            
            SetState(mCoreSlot.mState);
            
            if (mState == CardDeckManager.EState.Loading)
            {
                mSlider.value = 1.0f - mCoreSlot.mLoadTimer / Mathf.Max(BattleConfig.mData.word.loadSec, 0.001f);
            }
        }
    }

    protected List<Slot> mSlots = new List<Slot>();
    
    protected GameObject mSlotObjTemp = null;
    protected WordInformation mWordInfo = null;
    

    public void Awake()
    {
        TryFindSlotTemp();
        
        mWordInfo = transform.parent.Find("WordInformation").GetComponent<WordInformation>();
    }

    private void LateUpdate()
    {
        foreach (var slot in mSlots)
        {
            slot.Update();
        }
    }

    public void TryFindSlotTemp()
    {
        if (mSlotObjTemp != null)
            return;
        
        mSlotObjTemp = transform.Find("SlotTemp").gameObject;
        mSlotObjTemp.SetActive(false);
        mSlotObjTemp.transform.SetParent(null);
    }

    public void Setup(CardDeckManager deckManager)
    {
        InitSlots(deckManager.LoadSlots, deckManager.UnlockedHead, deckManager.LockedHead);
        
        EventManager.Subscribe<int, int>(EventEnum.DeckUpdate, SortSlots);
        EventManager.Subscribe(EventEnum.ShootCardUpdate,
            () => { mWordInfo.ChangeInformation(BattleRunner.Battle.CardDeckManager.GetCurrentCard());});
    }

    public void InitSlots(List<CardDeckManager.LoadSlot> coreSlots, int unlockHead, int lockedHead)
    {
        transform.DetachChildren();
        mSlots.Clear();
        
        foreach (var it in coreSlots)
        {
            var newSlot = new Slot();
            newSlot.Bind(Instantiate(mSlotObjTemp, transform), it);
            mSlots.Add(newSlot);
        }

        SortSlots(unlockHead, lockedHead);
    }

    public void SortSlots(int unlockHead, int lockedHead)
    {
        transform.DetachChildren();

        int idx = unlockHead;
        while (idx >= 0 && idx < mSlots.Count)
        {
            mSlots[idx].mRoot.SetParent(transform);
            idx = mSlots[idx].mCoreSlot.mNext;

            if (idx == unlockHead)
            {
                break;
            }
        }
        
        idx = lockedHead;
        while (idx >= 0 && idx < mSlots.Count)
        {
            mSlots[idx].mRoot.SetParent(transform);
            idx = mSlots[idx].mCoreSlot.mNext;
        }
    }
}