
using System.Collections.Generic;


public class LevelTable : MapTable<int, LevelTable.Data>
{
    public class Data
    {
        public int mKind;
        public float mHpLoss;
        public int mEventCount;
        public float mCardChance0;
        public float mCardChance1;
        public float mCardChance2;
        public float mCardChance3;
        public int mMonsterCount1;
        public float mMonsterChance1;
        public int mMonsterCount2;
        public float mMonsterChance2;
        public float mEliteChance;
        public int mEliteLimitCount;
        public int mProMonsterCount;
        public int mProEliteCount;
    }

    public override string AssetName => "LevelData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mHpLoss= reader.Read<float>();
        data.mEventCount= reader.Read<int>();
        data.mCardChance0= reader.Read<float>();
        data.mCardChance1= reader.Read<float>();
        data.mCardChance2= reader.Read<float>();
        data.mCardChance3= reader.Read<float>();
        data.mMonsterCount1= reader.Read<int>();
        data.mMonsterChance1= reader.Read<float>();
        data.mMonsterCount2= reader.Read<int>();
        data.mMonsterChance2= reader.Read<float>();
        data.mEliteChance= reader.Read<float>();
        data.mEliteLimitCount= reader.Read<int>();
        data.mProMonsterCount= reader.Read<int>();
        data.mProEliteCount= reader.Read<int>();

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}