using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewCardSO", menuName = "BattleSO/CardData")]
public class CardSO : ScriptableObject
{
    [Header("基础信息信息")][Space(5)]
    
    [Tooltip("卡牌正式名称")]
    public string wordName;
    
    [Tooltip("卡牌描述")][TextArea]
    public string desc;
    
    [FormerlySerializedAs("sprite")] [Tooltip("卡牌图像")]
    public Texture2D mainIcon;
    
    [Tooltip("卡牌背景")]
    public Texture2D titleIcon;



    [Space(20)][Header("关联词条")][Space(5)]

    [Tooltip("关联卡牌ID")]
    public List<int> relativeCard;

    [Tooltip("关联技能")]
    public List<CardSO> relativeSkills;

}
