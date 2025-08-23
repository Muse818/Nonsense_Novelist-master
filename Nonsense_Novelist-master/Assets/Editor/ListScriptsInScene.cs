using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ListScriptsInScene : EditorWindow
{
    private Vector2 scrollPos;
    private Dictionary<string, List<GameObject>> scriptMap = new Dictionary<string, List<GameObject>>();

    [MenuItem("Tools/List All Scripts In Scene")]
    static void ShowWindow()
    {
        GetWindow<ListScriptsInScene>("Scripts In Scene");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("列出当前场景所有 C# 脚本"))
        {
            CollectScripts();
        }

        if (scriptMap.Count > 0)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            foreach (var kv in scriptMap)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label($"脚本: {kv.Key} (挂载 {kv.Value.Count} 个对象)");

                foreach (var obj in kv.Value)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"- {obj.name}");
                    if (GUILayout.Button("高亮", GUILayout.Width(60)))
                    {
                        // 在 Hierarchy 高亮对象
                        Selection.activeGameObject = obj;
                        EditorGUIUtility.PingObject(obj);
                    }
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndVertical();
            }

            GUILayout.EndScrollView();
        }
    }

    private void CollectScripts()
    {
        scriptMap.Clear();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script == null) continue;

                string scriptName = script.GetType().Name;
                if (!scriptMap.ContainsKey(scriptName))
                    scriptMap[scriptName] = new List<GameObject>();

                scriptMap[scriptName].Add(obj);
            }
        }

        Debug.Log($"=== 当前场景挂载的 C# 脚本列表 ({scriptMap.Count} 个脚本) ===");
        foreach (var kv in scriptMap)
        {
            Debug.Log($"{kv.Key} 挂载在 {kv.Value.Count} 个对象上");
        }
    }
}
