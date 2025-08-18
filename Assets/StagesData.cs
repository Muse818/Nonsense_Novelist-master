using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StagesData", menuName = "TT/NewStageData")]
public class StagesData : ScriptableObject
{
    public List<OneStageData> stagesData = new List<OneStageData>();
}


