

using System.Collections.Generic;
using System.Diagnostics;

public class AbilityTemplateTable : MapTable<int, AbilityTemplateTable.Data>
{
    public class Data
    {
        public class ModuleData
        {
            public int mType;
            public List<float> mParams = new List<float>();
        }

        public int mKind;
        public ModuleData mTriggerData = new ModuleData();
        public ModuleData mSelectorData = new ModuleData();
        public List<ModuleData> mEffectApplyDataList = new List<Data.ModuleData>();
    }

    public override string AssetName => "AbilityTemplateData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        
        data.mTriggerData.mType = reader.Read<int>();
        data.mTriggerData.mParams.AddRange(reader.ReadVec<float>());
        
        data.mSelectorData.mType = reader.Read<int>();
        data.mSelectorData.mParams.AddRange(reader.ReadVec<float>());
        
        int effectNum = reader.Read<int>();
        if (effectNum == 0)
        {
            reader.MarkReadInvalid();
            return default;
        }
        
        for (int i = 0; i < effectNum; i++)
        {
            Data.ModuleData moduleData = new Data.ModuleData();
            moduleData.mType = reader.Read<int>();
            moduleData.mParams.AddRange(reader.ReadVec<float>());
            
            data.mEffectApplyDataList.Add(moduleData);
        }
        

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}

public class AbilityTriggerTable : MapTable<int, AbilityTriggerTable.Data>
{
    public class Data
    {
        public int mKind;
        public int mType;
        public string mDesc;
        public int mMaxTriggerTimes;
        public float mPossibility;
        public float mCoolDownDuration;
        public Dictionary<string, CustomParam> mCustomParams = new Dictionary<string, CustomParam>();
    }
    
    public override string AssetName => "AbilityTriggerData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = reader.Read<int>();
        data.mDesc = reader.Read<string>();
        data.mMaxTriggerTimes = reader.Read<int>();
        data.mPossibility = reader.Read<float>();
        data.mCoolDownDuration = reader.Read<float>();
        
        if (!ReaderUtils.ParseCustomParams(reader, data.mCustomParams))
            return default;
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}

public class AbilitySelectorTable : MapTable<int, AbilitySelectorTable.Data>
{
    public class Data
    {
        public int mKind;
        public int mType;
        public string mDesc;
        public int mTargetCount;
        public Dictionary<string, CustomParam> mCustomParams = new Dictionary<string, CustomParam>();
    }
    
    public override string AssetName => "AbilitySelectorData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = reader.Read<int>();
        data.mDesc = reader.Read<string>();
        data.mTargetCount = reader.Read<int>();
        
        if (!ReaderUtils.ParseCustomParams(reader, data.mCustomParams))
            return default;
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}

public class AbilityEffectApplierTable : MapTable<int, AbilityEffectApplierTable.Data>
{
    public class Data
    {
        public int mKind;
        public int mType;
        public string mDesc;
        public bool mCanBePurgedOrExpelled;
        public int mStackLimit;
        public float mDuration;
        public Dictionary<string, CustomParam> mCustomParams = new Dictionary<string, CustomParam>();
    }
    
    public override string AssetName => "AbilityEffectApplierData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = reader.Read<int>();
        data.mDesc = reader.Read<string>();
        data.mCanBePurgedOrExpelled = reader.Read<bool>();
        data.mStackLimit = reader.Read<int>();
        data.mDuration = reader.Read<float>();
        
        if (!ReaderUtils.ParseCustomParams(reader, data.mCustomParams))
            return default;
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}