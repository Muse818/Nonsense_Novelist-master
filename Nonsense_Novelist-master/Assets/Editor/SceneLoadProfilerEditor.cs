using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class SceneLoadProfilerEditor : EditorWindow
{
    [MenuItem("����/�������ط�����")]
    public static void ShowWindow()
    {
        GetWindow<SceneLoadProfilerEditor>("�������ط�����");
    }

    private Vector2 scrollPos;

    private void OnGUI()
    {
        if (GUILayout.Button("������ǰ����"))
        {
            AnalyzeCurrentScene();
        }

        if (GUILayout.Button("���� DontDestroyOnLoad ����"))
        {
            AnalyzeDDOLObjects();
        }
    }

    private void AnalyzeCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log($"===== ����������ʼ��{scene.name} =====");

        GameObject[] rootObjects = scene.GetRootGameObjects();
        int totalObjects = rootObjects.Length;
        int totalChildObjects = rootObjects.Sum(o => CountChildren(o));

        Debug.Log($"����������: {totalObjects}, �������������������壩: {totalChildObjects}");

        // ͳ����Ҫ�������
        int canvasCount = Resources.FindObjectsOfTypeAll<Canvas>().Count(c => c.gameObject.scene == scene);
        int animatorCount = Resources.FindObjectsOfTypeAll<Animator>().Count(a => a.gameObject.scene == scene);
        int particleCount = Resources.FindObjectsOfTypeAll<ParticleSystem>().Count(p => p.gameObject.scene == scene);
        int rigidbodyCount = Resources.FindObjectsOfTypeAll<Rigidbody>().Count(r => r.gameObject.scene == scene);
        int scriptsCount = Resources.FindObjectsOfTypeAll<MonoBehaviour>().Count(mb => mb.gameObject.scene == scene);

        Debug.Log($"Canvas ����: {canvasCount}, Animator ����: {animatorCount}, ParticleSystem ����: {particleCount}, Rigidbody ����: {rigidbodyCount}, �ű�����: {scriptsCount}");
        Debug.Log("===== ������������ =====");
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

        Debug.Log($"===== DontDestroyOnLoad ������� =====");
        Debug.Log($"DDOL ��������: {ddolObjects.Count}");

        foreach (var obj in ddolObjects)
        {
            int childCount = CountChildren(obj);
            Debug.Log($"DDOL ����: {obj.name}, ����������: {childCount}");
        }

        Debug.Log("===== DDOL �������� =====");
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
