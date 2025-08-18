
using System;
using System.Collections.Generic;
using UnityEngine;

public class WordEntry
{
    public WordType mType;
    public WordSource mSource;
    public bool mIsCopy;
    public float mApplyTime = 0;
    public float mEndTime = 0;

    public float mUpdateTimer = 0;
    public int mPower = 0;
    
    public WordTable.Data mData = null;
    public List<AbilityBase> mAbilities = null;
}

public class WordComponent : UnitComponent
{
    protected Dictionary<WordType, List<WordEntry>> mWordEntries = new Dictionary<WordType, List<WordEntry>>();
    protected List<WordEntry> mAdjectiveWords = null;
    protected List<WordEntry> mVerbWords = null;

    public List<WordEntry> GetWordsByType(WordType wt)
    {
        return mWordEntries.TryGetValue(wt, out var words) ? words : null;
    }

    public string GetFirstPropertyName()
    {
        var list = GetWordsByType(WordType.Property);
        return list is { Count: > 0 } ? list[0].mData.mName : "";
    }

    protected override void OnRegistered()
    {
        mWordEntries.Clear();
        foreach (WordType wt in Enum.GetValues(typeof(WordType)))
        {
            if (wt != WordType.Undefined)
            {
                mWordEntries.Add(wt, new List<WordEntry>());
            }
        }

        mAdjectiveWords = GetWordsByType(WordType.Adjective);
        mVerbWords = GetWordsByType(WordType.Verb);
    }

    public override void Start()
    {
        AddWord(mOwner.Data.mRoles);
    }

    public override void Update(float deltaTime)
    {
        TickAdjWords(deltaTime);
        TickVerbWords(deltaTime);
    }

    public WordEntry AddWord(int wordKind)
    {
        var wordData = WordTable.Find(wordKind);
        if (wordData == null || wordData.mForbidden)
            return null;

        var words = GetWordsByType(wordData.mType);
        if (words == null)
            return null;

        WordEntry entry = new WordEntry();

        entry.mType = wordData.mType;
        entry.mApplyTime = Owner.Battle.Now;
        entry.mData = wordData;

        switch (wordData.mType)
        {
            case WordType.Property:
            case WordType.Noun:
                break;
            
            case WordType.Verb:
                if (words.Count > 0 && words.Count >= BattleConfig.mData.word.maxVerbNum)
                {
                    ClearAbilities(words[0]);
                    
                    words.RemoveAt(0);
                }

                entry.mUpdateTimer = BattleConfig.mData.word.verbPowerInterval;
                entry.mPower = wordData.mInitPower;
                
                break;
            
            case WordType.Adjective:
                entry.mEndTime = entry.mApplyTime + wordData.mDuration;
                break;
            
            default:
                break;
        }
        
        words.Add(entry);

        entry.mAbilities = new List<AbilityBase>();
        
        foreach (var kind in wordData.mAbilities)
        {
            var abi = Owner.AbilityAgent.RegisterAbility(kind);
            if (abi != null)
            {
                abi.WordType = entry.mType;
                entry.mAbilities.Add(abi);
            }
        }
        EventManager.Invoke(EventEnum.AddWord,entry);
        return entry;
    }
    public WordEntry AddWord(int wordKind,WordSource source,bool isCopy=false)
    {
        var wordData = WordTable.Find(wordKind);
        if (wordData == null || wordData.mForbidden)
            return null;

        var words = GetWordsByType(wordData.mType);
        if (words == null)
            return null;

        WordEntry entry = new WordEntry();

        entry.mType = wordData.mType;
        entry.mApplyTime = Owner.Battle.Now;
        entry.mData = wordData;
        entry.mSource = source;
        entry.mIsCopy = isCopy;

        switch (wordData.mType)
        {
            case WordType.Property:
            case WordType.Noun:
                break;

            case WordType.Verb:
                if (words.Count > 0 && words.Count >= BattleConfig.mData.word.maxVerbNum)
                {
                    ClearAbilities(words[0]);

                    words.RemoveAt(0);
                }

                entry.mUpdateTimer = BattleConfig.mData.word.verbPowerInterval;
                entry.mPower = wordData.mInitPower;

                break;

            case WordType.Adjective:
                entry.mEndTime = entry.mApplyTime + wordData.mDuration;
                break;

            default:
                break;
        }

        words.Add(entry);

        entry.mAbilities = new List<AbilityBase>();

        foreach (var kind in wordData.mAbilities)
        {
            var abi = Owner.AbilityAgent.RegisterAbility(kind);
            if (abi != null)
            {
                abi.WordType = entry.mType;
                entry.mAbilities.Add(abi);
            }
        }
        EventManager.Invoke(EventEnum.AddWord, entry);
        return entry;
    }
    public void RemoveWord(WordEntry we)
    {
        if (we?.mData == null)
            return;
        
        var words = GetWordsByType(we.mData.mType);
        if (words == null)
            return;
        
        if (!words.Contains(we)) 
            return;

        ClearAbilities(we);

        words.Remove(we);
    }

    public void ClearAbilities(WordEntry we)
    {
        foreach (var abi in we.mAbilities)
        {
            Owner.AbilityAgent.RemoveAbility(abi);
        }
    }

    protected void TickAdjWords(float deltaTime)
    {
        List<WordEntry> Removed = new List<WordEntry>();
        foreach (var w in mAdjectiveWords)
        {
            if (w.mEndTime > Owner.Battle.Now)
            {
                Removed.Add(w);
            }
        }

        foreach (var w in Removed)
        {
            ClearAbilities(w);
            mAdjectiveWords.Remove(w);
        }
    }

    protected void TickVerbWords(float deltaTime)
    {
        foreach (var w in mVerbWords)
        {
            if (w.mPower >= w.mData.mTriggerPower)
            {
                foreach (var abi in w.mAbilities)
                {
                    abi.TryActivate();
                }
                
                w.mPower = 0;
            }
            
            w.mUpdateTimer -= deltaTime;
            if (w.mUpdateTimer <= 0)
            {
                w.mUpdateTimer += BattleConfig.mData.word.verbPowerInterval;
                
                ++w.mPower;
            }
        }
    }
    public void ReduceSingleVerbPower(int reduceValue)
    {
        var maxPower = 0;
        WordEntry wordVerb = null;
        foreach (var w in mVerbWords)
        {
            if(w.mPower > maxPower)
            {
                maxPower = w.mPower;
                wordVerb = w;
            }
        }
        wordVerb.mData.mTriggerPower = Mathf.Max(wordVerb.mData.mTriggerPower - reduceValue, 2);
    }
    public void ReduceAllVerbPower(int reduceValue)
    {
        foreach (var w in mVerbWords)
        {
            w.mData.mTriggerPower = Mathf.Max(w.mData.mTriggerPower - reduceValue, 2);
        }
    }
}