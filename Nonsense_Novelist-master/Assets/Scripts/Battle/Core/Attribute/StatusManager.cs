using System;

[Flags]
public enum Status
{
    None = 0,
    Stun = 0x1,
    BlockPositive = 0x2,
    BlockNegative = 0x4,
}

public class StatusManager
{
    protected Status mStatus = Status.None;
    public Status Status => mStatus;
    
    protected Status mStatusMod = Status.None;
    
    public bool InStatus(Status status)
    {
        return (mStatus & status) == status;
    }

    public void AddStatus(Status status)
    {
        mStatusMod |= status;
    }

    public void ApplyMod()
    {
        mStatus = mStatusMod;
        mStatusMod = 0;
    }
}