

using System.Collections.Generic;
using UnityEngine;

public static class NnMathUtils
{
    public static List<T> Shuffle<T>(List<T> pool, int count)
    {
        List<T> rst = new List<T>();
        List<int> indexes = new List<int>();

        for (int i = 0; i < pool.Count; i++)
        {
            indexes.Add(i);
        }

        int num = Mathf.Min(count, pool.Count);
        int last = pool.Count;
        for (int i = 0; i < num; i++)
        {
            int idx = UnityEngine.Random.Range(0, last--);
            (indexes[idx], indexes[0]) = (indexes[0], indexes[idx]);
        }

        for (int i = 0; i < num; i++)
        {
            rst.Add(pool[indexes[i]]);
        }
        
        return rst;
    }

    public static float GetSign(float x)
    {
        return x >= 0 ? 1 : -1;
    }
}