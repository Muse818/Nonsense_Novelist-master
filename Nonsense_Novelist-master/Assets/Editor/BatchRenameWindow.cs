using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;

public class BatchRenameWindow : EditorWindow
{
    // 目标区域选择
    private enum TargetArea { Hierarchy, Project }
    private TargetArea targetArea = TargetArea.Hierarchy;

    // 基础名称设置
    private string baseName = "GameObject";
    // 新增：是否保留原来名称作为基础名称
    private bool preserveOriginalName = false;

    // 前缀设置
    private bool usePrefix = false;
    private string prefix = "Prefix_";
    private bool prefixWithNumber = false;
    private int prefixStartNumber = 1;
    private int prefixPadding = 1;
    // 新增：是否使用父级GameObject名称作为前缀
    private bool useParentNameAsPrefix = false;
    private bool parentNameWithNumber = false;
    private int parentNameStartNumber = 1;
    private int parentNamePadding = 1;

    // 后缀设置
    private bool useSuffix = false;
    private string suffix = "_Suffix";
    private bool suffixWithNumber = false;
    private int suffixStartNumber = 1;
    private int suffixPadding = 1;
    // 新增：后缀数字前是否添加下划线
    private bool addUnderscoreBeforeSuffixNumber = true;

    // 查找替换设置
    private bool useFindReplace = false;
    private string findText = "";
    private string replaceText = "";
    private bool caseSensitive = false;

    // 新增：Project模式相关设置
    private bool moveToFolder = false;
    private string targetFolderPath = "Assets/";

    // 新增：滚动视图支持 - 为每个滚动视图创建独立的变量
    private Vector2 mainWindowScrollPos;
    private Vector2 previewScrollPos;
    private Vector2 previewListScrollPos;
    
    // 新增：预览列表系统（匹配BatchRenamer.cs）
    private List<KeyValuePair<string, string>> previewList = new List<KeyValuePair<string, string>>();
    private bool previewGenerated = false;

    [MenuItem("Tools/批量重命名工具")]
    public static void ShowWindow()
    {
        BatchRenameWindow window = GetWindow<BatchRenameWindow>("批量重命名");
        window.minSize = new Vector2(500, 750); // 调整窗口高度，移除模板功能后减少高度
        
        // 注意：不需要在这里初始化滚动位置，Unity会自动处理
        // 滚动位置会在OnGUI中自动保存和恢复
    }

