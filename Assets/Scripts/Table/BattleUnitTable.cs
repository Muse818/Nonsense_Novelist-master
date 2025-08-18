
using System.Collections.Generic;

public class BattleUnitTable : MapTable<int, BattleUnitTable.Data>
{
    public class Data
    {
        public int mKind;
        public BattleUnitType mInitType;
        public string mAsset;
        public string mName;
        public bool mForbidden;
        public BookNameEnum mBook;
        public int mLevel;
        public float mAttack;
        public float mMaxHp;
        public float mDefense;
        public float mPsy;
        public float mSan;
        public float mSoc;
        public float mPet;
        public float mMag;
        public float mSdu;
        public float mLuc;
        public float mTauntLevel;
        public float mRecoverHp;
        public float mVerbDamageCoefficient;
        public float mVerbDamageMod;
        public float mEffectDamageCoefficient;
        public float mNormalAttackDamageCoefficient;
        public float mSuckBloodCoefficient;
        public float mDebuffDurationUp;
        public float mHealUp;
        public float mTakeHealUp;
        public float mServantAttrBouns;
        public float mServantAttackSpeed;
        public float mPowerRecoverSpeed;
        public float mSingleMaxPowerDown;
        public float mAllMaxPowerDown;
        public List<int> mTalents = new List<int>();
        public int mRoles;
        public List<int> mInitServants = new List<int>();
        public Dictionary<int, List<int>> mTrees = new Dictionary<int, List<int>>();
    }

    public override string AssetName => "BattleUnitData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mInitType = (BattleUnitType)reader.Read<int>();
        data.mAsset = reader.Read<string>();
        data.mName = reader.Read<string>();
        data.mForbidden = reader.Read<bool>();
        data.mBook = (BookNameEnum)reader.Read<int>();
        data.mLevel = reader.Read<int>();
        data.mAttack = reader.Read<float>();
        data.mMaxHp = reader.Read<float>();
        data.mDefense = reader.Read<float>();
        data.mPsy = reader.Read<float>();
        data.mSan = reader.Read<float>();
        data.mSoc = reader.Read<float>();
        data.mPet = reader.Read<float>();
        data.mMag = reader.Read<float>();
        data.mSdu = reader.Read<float>();
        data.mLuc = reader.Read<float>();
        data.mTauntLevel = reader.Read<float>();
        data.mRecoverHp = reader.Read<float>();
        data.mVerbDamageCoefficient = reader.Read<float>();
        data.mVerbDamageMod= reader.Read<float>();
        data.mEffectDamageCoefficient = reader.Read<float>();
        data.mNormalAttackDamageCoefficient = reader.Read<float>();
        data.mSuckBloodCoefficient = reader.Read<float>();
        data.mDebuffDurationUp = reader.Read<float>();
        data.mHealUp=reader.Read<float>();
        data.mTakeHealUp=reader.Read<float>();
        data.mServantAttrBouns= reader.Read<float>();
        data.mServantAttackSpeed = reader.Read<float>();
        data.mPowerRecoverSpeed = reader.Read<float>();
        data.mSingleMaxPowerDown = reader.Read<float>();
        data.mAllMaxPowerDown= reader.Read<float>();
        data.mTalents.AddRange(reader.ReadVec<int>());
        data.mRoles = reader.Read<int>();
        data.mInitServants.AddRange(reader.ReadVec<int>());
        for (int _temp = 0; _temp < 6; _temp++)
        {
            data.mTrees.TryAdd(reader.Read<int>(), reader.ReadVec<int>());
        }



        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}