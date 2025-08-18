


public enum AttributeType
{
    None = 0,
    MaxHp,
    Attack,
    AttackSpeed,
    Def,
    Psy,
    San,
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