
using System.Collections.Generic;

public enum EmitType
{
    None = 0,
    SelfToEnemy,
    EnemyToSelf,
}

public class EmitTable : MapTable<int, EmitTable.Data>
{
    public class Data
    {
        public int mKind;
        public EmitType mType;
        public string mName;
        public string mAsset;
        public float mSpeed;
    }

    public override string AssetName => "EmitData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mAsset = reader.Read<string>();
        data.mType = (EmitType)reader.Read<int>();
        data.mName = reader.Read<string>();
        data.mSpeed = reader.Read<float>();
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}