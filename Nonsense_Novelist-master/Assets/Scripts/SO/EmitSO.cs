using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEmitSO", menuName = "BattleSO/EmitData")]
public class EmitSO : ScriptableObject
{
    [Header("基础信息信息")][Space(5)]

    [Tooltip("正式名称")]
    public string projName;

    [Tooltip("预制体")]
    public GameObject projObject;
    
    [Tooltip("发射者特效")]
    public GameObject emitterFx;
    
    [Tooltip("到达者特效")]
    public GameObject arriverFx;
}