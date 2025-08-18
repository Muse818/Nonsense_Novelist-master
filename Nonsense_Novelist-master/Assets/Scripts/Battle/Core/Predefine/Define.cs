
using System;

public enum BattleState
{
    None = 0,
    Inprogress,
    End,
}

[Flags]
public enum BattleCamp : int
{
    None = 0,
    Neutral = 1 << 0,
    Camp1 = 1 << 1,
    Camp2 = 1 << 2,
    Camp3 = 1 << 3,
    Camp4 = 1 << 4,
    Camp5 = 1 << 5,
    Camp6 = 1 << 6,
    Camp7 = 1 << 7,
    Camp8 = 1 << 8,
    Boss = 1 << 7,
}

[Flags]
public enum BattleRelation : int
{
    None = 0,
    Self = 1 << 0,
    Ally = 1 << 1,
    Enemy = 1 << 2,
}

public enum DamageSource
{
    None = 0,
    AutoAttack,
    Ability,
}

public enum DamageType
{
    None = 0,
    Fix,
    Psy,
    Magic,
}

[Flags]
public enum DealDamageFlag
{
    None = 0,
    Fixed = 1,
    AlwaysHit = 2,
    AlwaysCritical = 4,
}

[Flags]
public enum TakeDamageFlag
{
    Dodged = 1,
    Critical = 2,
    Blocked = 4,
    Immune = 8,
}