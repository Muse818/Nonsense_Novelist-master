using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class SceneLoadProfilerEditor : EditorWindow
{
    [MenuItem("工具/场景加载分析器")]
    public static void ShowWindow()
    {
        GetWindow<SceneLoadProfilerEditor>("场景加载分析器");
    }

    private Vector2 scrollPos;

    private void OnGUI()
    {
        if (GUILayout.Button("分析当前场景"))
        {
            AnalyzeCurrentScene();
        }

        if (GUILayout.Button("分析 DontDestroyOnLoad 对象"))
        {
            AnalyzeDDOLObjects();
        }
    }

    private void AnalyzeCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log($"===== 场景分析开始：{scene.name} =====");

        GameObject[] rootObjects = scene.GetRootGameObjects();
        int totalObjects = rootObjects.Length;
        int totalChildObjects = rootObjects.Sum(o => CountChildren(o));

        Debug.Log($"根物体数量: {totalObjects}, 总物体数量（含子物体）: {totalChildObjects}");

        // 统计主要组件数量
        int canvasCount = Resources.FindObjectsOfTypeAll<Canvas>().Count(c => c.gameObject.scene == scene);
        int animatorCount = Resources.FindObjectsOfTypeAll<Animator>().Count(a => a.gameObject.scene == scene);
        int particleCount = Resources.FindObjectsOfTypeAll<ParticleSystem>().Count(p => p.gameObject.scene == scene);
        int rigidbodyCount = Resources.FindObjectsOfTypeAll<Rigidbody>().Count(r => r.gameObject.scene == scene);
        int scriptsCount = Resources.FindObjectsOfTypeAll<MonoBehaviour>().Count(mb => mb.gameObject.scene == scene);

        Debug.Log($"Canvas 数量: {canvasCount}, Animator 数量: {animatorCount}, ParticleSystem 数量: {particleCount}, Rigidbody 数量: {rigidbodyCount}, 脚本数量: {scriptsCount}");
        Debug.Log("===== 场景分析结束 =====");
    }

    private void AnalyzeDDOLObjects()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        var ddolObjects = new List<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.hideFlags == HideFlags.DontSave)
            {
                ddolObjects.Add(obj);
            }
        }

        Debug.Log($"===== DontDestroyOnLoad 对象分析 =====");
        Debug.Log($"DDOL 对象总数: {ddolObjects.Count}");

        foreach (var obj in ddolObjects)
        {
            int childCount = CountChildren(obj);
            Debug.Log($"DDOL 对象: {obj.name}, 子物体数量: {childCount}");
        }

        Debug.Log("===== DDOL 分析结束 =====");
    }

    private int CountChildren(GameObject obj)
    {
        int count = obj.transform.childCount;
        foreach (Transform child in obj.transform)
        {
            count += CountChildren(child.gameObject);
        }
        return count;
    }
}
