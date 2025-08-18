using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllData :MonoSingleton<AllData>
{
    public eventExcelData data;
    public MonsterExcelData monsterDate;/* AssetDatabase.LoadAssetAtPath<MonsterExcelData>(@"Assets/Resources/ExcelAsset/MonsterExcelData.asset");*/
    public cardRareExcelData cardRareDate;
    public CharaInfoExcelData charaInfo ;

    override public void Awake()
    {
        base.Awake();

        var so = ResMgr.GetInstance().Load<AllDataSO>("AllDataSO");
        if (data == null) data = so.data;
        if (monsterDate == null) monsterDate = so.monsterDate;
        if (cardRareDate == null) cardRareDate = so.cardRareDate;
        if (charaInfo == null) charaInfo = so.charaInfo;

    }


}
