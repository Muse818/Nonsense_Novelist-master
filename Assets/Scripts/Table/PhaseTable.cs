using System.Collections.Generic;


public class PhaseTable : MapTable<int, PhaseTable.Data>
{
    public class Data
    {
        public int mKind;
        public int mChapter;
        public BattlePhaseType mType;
        public int mLevel;
        public float mWeight;
        public float mConstant;
    }

    public override string AssetName => "PhaseData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mChapter = reader.Read<int>();
        data.mType = (BattlePhaseType)reader.Read<int>();
        data.mLevel = reader.Read<int>();
        data.mWeight = reader.Read<float>();
        data.mConstant = reader.Read<float>();

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}