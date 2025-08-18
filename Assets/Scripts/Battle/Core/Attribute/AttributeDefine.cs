


public enum AttributeType
{
    None = 0,
    MaxHp,
    Def,
    Psy,
    San,
    RecoverHp,
    Attack,
    AttackSpeed,
    VerbDamageCoefficient,
    VerbDamageMod,
    EffectDamageCoefficient,
    NormalAttackDamage,
    DebuffUp,
    HealUp,
    TakeHealUp,
    SuckBlood,//吸血
    Soc,//社会，五维之一，随从没有，用于提升墙体效果的属性，具体见墙体属性
    Pet,//五维之一，随从没有，用于提升随从的属性，具体见随从属性
    Mag,//Magic Resistance魔抗
    Sdu,
    Luc,
    TauntLevel,
    NounSlotNum,
    VerbSlotNum,
    ServantSlotNum,
    ServantAttrBouns,
    ServantAttackSpeed,
    VerbMaxPower,
    PowerRecoverSpeed,
    SingleMaxPowerDown,
    AllMaxPowerDown,
}
public enum ComparisonOperator
{
    LessThan,
    GreaterThan,
    LessOrEqual,
    Equal,
    GreaterOrEqual,
}

public class Attribute
{
    public AttributeType mType;
    public float mOriginValue;
    public float mBaseValue;
    public float mValue;
    public float mMod;
    public float mPercentMod;
}