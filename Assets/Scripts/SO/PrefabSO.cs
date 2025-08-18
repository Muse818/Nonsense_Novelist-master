using System.Collections.Generic;
using UnityEngine;
public enum prefabSOType
{
    Other,
    UI,
    BattleObj,    
}

[CreateAssetMenu(fileName = "PrefabSO", menuName = "PrefabData", order = 1)]
public class PrefabSO : ScriptableObject
{
    [System.Serializable]
    public class PrefabData
    {
        public string mName;
        public GameObject mPrefab;
    }
    public List<PrefabData> OtherpPrefabs; 
    public List<PrefabData> UIPrefabs;
    public List<PrefabData> BattleObjPrefabs; 
}