    private void OnGUI()
    {
        // 为整个窗口添加滚动视图
        mainWindowScrollPos = EditorGUILayout.BeginScrollView(mainWindowScrollPos);
        
        GUILayout.Label(" GameObject 批量重命名", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 目标区域选择
        EditorGUILayout.LabelField("目标区域选择", EditorStyles.label);
        targetArea = (TargetArea)EditorGUILayout.EnumPopup("选择目标区域", targetArea);
        
        // 添加模式说明
        if (targetArea == TargetArea.Hierarchy)
        {
            EditorGUILayout.HelpBox("Hierarchy模式：重命名场景中的GameObject", MessageType.Info);
        }
        else if (targetArea == TargetArea.Project)
        {
            EditorGUILayout.HelpBox("Project模式：重命名Project中的Asset文件", MessageType.Info);
        }
        
        EditorGUILayout.Space();

        // 基础名称设置区域
        EditorGUILayout.LabelField("基础名称设置", EditorStyles.label);
        preserveOriginalName = EditorGUILayout.Toggle("保留原来名称作为基础名称", preserveOriginalName);
        
        if (!preserveOriginalName)
        {
            baseName = EditorGUILayout.TextField("基础名称", baseName);
        }
        else
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("基础名称", "使用原名称");
            EditorGUI.EndDisabledGroup();
        }
        EditorGUILayout.Space();

        // 前缀设置
        usePrefix = EditorGUILayout.Toggle("使用前缀", usePrefix);

        if (usePrefix)
        {
            EditorGUI.indentLevel++;
            prefix = EditorGUILayout.TextField("前缀文本", prefix);

            prefixWithNumber = EditorGUILayout.Toggle("前缀包含数字", prefixWithNumber);
            if (prefixWithNumber)
            {
                EditorGUI.indentLevel++;
                prefixStartNumber = EditorGUILayout.IntField("起始数字", prefixStartNumber);
                prefixPadding = EditorGUILayout.IntField("数字位数补零", prefixPadding);
                prefixPadding = Mathf.Max(1, prefixPadding);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        // 新增：父级名称前缀设置
        useParentNameAsPrefix = EditorGUILayout.Toggle("使用父级名称作为前缀", useParentNameAsPrefix);
        
        if (useParentNameAsPrefix)
        {
            EditorGUI.indentLevel++;
            parentNameWithNumber = EditorGUILayout.Toggle("父级名称包含数字", parentNameWithNumber);
            if (parentNameWithNumber)
            {
                EditorGUI.indentLevel++;
                parentNameStartNumber = EditorGUILayout.IntField("父级名称起始数字", parentNameStartNumber);
                parentNamePadding = EditorGUILayout.IntField("父级名称数字位数补零", parentNamePadding);
                parentNamePadding = Mathf.Max(1, parentNamePadding);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        // 后缀设置
        useSuffix = EditorGUILayout.Toggle("使用后缀", useSuffix);

        if (useSuffix)
        {
            EditorGUI.indentLevel++;
            suffix = EditorGUILayout.TextField("后缀文本", suffix);

            suffixWithNumber = EditorGUILayout.Toggle("后缀包含数字", suffixWithNumber);
            if (suffixWithNumber)
            {
                EditorGUI.indentLevel++;
                suffixStartNumber = EditorGUILayout.IntField("起始数字", suffixStartNumber);
                suffixPadding = EditorGUILayout.IntField("数字位数补零", suffixPadding);
                suffixPadding = Mathf.Max(1, suffixPadding);
                // 新增：显示下划线选项
                addUnderscoreBeforeSuffixNumber = EditorGUILayout.Toggle("数字前添加下划线", addUnderscoreBeforeSuffixNumber);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        // 查找替换设置区域（独立）
        EditorGUILayout.LabelField("查找替换设置", EditorStyles.label);
        useFindReplace = EditorGUILayout.Toggle("启用查找替换", useFindReplace);

        if (useFindReplace)
        {
            EditorGUI.indentLevel++;
            findText = EditorGUILayout.TextField("查找内容", findText);
            replaceText = EditorGUILayout.TextField("替换为", replaceText);
            caseSensitive = EditorGUILayout.Toggle("区分大小写", caseSensitive);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        // 新增：Project模式设置
        if (targetArea == TargetArea.Project)
        {
            EditorGUILayout.LabelField("Project模式设置", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Project模式使用与Hierarchy模式相同的命名系统（前缀、基础名称、后缀等）", MessageType.Info);
            
            // 移动到文件夹选项
            moveToFolder = EditorGUILayout.Toggle("重命名后移动到文件夹", moveToFolder);
            if (moveToFolder)
            {
                EditorGUI.indentLevel++;
                targetFolderPath = EditorGUILayout.TextField("目标文件夹路径 (Assets/...):", targetFolderPath);
                if (!targetFolderPath.EndsWith("/"))
                {
                    targetFolderPath += "/";
                }
                if (!AssetDatabase.IsValidFolder(targetFolderPath))
                {
                    EditorGUILayout.HelpBox("目标文件夹不存在，将自动创建", MessageType.Info);
                }
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();
        }

        // 显示排序方式说明
        EditorGUILayout.HelpBox("将严格按照Hierarchy中的显示顺序排序，从上到下依次编号增大", MessageType.Info);
        EditorGUILayout.Space();

        // 新增：实时预览区域
        if (GetSelectedObjectsCount() > 0)
        {
            EditorGUILayout.LabelField("重命名预览", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("下方显示每个选中对象的重命名预览，按照Hierarchy顺序排列", MessageType.Info);
            EditorGUILayout.Space();
            
            // 创建预览区域的背景
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            // 获取排序后的对象
            var sortedObjects = GetSortedObjects();
            
            // 分析重命名结果，合并相同的新名称
            var previewGroups = AnalyzePreviewGroups(sortedObjects);
            
            // 使用滚动视图显示预览，确保显示滑条
            previewScrollPos = EditorGUILayout.BeginScrollView(previewScrollPos, false, false, GUILayout.Height(200));
            
            // 显示合并后的预览
            for (int i = 0; i < previewGroups.Count; i++)
            {
                var group = previewGroups[i];
                
                if (group.Objects.Count == 1)
                {
                    // 单个对象，正常显示
                    var obj = group.Objects[0];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"{i + 1}.", GUILayout.Width(25));
                    // 原始名称保持原来的方法和颜色
                    EditorGUILayout.LabelField(GetObjectDisplayName(obj), EditorStyles.miniLabel, GUILayout.Width(120));
                    EditorGUILayout.LabelField("→", GUILayout.Width(20));
                    
                    // 只有当新名称与原始名称不同时才用绿色显示
                    string originalName = GetObjectDisplayName(obj);
                    if (group.NewName != originalName)
                    {
                        var greenStyle = new GUIStyle(EditorStyles.boldLabel);
                        greenStyle.normal.textColor = Color.green;
                        EditorGUILayout.LabelField(group.NewName, greenStyle);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(group.NewName, EditorStyles.boldLabel);
                    }
                    
                    EditorGUILayout.LabelField("", GUILayout.Width(60)); // 占位符，保持对齐
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    // 多个对象，合并显示
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"{i + 1}.", GUILayout.Width(25));
                    
                    // 合并显示原始名称，用#分隔，保持原来的方法和颜色
                    string originalNames = string.Join(" # ", group.Objects.Select(obj => GetObjectDisplayName(obj)));
                    var miniStyle = new GUIStyle(EditorStyles.miniLabel);
                    miniStyle.fontSize = 9; // 使用更小的字体
                    
                    EditorGUILayout.LabelField(originalNames, miniStyle, GUILayout.Width(120));
                    
                    EditorGUILayout.LabelField("→", GUILayout.Width(20));
                    
                    // 检查是否有任何对象会改变名称
                    bool hasNameChange = group.Objects.Any(obj => GetObjectDisplayName(obj) != group.NewName);
                    if (hasNameChange)
                    {
                        var greenStyle = new GUIStyle(EditorStyles.boldLabel);
                        greenStyle.normal.textColor = Color.green;
                        EditorGUILayout.LabelField(group.NewName, greenStyle);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(group.NewName, EditorStyles.boldLabel);
                    }
                    
                    // 显示合并数量提示
                    EditorGUILayout.LabelField($"({group.Objects.Count}个对象)", EditorStyles.miniLabel, GUILayout.Width(60));
                    EditorGUILayout.EndHorizontal();
                }
                
                // 如果不是最后一个组，添加分隔线
                if (i < previewGroups.Count - 1)
                {
                    EditorGUILayout.Space(2);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.Space(2);
                }
            }
            
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // 操作按钮区域
        EditorGUILayout.LabelField("选中的对象数量: " + GetSelectedObjectsCount(), EditorStyles.helpBox);
        
        // 测试排序按钮
        if (GUILayout.Button("测试排序顺序", GUILayout.Height(25)))
        {
            TestSortingOrder();
        }
        
        // 分开的两个按钮：应用重命名和单独的查找替换
        if (GUILayout.Button("应用重命名", GUILayout.Height(30)))
        {
            RenameSelectedObjects();
        }
        
        if (GUILayout.Button("仅执行查找替换", GUILayout.Height(30)))
        {
            PerformFindReplaceOnly();
        }
        
        // 新增：显示预览列表（匹配BatchRenamer.cs）
        if (previewList.Count > 0)
        {
            EditorGUILayout.Space(15);
            EditorGUILayout.LabelField("预览列表:", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("显示重命名前后的名称对比", MessageType.Info);
            
            const float oldNameMinWidth = 100f;
            const float arrowWidth = 20f;
            
            previewListScrollPos = EditorGUILayout.BeginScrollView(previewListScrollPos, false, false, GUILayout.Height(200));
            
            foreach (var kvp in previewList)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    // 原始名称保持原来的方法和颜色
                    EditorGUILayout.LabelField(kvp.Key, GUILayout.MinWidth(oldNameMinWidth), GUILayout.MaxWidth(200));
                    EditorGUILayout.LabelField("→", GUILayout.Width(arrowWidth));
                    
                    // 只有当新名称与原始名称不同时才用绿色显示
                    if (kvp.Value != kvp.Key)
                    {
                        var greenStyle = new GUIStyle(EditorStyles.label);
                        greenStyle.normal.textColor = Color.green;
                        EditorGUILayout.LabelField(kvp.Value, greenStyle);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(kvp.Value);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndScrollView();
        }
        
        // 结束整个窗口的滚动视图
        EditorGUILayout.EndScrollView();
        
        // 调试：显示当前滚动位置（可选，用于调试）
        // EditorGUILayout.LabelField($"主窗口滚动位置: {mainWindowScrollPos}");
        // EditorGUILayout.LabelField($"预览滚动位置: {previewScrollPos}");
        // EditorGUILayout.LabelField($"预览列表滚动位置: {previewListScrollPos}");
    }

    // 原始的重命名功能
    private void RenameSelectedObjects()
    {
        if (targetArea == TargetArea.Hierarchy)
        {
            RenameHierarchyObjects();
        }
        else if (targetArea == TargetArea.Project)
        {
            RenameProjectAssets();
        }
    }

    // Hierarchy模式重命名
    private void RenameHierarchyObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "请先在Hierarchy中选择要重命名的对象", "确定");
            return;
        }

        // 按Hierarchy顺序排序
        selectedObjects = SortByHierarchyOrder(selectedObjects).ToArray();

        // 调试：显示排序后的顺序
        Debug.Log("排序后的对象顺序：");
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            GameObject obj = selectedObjects[i];
            Debug.Log($"索引 {i}: {obj.name} (Hierarchy路径: {GetHierarchyPath(obj)})");
        }

        // 开始批量重命名
        Undo.RecordObjects(selectedObjects, "批量重命名");

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            GameObject obj = selectedObjects[i];
            string newName = ConstructNewName(obj, i);
            obj.name = newName;
        }

        Debug.Log($"成功重命名 {selectedObjects.Length} 个Hierarchy对象，按Hierarchy显示顺序排序");
        
        // 清除预览列表
        previewList.Clear();
        previewGenerated = false;
    }

    // Project模式重命名
    private void RenameProjectAssets()
    {
        if (Selection.assetGUIDs.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "请在Project中选择要重命名的Asset", "确定");
            return;
        }

        // 检查目标文件夹
        if (moveToFolder)
        {
            if (!AssetDatabase.IsValidFolder(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
                AssetDatabase.Refresh();
            }
        }

        // 开始批量重命名
        int successCount = 0;

        for (int i = 0; i < Selection.assetGUIDs.Length; i++)
        {
            var guid = Selection.assetGUIDs[i];
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string oldName = Path.GetFileNameWithoutExtension(assetPath);
            string newName = ConstructNewName(assetPath, i);

            try
            {
                // 重命名Asset
                AssetDatabase.RenameAsset(assetPath, newName);
                successCount++;

                // 如果需要移动到文件夹
                if (moveToFolder)
                {
                    string currentPath = AssetDatabase.GUIDToAssetPath(guid);
                    string destPath = $"{targetFolderPath}/{newName}{Path.GetExtension(assetPath)}";
                    
                    if (currentPath != destPath)
                    {
                        AssetDatabase.MoveAsset(currentPath, destPath);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"重命名Asset失败: {assetPath}, 错误: {e.Message}");
            }
        }

        // 保存并刷新
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"成功重命名 {successCount} 个Project Asset");
        
        // 清除预览列表
        previewList.Clear();
        previewGenerated = false;
    }

    // 单独的查找替换功能，不影响基础命名逻辑
    private void PerformFindReplaceOnly()
    {
        if (targetArea == TargetArea.Hierarchy)
        {
            PerformHierarchyFindReplace();
        }
        else if (targetArea == TargetArea.Project)
        {
            PerformProjectFindReplace();
        }
    }

    // Hierarchy模式查找替换
    private void PerformHierarchyFindReplace()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "请先在Hierarchy中选择要重命名的对象", "确定");
            return;
        }

        if (!useFindReplace || string.IsNullOrEmpty(findText))
        {
            EditorUtility.DisplayDialog("提示", "请启用查找替换并输入查找内容", "确定");
            return;
        }

        // 开始查找替换
        Undo.RecordObjects(selectedObjects, "批量查找替换");

        int replacedCount = 0;
        foreach (GameObject obj in selectedObjects)
        {
            string originalName = obj.name;
            string newName;

            if (caseSensitive)
            {
                newName = originalName.Replace(findText, replaceText);
            }
            else
            {
                newName = ReplaceCaseInsensitive(originalName, findText, replaceText);
            }

            if (newName != originalName)
            {
                obj.name = newName;
                replacedCount++;
            }
        }

        Debug.Log($"成功在 {replacedCount} 个Hierarchy对象中执行查找替换");
        
        // 清除预览列表
        previewList.Clear();
        previewGenerated = false;
    }

    // Project模式查找替换
    private void PerformProjectFindReplace()
    {
        if (Selection.assetGUIDs.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "请先在Project中选择要重命名的Asset", "确定");
            return;
        }

        if (!useFindReplace || string.IsNullOrEmpty(findText))
        {
            EditorUtility.DisplayDialog("提示", "请启用查找替换并输入查找内容", "确定");
            return;
        }

        // 开始查找替换
        int replacedCount = 0;

        foreach (var guid in Selection.assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string oldName = Path.GetFileNameWithoutExtension(assetPath);
            string newName;

            if (caseSensitive)
            {
                newName = oldName.Replace(findText, replaceText);
            }
            else
            {
                newName = ReplaceCaseInsensitive(oldName, findText, replaceText);
            }

            if (newName != oldName)
            {
                try
                {
                    AssetDatabase.RenameAsset(assetPath, newName);
                    replacedCount++;
                }
                catch (Exception e)
                {
                    Debug.LogError($"重命名Asset失败: {assetPath}, 错误: {e.Message}");
                }
            }
        }

        // 保存并刷新
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"成功在 {replacedCount} 个Project Asset中执行查找替换");
        
        // 清除预览列表
        previewList.Clear();
        previewGenerated = false;
    }

    // 测试排序顺序的方法
    private void TestSortingOrder()
    {
        if (targetArea == TargetArea.Hierarchy)
        {
            TestHierarchySorting();
        }
        else if (targetArea == TargetArea.Project)
        {
            TestProjectSorting();
        }
    }

    // 测试Hierarchy排序
    private void TestHierarchySorting()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "请先在Hierarchy中选择要测试的对象", "确定");
            return;
        }

        // 按Hierarchy顺序排序
        var sortedObjects = SortByHierarchyOrder(selectedObjects).ToArray();

        Debug.Log("=== Hierarchy排序测试结果 ===");
        Debug.Log($"选中对象数量: {selectedObjects.Length}");
        Debug.Log("排序后的对象顺序：");
        
        for (int i = 0; i < sortedObjects.Length; i++)
        {
            GameObject obj = sortedObjects[i];
            string hierarchyPath = GetHierarchyPath(obj);
            Debug.Log($"索引 {i}: {obj.name} | 层级路径: {hierarchyPath}");
        }
        
        Debug.Log("=== Hierarchy排序测试完成 ===");
    }

    // 测试Project排序
    private void TestProjectSorting()
    {
        if (Selection.assetGUIDs.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "请先在Project中选择要测试的Asset", "确定");
            return;
        }

        var assetPaths = Selection.assetGUIDs.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();

        Debug.Log("=== Project排序测试结果 ===");
        Debug.Log($"选中Asset数量: {assetPaths.Length}");
        Debug.Log("Asset路径列表：");
        
        for (int i = 0; i < assetPaths.Length; i++)
        {
            string assetPath = assetPaths[i];
            string assetName = Path.GetFileNameWithoutExtension(assetPath);
            Debug.Log($"索引 {i}: {assetName} | 路径: {assetPath}");
        }
        
        Debug.Log("=== Project排序测试完成 ===");
    }

    // 新增：生成预览方法（匹配BatchRenamer.cs）
    private void GeneratePreview()
    {
        previewList.Clear();

        if (targetArea == TargetArea.Hierarchy)
        {
            var sortedObjects = SortByHierarchyOrder(Selection.gameObjects).ToArray();
            for (int i = 0; i < sortedObjects.Length; i++)
            {
                GameObject go = sortedObjects[i];
                string oldName = go.name;
                string newName = ConstructNewName(go, i);
                previewList.Add(new KeyValuePair<string, string>(oldName, newName));
            }
        }
        else if (targetArea == TargetArea.Project)
        {
            var assetGUIDs = Selection.assetGUIDs;
            for (int i = 0; i < assetGUIDs.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
                string oldName = Path.GetFileNameWithoutExtension(assetPath);
                string newName = ConstructNewName(assetPath, i);
                previewList.Add(new KeyValuePair<string, string>(oldName, newName));
            }
        }
        
        previewGenerated = true;
        Debug.Log($"生成了 {previewList.Count} 个预览项目");
    }

    // 预览组类，用于合并相同新名称的对象
    private class PreviewGroup
    {
        public List<object> Objects { get; set; } = new List<object>();
        public string NewName { get; set; }
    }

    // 分析预览组，将具有相同新名称的对象合并
    private List<PreviewGroup> AnalyzePreviewGroups(object[] objects)
    {
        var groups = new List<PreviewGroup>();
        var nameToGroup = new Dictionary<string, PreviewGroup>();
        
        for (int i = 0; i < objects.Length; i++)
        {
            object obj = objects[i];
            string newName = ConstructNewName(obj, i);
            
            if (nameToGroup.ContainsKey(newName))
            {
                // 添加到现有组
                nameToGroup[newName].Objects.Add(obj);
            }
            else
            {
                // 创建新组
                var newGroup = new PreviewGroup
                {
                    NewName = newName
                };
                newGroup.Objects.Add(obj);
                nameToGroup[newName] = newGroup;
                groups.Add(newGroup);
            }
        }
        
        return groups;
    }

    private string ReplaceCaseInsensitive(string original, string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(original) || string.IsNullOrEmpty(oldValue))
            return original;

        int index = 0;
        while ((index = original.IndexOf(oldValue, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            original = original.Remove(index, oldValue.Length);
            original = original.Insert(index, newValue);
            index += newValue.Length;
        }
        return original;
    }

    private IEnumerable<GameObject> SortByHierarchyOrder(GameObject[] objects)
    {
        // 使用Unity内置的方法来获取准确的层级路径，确保按照Hierarchy中的视觉顺序排序
        var sorted = objects.OrderBy(obj => GetHierarchyPath(obj));
        return sorted;
    }

    private string GetHierarchyPath(GameObject obj)
    {
        string path = obj.name;
        Transform parent = obj.transform.parent;

        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }

        return path;
    }

    // 修改后的构造新名称方法，现在接受object参数以支持不同类型的对象
    private string ConstructNewName(object obj, int index)
    {
        // 对于Project模式，使用简化的命名逻辑
        if (targetArea == TargetArea.Project)
        {
            string originalName = GetObjectName(obj);
            return BuildProjectName(originalName, index);
        }

        // Hierarchy模式的原有逻辑
        GameObject gameObj = obj as GameObject;
        if (gameObj == null) return "Unknown";

        string newName = "";

        // 添加父级名称前缀
        if (useParentNameAsPrefix)
        {
            Transform parent = gameObj.transform.parent;
            if (parent != null)
            {
                string parentName = parent.name;
                if (parentNameWithNumber)
                {
                    int number = parentNameStartNumber + index;
                    parentName += number.ToString($"D{parentNamePadding}");
                }
                newName += parentName + "_";
            }
        }

        // 添加前缀
        if (usePrefix)
        {
            newName += prefix;
            if (prefixWithNumber)
            {
                int number = prefixStartNumber + index;
                newName += number.ToString($"D{prefixPadding}");
            }
        }

        // 添加基础名称
        if (preserveOriginalName)
        {
            newName += gameObj.name;
        }
        else
        {
            newName += baseName;
        }

        // 添加后缀
        if (useSuffix)
        {
            if (suffixWithNumber)
            {
                int number = suffixStartNumber + index;
                // 新增：根据选项决定是否添加下划线
                if (addUnderscoreBeforeSuffixNumber)
                {
                    newName += "_";
                }
                newName += number.ToString($"D{suffixPadding}");
            }
            newName += suffix;
        }

        return newName;
    }

    // 获取对象名称（支持GameObject和Asset路径）
    private string GetObjectName(object obj)
    {
        if (obj is GameObject gameObj)
        {
            return gameObj.name;
        }
        else if (obj is string assetPath)
        {
            return Path.GetFileNameWithoutExtension(assetPath);
        }
        return "Unknown";
    }

    // Project模式命名逻辑
    private string BuildProjectName(string originalName, int index)
    {
        // 使用与Hierarchy模式相同的命名逻辑
        string newName = "";

        // 添加前缀
        if (usePrefix)
        {
            newName += prefix;
            if (prefixWithNumber)
            {
                int number = prefixStartNumber + index;
                newName += number.ToString($"D{prefixPadding}");
            }
        }

        // 添加基础名称
        if (preserveOriginalName)
        {
            newName += originalName;
        }
        else
        {
            newName += baseName;
        }

        // 添加后缀
        if (useSuffix)
        {
            if (suffixWithNumber)
            {
                int number = suffixStartNumber + index;
                if (addUnderscoreBeforeSuffixNumber)
                {
                    newName += "_";
                }
                newName += number.ToString($"D{suffixPadding}");
            }
            newName += suffix;
        }

        return newName;
    }

    // 获取当前选中的对象数量
    private int GetSelectedObjectsCount()
    {
        if (targetArea == TargetArea.Hierarchy)
        {
            return Selection.gameObjects.Length;
        }
        else if (targetArea == TargetArea.Project)
        {
            // 在Project模式下，获取选中的Asset数量
            return Selection.assetGUIDs.Length;
        }
        return 0;
    }

    // 获取排序后的对象列表
    private object[] GetSortedObjects()
    {
        if (targetArea == TargetArea.Hierarchy)
        {
            return SortByHierarchyOrder(Selection.gameObjects).ToArray();
        }
        else if (targetArea == TargetArea.Project)
        {
            // Project模式：返回Asset路径列表
            return Selection.assetGUIDs.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
        }
        return new object[0];
    }

    // 获取对象的显示名称
    private string GetObjectDisplayName(object obj)
    {
        if (obj is GameObject gameObj)
        {
            return gameObj.name;
        }
        else if (obj is string assetPath)
        {
            return Path.GetFileNameWithoutExtension(assetPath);
        }
        return "Unknown";
    }
    
    // 当窗口重新获得焦点时调用，确保滚动位置保持
    private void OnFocus()
    {
        // 这个方法会在窗口重新获得焦点时调用
        // 滚动位置应该会自动保持，不需要额外处理
    }
}
