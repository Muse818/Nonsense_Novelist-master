using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleUnitSO", menuName = "BattleSO/BattleUnitData")]
public class BattleUnitSO : ScriptableObject
{
    [Header("基础信息信息")][Space(5)]

    [Tooltip("角色正式名称")]
    public string unitName;

    [Tooltip("角色预制体")]
    public GameObject prefab;
    
    [Tooltip("UI展示预制体")]
    public GameObject uiPrefab;

    [Tooltip("角色缩略描述")][TextArea]
    public string infoShort;

    [Tooltip("角色详细描述")][TextArea]
    public string infoDetail;

    [Tooltip("角色立绘")]
    public Texture2D pic;
    
    [Tooltip("场景角色")]
    public Texture2D sprite;

    [Tooltip("角色动画")]
    public AnimatorController animatorController;

    [Tooltip("武器插槽名")] 
    public string weaponSocket = "weaponMuzzle";

    [Space(20)][Header("角色特性")][Space(5)]

    [Tooltip("角色特性名称")]
    public string roleName;

    [Tooltip("角色特性描述")][TextArea]
    public string roleInfo;


}
