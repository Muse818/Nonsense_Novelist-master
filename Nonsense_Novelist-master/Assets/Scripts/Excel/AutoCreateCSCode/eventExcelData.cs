/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class eventExcelItem : ExcelItemBase
{
	public EventType type;
	public string name;
	public string textEvent;
	public string textDraft;
	public string textEfc;
	public string textTrigger;
	public bool isKey;
	public string happen;
}

[CreateAssetMenu(fileName = "eventExcelData", menuName = "Excel To ScriptableObject/Create eventExcelData", order = 1)]
public class eventExcelData : ExcelDataBase<eventExcelItem>
{
}

#if UNITY_EDITOR
public class eventAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		eventExcelItem[] items = new eventExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new eventExcelItem();
			items[i].id = allItemValueRowList[i]["id"];
			items[i].type = (EventType)(Convert.ToInt32(allItemValueRowList[i]["type"]));
			items[i].name = allItemValueRowList[i]["name"];
			items[i].textEvent = allItemValueRowList[i]["textEvent"];
			items[i].textDraft = allItemValueRowList[i]["textDraft"];
			items[i].textEfc = allItemValueRowList[i]["textEfc"];
			items[i].textTrigger = allItemValueRowList[i]["textTrigger"];
			items[i].isKey = Convert.ToBoolean(allItemValueRowList[i]["isKey"]);
			items[i].happen = allItemValueRowList[i]["happen"];
		}
		eventExcelData excelDataAsset = ScriptableObject.CreateInstance<eventExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(eventExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


