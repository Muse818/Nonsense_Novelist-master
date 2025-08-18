
using System.Collections.Generic;


public class AbilityTable : MapTable<int, AbilityTable.Data>
{
    public class Data
    {
        public int mKind;
        public AbilityType mType;
        public string mName;
        public string mDesc;

        public bool mForbidden;
        
        public int mTriggerKind;
        public int mSelectorKind;
        public List<int> mEffectApplierList;
        
        public string mAnimName;
        public int mProjKind;

        public float mCoolDown;
        public int mMaxStackCount;

        public Dictionary<string, CustomParam> mCustomParams = new Dictionary<string, CustomParam>();
    }

    public override string AssetName => "AbilityData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = (AbilityType)reader.Read<int>();
        data.mName = reader.Read<string>();
        data.mDesc = reader.Read<string>();
        
        data.mForbidden = reader.Read<bool>();
        
        // data.mMaxStackCount = reader.Read<int>();
        
        data.mTriggerKind = reader.Read<int>();
        data.mSelectorKind = reader.Read<int>();
        data.mEffectApplierList = reader.ReadVec<int>();
        
        data.mAnimName = reader.Read<string>();
        data.mProjKind = reader.Read<int>();
        
        data.mCoolDown = reader.Read<float>();
        
        if (!ReaderUtils.ParseCustomParams(reader, data.mCustomParams))
            return default;
        
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }

}
