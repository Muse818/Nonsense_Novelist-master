namespace AI
{
    /// <summary>
    /// 触发器枚举
    /// </summary>
    public enum TriggerID
    {
        /// <summary>开始走路</summary>
        IdleToAttack,
        /// <summary>离开攻击范围</summary>
        OutAttack,
        /// <summary>生命值为0</summary>
        NoHealth,
        /// <summary>消灭对方</summary>
        KilledAim,
        /// <summary>被晕</summary>
        BeDizzy,
        /// <summary>解除眩晕状态</summary>
        OutDizzy,
        /// <summary>复活</summary>
        ReLife,
    }
}
