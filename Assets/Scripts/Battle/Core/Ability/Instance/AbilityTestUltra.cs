
using UnityEngine;

public class AbilityTestUltra : AbilityUltraBase
{
    public override BattleUnit PickTarget()
    {
        var enemies = Unit.Enemies;
        if (enemies.Count > 0)
        {
            return enemies[Random.Range(0, enemies.Count)];
        }
        
        return null;
    }

    public override void OnAnimTrigger()
    {
        Debug.Log("Test Ultra");
    }
}