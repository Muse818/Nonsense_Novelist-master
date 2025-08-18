

using System.Collections.Generic;
public enum WordType
{
    Undefined = 0,
    Verb,                  //动词
    Adjective,             //形容词
    Noun,                  //名词
    Property,              //特性
}

public enum WordTag
{
    Undefined = 0,
}

public class WordTable : MapTable<int, WordTable.Data>
{
    public class Data
    {
        public int mKind;
        public WordType mType;
        public string mAssetName;
        public string mName;
        public bool mForbidden;
        public BookNameEnum mBook;
        public ShootType mShootType;
        public float mDuration;
        public int mInitPower;
        public int mTriggerPower;
        public List<int> mTags;
        public List<int> mAbilities;
    }

    public override string AssetName => "WordData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mType = (WordType)reader.Read<int>();
        data.mAssetName = reader.Read<string>();
        data.mName = reader.Read<string>();
        data.mForbidden = reader.Read<bool>();
        data.mBook = (BookNameEnum)reader.Read<int>();
        data.mShootType = (ShootType)reader.Read<int>();
        data.mDuration = reader.Read<float>();
        data.mInitPower = reader.Read<int>();
        data.mTriggerPower = reader.Read<int>();
        data.mTags = reader.ReadVec<int>();
        data.mAbilities = reader.ReadVec<int>();

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}