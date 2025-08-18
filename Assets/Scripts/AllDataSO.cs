using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AllDataSo", menuName = "AllDataSO")]
public class AllDataSO : ScriptableObject
{
    public eventExcelData data;
    public MonsterExcelData monsterDate;/* AssetDatabase.LoadAssetAtPath<MonsterExcelData>(@"Assets/Resources/ExcelAsset/MonsterExcelData.asset");*/
    public cardRareExcelData cardRareDate;
    public CharaInfoExcelData charaInfo;
    
}

