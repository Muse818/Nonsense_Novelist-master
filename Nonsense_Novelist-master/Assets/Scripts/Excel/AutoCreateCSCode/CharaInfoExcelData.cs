/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class CharaInfoExcelItem : ExcelItemBase
{
	public int charaID;
	public string name;
	public string typeName;
	public BookNameEnum book;
	public float maxhp;
	public float atk;
	public float def;
	public float psy;
	public float san;
	public string roleName;
	public string roleInfo;
	public string bg;
}

[CreateAssetMenu(fileName = "CharaInfoExcelData", menuName = "Excel To ScriptableObject/Create CharaInfoExcelData", order = 1)]
public class CharaInfoExcelData : ExcelDataBase<CharaInfoExcelItem>
{
}

#if UNITY_EDITOR
public class CharaInfoAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		CharaInfoExcelItem[] items = new CharaInfoExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new CharaInfoExcelItem();
			items[i].charaID = Convert.ToInt32(allItemValueRowList[i]["charaID"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].typeName = allItemValueRowList[i]["typeName"];
			items[i].book = (BookNameEnum)(Convert.ToInt32(allItemValueRowList[i]["book"]));
			items[i].maxhp = Convert.ToSingle(allItemValueRowList[i]["maxhp"]);
			items[i].atk = Convert.ToSingle(allItemValueRowList[i]["atk"]);
			items[i].def = Convert.ToSingle(allItemValueRowList[i]["def"]);
			items[i].psy = Convert.ToSingle(allItemValueRowList[i]["psy"]);
			items[i].san = Convert.ToSingle(allItemValueRowList[i]["san"]);
			items[i].roleName = allItemValueRowList[i]["roleName"];
			items[i].roleInfo = allItemValueRowList[i]["roleInfo"];
			items[i].bg = allItemValueRowList[i]["bg"];
		}
		CharaInfoExcelData excelDataAsset = ScriptableObject.CreateInstance<CharaInfoExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(CharaInfoExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


