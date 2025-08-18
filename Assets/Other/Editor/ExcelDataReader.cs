#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Reflection;
using System;

//Excel中间数据
public class ExcelMediumData
{
    //Excel名字
    public string excelName;
    //Dictionary<字段名称, 字段类型>，记录类的所有字段及其类型
    public Dictionary<string, string> propertyNameTypeDic;
    //List<一行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
    //记录类的所有字段值，按行记录
    public List<Dictionary<string, string>> allItemValueRowList;
}

public static class ExcelDataReader
{
    //Excel第2行对应字段名称
    const int excelNameRow = 2;
    //Excel第4行对应字段类型
    const int excelTypeRow = 4;
    //Excel第5行及以后对应字段值
    const int excelDataRow = 5;

    //Excel读取路径
    public static string excelFilePath = Application.dataPath + "/Other/Excel";
    //public static string excelFilePath = Application.dataPath.Replace("Assets/Excel", "Excel");

    //自动生成C#类文件路径
    static string excelCodePath = Application.dataPath + "/Scripts/Excel/AutoCreateCSCode";
    //自动生成Asset文件路径
    static string excelAssetPath = "Assets/Resources/ExcelAsset";

    #region --- Read Excel ---

    //创建Excel对应的C#类
    public static void ReadAllExcelToCode()
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        string[] excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");

        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //遍历所有Excel，创建C#类
        for (int i = 0; i < excelFileFullPaths.Length; i++)
        {
            ReadOneExcelToCode(excelFileFullPaths[i]);
        }
    }

    //创建Excel对应的C#类
    public static void ReadOneExcelToCode(string excelFileFullPath)
    {
        //解析Excel获取中间数据
        ExcelMediumData excelMediumData = CreateClassCodeByExcelPath(excelFileFullPath);
        if (excelMediumData != null)
        {
            //根据数据生成C#脚本
            string classCodeStr = ExcelCodeCreater.CreateCodeStrByExcelData(excelMediumData);
            if (!string.IsNullOrEmpty(classCodeStr))
            {
                //写文件，生成CSharp.cs
                if (WriteCodeStrToSave(excelCodePath, excelMediumData.excelName + "ExcelData", classCodeStr))
                {
                    Debug.Log("<color=green>Auto Create Excel Scripts Success : </color>" + excelMediumData.excelName);
                    return;
                }
            }
        }
        //生成失败
        Debug.LogError("Auto Create Excel Scripts Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
    }

    #endregion

    #region --- Create Asset ---

    //创建Excel对应的Asset数据文件
    public static void CreateAllExcelAsset()
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        string[] excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");
        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //遍历所有Excel，创建Asset
        for (int i = 0; i < excelFileFullPaths.Length; i++)
        {
            CreateOneExcelAsset(excelFileFullPaths[i]);
        }
    }

    //创建Excel对应的Asset数据文件
    public static void CreateOneExcelAsset(string excelFileFullPath)
    {
        //解析Excel获取中间数据
        ExcelMediumData excelMediumData = CreateClassCodeByExcelPath(excelFileFullPath);
        if (excelMediumData != null)
        {
            //获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
           // 创建类的实例，返回为 object 类型，需要强制类型转换，assembly.CreateInstance("类的完全限定名（即包括命名空间）");
            object class0bj = assembly.CreateInstance(excelMediumData.excelName + "Assignment",true);

            //必须遍历所有程序集来获得类型。当前在Assembly-CSharp-Editor中，目标类型在Assembly-CSharp中，不同程序将无法获取类型
            Type type = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                //查找目标类型
                Type tempType = asm.GetType(excelMediumData.excelName + "AssetAssignment");
                if (tempType != null)
                {
                    type = tempType;
                    break;
                }
            }
            if (type != null)
            {
                //反射获取方法
                MethodInfo methodInfo = type.GetMethod("CreateAsset");
                if (methodInfo != null)
                {
                    methodInfo.Invoke(null, new object[] { excelMediumData.allItemValueRowList, excelAssetPath });
                    //创建Asset文件成功
                    Debug.Log("<color=green>Auto Create Excel Asset Success : </color>" + excelMediumData.excelName);
                    return;
                }
            }
        }
        //创建Asset文件失败
        Debug.LogError("Auto Create Excel Asset Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
    }

    #endregion

    #region --- private ---

    //解析Excel，创建中间数据
    private static ExcelMediumData CreateClassCodeByExcelPath(string excelFileFullPath)
    {
        if (string.IsNullOrEmpty(excelFileFullPath))
            return null;

        excelFileFullPath = excelFileFullPath.Replace("\\", "/");

        FileStream stream = File.Open(excelFileFullPath, FileMode.Open, FileAccess.Read);
        if (stream == null)
            return null;
        //解析Excel
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //无效Excel
        if (excelReader == null || !excelReader.IsValid)
        {
            Debug.Log("Invalid excel ： " + excelFileFullPath);
            return null;
        }

        //<数据名称,数据类型>
        KeyValuePair<string, string>[] propertyNameTypes = null;
        //List<KeyValuePair<数据名称, 单元格数据值>[]>，所有数据值，按行记录
        List<Dictionary<string, string>> allItemValueRowList = new List<Dictionary<string, string>>();

        //每行数据数量
        int propertyCount = 0;
        //当前遍历行，从1开始
        int curRowIndex = 1;
        //开始读取，按行遍历
        while (excelReader.Read())
        {
            if (excelReader.FieldCount == 0)
                continue;
            //读取一行的数据
            string[] datas = new string[excelReader.FieldCount];
            for (int j = 0; j < excelReader.FieldCount; ++j)
            {
                //赋值一行的每一个单元格数据
                datas[j] = excelReader.GetString(j);
            }
            //空行/行第一个单元格为空，视为无效数据
            if (datas.Length == 0 || string.IsNullOrEmpty(datas[0]))
            {
                curRowIndex++;
                continue;
            }
            //数据行
            if (curRowIndex >= excelDataRow)
            {
                //数据无效
                if (propertyCount <= 0)
                    return null;

                Dictionary<string, string> itemDic = new Dictionary<string, string>(propertyCount);
                //遍历一行里的每个单元格数据
                for (int j = 0; j < propertyCount; j++)
                {
                    //判断长度
                    if (j < datas.Length)
                        itemDic[propertyNameTypes[j].Key] = datas[j];
                    else
                        itemDic[propertyNameTypes[j].Key] = null;
                }
                allItemValueRowList.Add(itemDic);
            }
            //数据名称行
            else if (curRowIndex == excelNameRow)
            {
                //以数据名称确定每行的数据数量
                propertyCount = datas.Length;
                if (propertyCount <= 0)
                    return null;
                //记录数据名称
                propertyNameTypes = new KeyValuePair<string, string>[propertyCount];
                for (int i = 0; i < propertyCount; i++)
                {
                    propertyNameTypes[i] = new KeyValuePair<string, string>(datas[i], null);
                }
            }
            //数据类型行
            else if (curRowIndex == excelTypeRow)
            {
                //数据类型数量少于指定数量，数据无效
                if (propertyCount <= 0 || datas.Length < propertyCount)
                    return null;
                //记录数据名称及类型
                for (int i = 0; i < propertyCount; i++)
                {
                    propertyNameTypes[i] = new KeyValuePair<string, string>(propertyNameTypes[i].Key, datas[i]);
                }
            }
            curRowIndex++;
        }

        if (propertyNameTypes.Length == 0 || allItemValueRowList.Count == 0)
            return null;

        ExcelMediumData excelMediumData = new ExcelMediumData();
        //类名
        excelMediumData.excelName = excelReader.Name;
        //Dictionary<数据名称,数据类型>
        excelMediumData.propertyNameTypeDic = new Dictionary<string, string>();
        //转换存储格式
        for (int i = 0; i < propertyCount; i++)
        {
            //数据名重复，数据无效
            if (excelMediumData.propertyNameTypeDic.ContainsKey(propertyNameTypes[i].Key))
                return null;
            excelMediumData.propertyNameTypeDic.Add(propertyNameTypes[i].Key, propertyNameTypes[i].Value);
        }
        excelMediumData.allItemValueRowList = allItemValueRowList;
        return excelMediumData;
    }

    //写文件
    private static bool WriteCodeStrToSave(string writeFilePath, string codeFileName, string classCodeStr)
    {
        if (string.IsNullOrEmpty(codeFileName) || string.IsNullOrEmpty(classCodeStr))
            return false;
        //检查导出路径
        if (!Directory.Exists(writeFilePath))
            Directory.CreateDirectory(writeFilePath);
        //写文件，生成CS类文件
        StreamWriter sw = new StreamWriter(writeFilePath + "/" + codeFileName + ".cs");
        sw.WriteLine(classCodeStr);
        sw.Close();
        //
        UnityEditor.AssetDatabase.Refresh();
        return true;
    }

    #endregion

}
#endif