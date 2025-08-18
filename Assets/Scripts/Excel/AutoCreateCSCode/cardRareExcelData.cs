/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class cardRareExcelItem : ExcelItemBase
{
	public int stage;
	public float rate1;
	public float rate2;
	public float rate3;
	public float rate4;
	public float sum;
}

[CreateAssetMenu(fileName = "cardRareExcelData", menuName = "Excel To ScriptableObject/Create cardRareExcelData", order = 1)]
public class cardRareExcelData : ExcelDataBase<cardRareExcelItem>
{
}

#if UNITY_EDITOR
public class cardRareAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		cardRareExcelItem[] items = new cardRareExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new cardRareExcelItem();
			items[i].stage = Convert.ToInt32(allItemValueRowList[i]["stage"]);
			items[i].rate1 = Convert.ToSingle(allItemValueRowList[i]["rate1"]);
			items[i].rate2 = Convert.ToSingle(allItemValueRowList[i]["rate2"]);
			items[i].rate3 = Convert.ToSingle(allItemValueRowList[i]["rate3"]);
			items[i].rate4 = Convert.ToSingle(allItemValueRowList[i]["rate4"]);
			items[i].sum = Convert.ToSingle(allItemValueRowList[i]["sum"]);
		}
		cardRareExcelData excelDataAsset = ScriptableObject.CreateInstance<cardRareExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(cardRareExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


