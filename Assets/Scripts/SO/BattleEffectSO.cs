using System.Collections.Generic;
using UnityEngine;

public enum EffectFxPlayType
{
    None = 0,
    Instant,
    Script,
}

[System.Serializable]
public class EffectFxData
{
    [Tooltip("播放类型")]
    public EffectFxPlayType mPlayType = EffectFxPlayType.None;
    
    [Tooltip("描述")]
    public string mDesc;
    
    [Tooltip("插槽")]
    public string mSocketName;

    [Tooltip("特效")]
    public GameObject mFxObj;
}

[System.Serializable]
public class EffectFxList
{
    [Tooltip("Buff类型")]
    public EffectType mType = EffectType.None;
    
    [Tooltip("特效列表")]
    public List<EffectFxData> mFxList;
}


[CreateAssetMenu(fileName = "NewBattleEffectSO", menuName = "BattleSO/BattleEffectSO")]
public class BattleEffectSO : ScriptableObject
{
    public List<EffectFxList> mConfigs;
    
    public EffectFxList GetFxList(EffectType type)
    {
        foreach (var it in mConfigs)
        {
            if (it.mType == type)
            {
                return it;
            }
        }

        return null;
    }
}