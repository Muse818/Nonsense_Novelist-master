using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneResourceAnalyzer : EditorWindow
{
    [MenuItem("Tools/������Դ����������ǿ�棩")]
    public static void ShowWindow()
    {
        GetWindow<SceneResourceAnalyzer>("������Դ������");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("������ǰ����"))
        {
            AnalyzeScene();
        }
    }

    private void AnalyzeScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!scene.isLoaded)
        {
            Debug.LogWarning("����δ����");
            return;
        }

        GameObject[] rootObjects = scene.GetRootGameObjects();
        List<AnalysisResult> results = new List<AnalysisResult>();

        foreach (var root in rootObjects)
        {
            AnalysisResult result = AnalyzeObject(root);
            results.Add(result);
        }

        // ��������Դ��С����
        results.Sort((a, b) => b.EstimatedSize.CompareTo(a.EstimatedSize));

        Debug.Log($"----- ���� {scene.name} ��Դ�����������ǿ�棩 -----");
        foreach (var r in results)
        {
            Debug.Log(
                $"������: {r.RootName} | ��������: {r.TotalChildren} | ���Ƕ�����: {r.MaxDepth} | Canvas: {r.CanvasCount} | Animator: {r.AnimatorCount} | ParticleSystem: {r.ParticleCount} | ������Դ��С: {r.EstimatedSize / 1024f:F2} KB"
            );
        }
    }

    private AnalysisResult AnalyzeObject(GameObject go)
    {
        AnalysisResult result = new AnalysisResult
        {
            RootName = go.name
        };

        Traverse(go.transform, 0, result);

        return result;
    }

    private void Traverse(Transform t, int depth, AnalysisResult result)
    {
        result.TotalChildren++;
        if (depth > result.MaxDepth)
            result.MaxDepth = depth;

        if (t.GetComponent<Canvas>()) result.CanvasCount++;
        if (t.GetComponent<Animator>()) result.AnimatorCount++;
        if (t.GetComponent<ParticleSystem>()) result.ParticleCount++;

        // ������Դ��С
        result.EstimatedSize += EstimateObjectSize(t.gameObject);

        foreach (Transform child in t)
        {
            Traverse(child, depth + 1, result);
        }
    }

    private long EstimateObjectSize(GameObject go)
    {
        long size = 0;

        // Mesh ��С
        MeshFilter mf = go.GetComponent<MeshFilter>();
        if (mf && mf.sharedMesh != null)
            size += EstimateMeshSize(mf.sharedMesh);

        // SkinnedMeshRenderer
        SkinnedMeshRenderer smr = go.GetComponent<SkinnedMeshRenderer>();
        if (smr && smr.sharedMesh != null)
            size += EstimateMeshSize(smr.sharedMesh);

        // ���ʺ������С
        Renderer renderer = go.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (var mat in renderer.sharedMaterials)
            {
                if (mat == null) continue;
                foreach (var texName in mat.GetTexturePropertyNames())
                {
                    Texture tex = mat.GetTexture(texName);
                    if (tex != null)
                        size += EstimateTextureSize(tex);
                }
            }
        }

        // AudioClip ��С
        AudioSource audio = go.GetComponent<AudioSource>();
        if (audio && audio.clip != null)
            size += EstimateAudioClipSize(audio.clip);

        return size;
    }

    private long EstimateMeshSize(Mesh mesh)
    {
        // ���� + ���� + uv ��Լÿ�� float 4 �ֽ�
        long verts = mesh.vertexCount * 3 * 4;
        long normals = mesh.vertexCount * 3 * 4;
        long uvs = mesh.vertexCount * 2 * 4;
        long triangles = mesh.triangles.Length * 4; // int

        return verts + normals + uvs + triangles;
    }

    private long EstimateTextureSize(Texture tex)
    {
        if (tex == null) return 0;
        // ���Թ��㣺�� * �� * 4 �ֽڣ�RGBA32��
        return tex.width * tex.height * 4;
    }

    private long EstimateAudioClipSize(AudioClip clip)
    {
        if (clip == null) return 0;
        // ���Թ��㣺samples * channels * 2 �ֽڣ�16-bit PCM��
        return clip.samples * clip.channels * 2;
    }

    private class AnalysisResult
    {
        public string RootName;
        public int TotalChildren = 0;
        public int MaxDepth = 0;
        public int CanvasCount = 0;
        public int AnimatorCount = 0;
        public int ParticleCount = 0;
        public long EstimatedSize = 0; // ��Դ��С���㣬��λ�ֽ�
    }
}
