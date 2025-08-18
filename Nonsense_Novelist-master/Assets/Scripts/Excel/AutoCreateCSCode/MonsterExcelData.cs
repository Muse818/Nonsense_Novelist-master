/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class MonsterExcelItem : ExcelItemBase
{
	public string type;
	public int name;
	public float hp;
	public float atk;
	public float def;
	public float psy;
	public float san;
	public int Mid;
	public string word1;
	public string word2;
	public string word3;
	public string word4;
}

[CreateAssetMenu(fileName = "MonsterExcelData", menuName = "Excel To ScriptableObject/Create MonsterExcelData", order = 1)]
public class MonsterExcelData : ExcelDataBase<MonsterExcelItem>
{
}

#if UNITY_EDITOR
public class MonsterAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		MonsterExcelItem[] items = new MonsterExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new MonsterExcelItem();
			items[i].type = allItemValueRowList[i]["type"];
			items[i].name = Convert.ToInt32(allItemValueRowList[i]["name"]);
			items[i].hp = Convert.ToSingle(allItemValueRowList[i]["hp"]);
			items[i].atk = Convert.ToSingle(allItemValueRowList[i]["atk"]);
			items[i].def = Convert.ToSingle(allItemValueRowList[i]["def"]);
			items[i].psy = Convert.ToSingle(allItemValueRowList[i]["psy"]);
			items[i].san = Convert.ToSingle(allItemValueRowList[i]["san"]);
			items[i].Mid = Convert.ToInt32(allItemValueRowList[i]["Mid"]);
			items[i].word1 = allItemValueRowList[i]["word1"];
			items[i].word2 = allItemValueRowList[i]["word2"];
			items[i].word3 = allItemValueRowList[i]["word3"];
			items[i].word4 = allItemValueRowList[i]["word4"];
		}
		MonsterExcelData excelDataAsset = ScriptableObject.CreateInstance<MonsterExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(MonsterExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


