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
        if (GUILayout.Button("�г���ǰ�������� C# �ű�"))
        {
            CollectScripts();
        }

        if (scriptMap.Count > 0)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            foreach (var kv in scriptMap)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label($"�ű�: {kv.Key} (���� {kv.Value.Count} ������)");

                foreach (var obj in kv.Value)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"- {obj.name}");
                    if (GUILayout.Button("����", GUILayout.Width(60)))
                    {
                        // �� Hierarchy ��������
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

        Debug.Log($"=== ��ǰ�������ص� C# �ű��б� ({scriptMap.Count} ���ű�) ===");
        foreach (var kv in scriptMap)
        {
            Debug.Log($"{kv.Key} ������ {kv.Value.Count} ��������");
        }
    }
}
