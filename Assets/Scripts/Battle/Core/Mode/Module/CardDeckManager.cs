using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : BattleModule
{
    protected WordTable.Data mCurrentCard = null;
    protected Queue<WordTable.Data> mPreviewCards = new Queue<WordTable.Data>();

    protected bool mHasShuffleDeck = false;
    
    public enum EState
    {
        None = 0,
        Lock,
        Loading,
        Ready,
    }
    
    public class LoadSlot
    {
        public EState mState = EState.None;
        public int mNext = 0;
        public float mLoadTimer = 0;
        public WordTable.Data mWord = null;
    }
    
    protected int mLockedHead = 0;
    public int LockedHead => mLockedHead;
    
    protected int mUnlockedHead = 0;
    public int UnlockedHead => mUnlockedHead;
    
    protected int mUnlockedTail = 0;
    public int UnlockedTail => mUnlockedTail;

    protected List<LoadSlot> mLoadSlots = new List<LoadSlot>();
    public List<LoadSlot> LoadSlots => mLoadSlots;

    // 未抽取的牌库
    protected List<WordTable.Data> mDeck = new List<WordTable.Data>();
    // 弃牌堆
    protected List<WordTable.Data> mDiscardPile = new List<WordTable.Data>();

    public override void Init()
    {
        AddInitCardDeck();
        
        for (int i = 0; i < BattleConfig.mData.word.maxLoadCount; ++i)
        {
            mLoadSlots.Add(new LoadSlot()
            {
                mState = EState.Lock,
                mNext = i + 1,
                mLoadTimer = BattleConfig.mData.word.loadSec,
                mWord = null,
            });
        }

        mUnlockedHead = -1;
        mUnlockedTail = -1;
        mLockedHead = 0;
        mLoadSlots[^1].mNext = -1;
        
        EventManager.Invoke(EventEnum.DeckUpdate, mUnlockedHead, mLockedHead);
    }

    public override void Update(float deltaSec)
    {
        if (!Battle.BattlePhase.IsCombat)
            return;
        
        foreach (var slot in mLoadSlots)
        {
            if (slot.mState != EState.Loading)
                continue;

            slot.mLoadTimer -= deltaSec;
            if (slot.mLoadTimer <= 0)
            {
                slot.mLoadTimer = BattleConfig.mData.word.loadSec;
                slot.mWord = LoadWord();
                slot.mState = EState.Ready;

                if (mLoadSlots.IndexOf(slot) == UnlockedHead)
                {
                    EventManager.Invoke(EventEnum.ShootCardUpdate, mUnlockedHead, mLockedHead);
                }
            }
        }
    }

    public override void OnEnterCombatPhase()
    {
        if (!mHasShuffleDeck)
        {
            ShuffleDeck();
            
            UnlockLoadSlot(BattleConfig.mData.word.initLoadCount);
            
            mHasShuffleDeck = true;
        }
    }

    protected override void OnDisposed()
    {
        base.OnDisposed();

        mCurrentCard = null;
        mPreviewCards.Clear();
        mDeck.Clear();
        mDiscardPile.Clear();
    }
    

    /// <summary>
    ///初始卡牌
    /// </summary>
    private void AddInitCardDeck()
    {
        foreach (var word in WordTable.DataList.Values)
        {
            if (!word.mForbidden)
            {
                AddCardToDeck(word);
            }
        }
    }

    /// <summary>
    /// 洗牌操作
    /// </summary>
    private void ShuffleDeck()
    {
        for (int i = 0; i < mDeck.Count; i++)
        {
            int randomIndex = Random.Range(0, mDeck.Count);
            (mDeck[i], mDeck[randomIndex]) = (mDeck[randomIndex], mDeck[i]);
        }
    }
    
    /// <summary>
    /// 解锁槽位
    /// </summary>
    private void UnlockLoadSlot(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var slot = GetLoadSlot(mLockedHead);
            if (slot == null)
                break;
            
            slot.mWord = LoadWord();
            slot.mState = EState.Ready;

            if (mUnlockedHead == -1)
            {
                mUnlockedHead = mLockedHead;
                mUnlockedTail = mLockedHead;
                
                EventManager.Invoke(EventEnum.ShootCardUpdate, mUnlockedHead, mLockedHead);
            }
            else
            {
                mLoadSlots[mUnlockedTail].mNext = mLockedHead;
                mUnlockedTail = mLockedHead;
            }
            
            mLockedHead = slot.mNext;
            slot.mNext = mUnlockedHead;
        }
        
        EventManager.Invoke(EventEnum.DeckUpdate, mUnlockedHead, mLockedHead);
    }

    /// <summary>
    /// 填充卡槽
    /// </summary>
    private WordTable.Data LoadWord()
    {
        if (mDeck.Count == 0)
        {
            RefillDeckFromDiscardPile();
        }

        var word = mDeck[^1];
        mDeck.RemoveAt(mDeck.Count - 1);
        
        return word;
    }
    
    
    /// <summary>
    /// 获取卡槽
    /// </summary>
    public LoadSlot GetLoadSlot(int index)
    {
        if (index < 0 || index >= mLoadSlots.Count)
            return null;
        
        return mLoadSlots[index];
    }
    
    /// <summary>
    /// 获取当前卡牌
    /// </summary>
    public WordTable.Data GetCurrentCard()
    {
        var slot = GetLoadSlot(mUnlockedHead);
        if (slot != null)
        {
            return slot.mWord;
        }

        return null;
    }
    
    /// <summary>
    /// 是否有已经加载好的卡牌
    /// </summary>
    public bool HasLoadCard()
    {
        return GetCurrentCard() != null;
    }

    /// <summary>
    /// 使用当前卡牌
    /// </summary>
    public void UseCurrentCard()
    {
        var slot = GetLoadSlot(mUnlockedHead);
        if (slot is { mState: EState.Ready })
        {
            slot.mWord = null;
            slot.mLoadTimer = BattleConfig.mData.word.loadSec;
            slot.mState = EState.Loading;

            mUnlockedTail = mUnlockedHead;
            mUnlockedHead = slot.mNext;
        }
        
        EventManager.Invoke(EventEnum.DeckUpdate, mUnlockedHead, mLockedHead);
        EventManager.Invoke(EventEnum.ShootCardUpdate, mUnlockedHead, mLockedHead);
    }

    /// <summary>
    /// 弃牌堆加入牌库并洗牌
    /// </summary>
    private void RefillDeckFromDiscardPile()
    {
        if (mDiscardPile.Count == 0)
        {
            Debug.LogError("discardPile.Count == 0");
            return;
        }

        mDeck.AddRange(mDiscardPile);
        mDiscardPile.Clear();
        ShuffleDeck();
    }

    /// <summary>
    /// 增加新卡牌到牌库
    /// </summary>
    public void AddCardToDeck(WordTable.Data cardData)
    {
        if (cardData is { mForbidden: false })
        {
            mDeck.Add(cardData);
        }
    }

    /// <summary>
    /// 移除某张卡牌
    /// </summary>
    public void RemoveCardFromGame(WordTable.Data cardData)
    {
        mDeck.Remove(cardData);
        mDiscardPile.Remove(cardData);

        // 如果当前卡牌被移除，立即更新当前卡牌
        if (mCurrentCard == cardData)
        {
            UseCurrentCard();
        }
    }

    public Queue<WordTable.Data> GetPreviewCards()
    {
        return new Queue<WordTable.Data>(mPreviewCards);
    }
}
