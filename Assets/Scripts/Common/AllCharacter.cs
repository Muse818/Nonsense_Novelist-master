using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 所有角色静态类
/// </summary>
public static class AllCharacter
{
    /// <summary>全部角色</summary>
    public static List<Type> list_allchara = new List<Type>();

    static AllCharacter()
    {
        list_allchara.AddRange(new Type[] { typeof(Anubis),typeof(BeiLuoJi),typeof(DiKaDe), typeof(LinDaiYu), 
            typeof(MuNaiYi),typeof(Rat),typeof(ShiLian), typeof(SiYangYuan), typeof(ShaLeMei),typeof(WangXiFeng),
        
        });
    }
    /// <summary>
    /// 静态随机生成角色
    /// </summary>
    public static Type CreateCharacter()
    {
        int number = UnityEngine.Random.Range(0, list_allchara.Count);
        return list_allchara[number];
    }
    /// <summary>
    /// 返回全部角色
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Type AllCharacters(int i)
    {
        return list_allchara[i];
    }
}
