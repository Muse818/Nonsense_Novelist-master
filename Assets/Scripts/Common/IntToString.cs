using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据参数的int返回值string的描述词
/// </summary>
public static class IntToString 
{
    /// <summary>错误字符（或者根据需要改成其他奇怪的描述 比如：离谱）</summary>
    private static string errorString = "ERROR";
    /// <summary>
    /// 攻击
    /// </summary>
   public static string SwitchATK(float data)
    {
       if(data>=0&&data<3)
        {
            return "蚍蜉";
        }
       else if(data>=3&&data<7)
        {
            return "寻常";
        }
       else if(data>=7&&data<12)
        {
            return "凶猛";
        }
       else if(data>=12&&data<20)
        {
            return "残暴";
        }
       else if(data >=20&&data<999)
        {
            return "登峰造极";
        }
       else  //小于0或者大于999返回错误字符（或者根据需要改成其他奇怪的描述 比如：离谱）
        {
            return errorString;
        }
    }

    /// <summary>
    /// 防御
    /// </summary>
    public static string SwitchDEF(float data)
    {
        if (data >= 0 && data < 3)
        {
            return "纤弱";
        }
        else if (data >= 3 && data < 7)
        {
            return "寻常";
        }
        else if (data >= 7 && data < 12)
        {
            return "坚固";
        }
        else if (data >= 12 && data < 20)
        {
            return "磐石般";
        }
        else if (data >= 20 && data < 999)
        {
            return "不灭金身";
        }
        else  //小于0或者大于999
        {
            return errorString;
        }
    }

    /// <summary>
    /// 精神
    /// </summary>
    public static string SwitchPSY(float data)
    {
        if (data >= 0 && data < 3)
        {
            return "愚钝";
        }
        else if (data >= 3 && data < 7)
        {
            return "寻常";
        }
        else if (data >= 7 && data < 12)
        {
            return "聪明";
        }
        else if (data >= 12 && data < 20)
        {
            return "深邃";
        }
        else if (data >= 20 && data < 999)
        {
            return "移星换斗";
        }
        else  //小于0或者大于999
        {
            return errorString;
        }
    }

    /// <summary>
    /// 意志
    /// </summary>
    public static string SwitchSAN(float data)
    {
        if (data >= 0 && data < 3)
        {
            return "薄弱";
        }
        else if (data >= 3 && data < 7)
        {
            return "寻常";
        }
        else if (data >= 7 && data < 12)
        {
            return "笃定";
        }
        else if (data >= 12 && data < 20)
        {
            return "坚韧";
        }
        else if (data >= 20 && data < 999)
        {
            return "超尘脱俗";
        }
        else  //小于0或者大于999
        {
            return errorString;
        }
    }

    /// <summary>
    /// 暴击几率
    /// </summary>
    public static string SwitchCriticalChance(float data)
    {
        if (data== 0)
        {
            return "妄想";
        }
        else if (data >0 && data <0.2)
        {
            return "冒失";
        }
        else if (data >= 0.2 && data <0.5)
        {
            return "寻常";
        }
        else if (data >= 0.5 && data < 0.75)
        {
            return "熟练";
        }
        else if (data >= 0.75 && data < 0.95)
        {
            return "炉火纯青";
        }
        else if(data >= 0.95 && data <= 1)
        {
            return "出神入化";
        }
        else  //小于0或者大于1
        {
            return errorString;
        }
    }

    /// <summary>
    /// 暴击倍率
    /// </summary>
    public static string SwitchMultipleCriticalStrike(int data)
    {
        if (data >= 0 && data < 2)
        {
            return "寻常";
        }
        else if (data >= 2 && data <4)
        {
            return "凶猛";
        }
        else if (data >= 4 && data < 6)
        {
            return "残暴";
        }
        else if (data >= 6 && data < 10)
        {
            return "怪物般";
        }
        else if (data >= 10 && data < 999)
        {
            return "风魔九伯";
        }
        else  //小于0或者大于999
        {
            return errorString;
        }
    }

    /// <summary>
    /// 攻击间隔
    /// </summary>
    public static string SwitchAttackInterval(int data)
    {
        if (data >= 2.5 && data < 999)
        {
            return "僵硬";
        }
        else if (data >= 1.7 && data < 2.5)
        {
            return "缓慢";
        }
        else if (data >= 1.2&& data < 1.7)
        {
            return "寻常";
        }
        else if (data >=0.7 && data < 1.2)
        {
            return "迅速";
        }
        else if (data >0 && data <0.7)
        {
            return "疾风骤雨";
        }
        else  //小于等于0或者大于999
        {
            return errorString;
        }
    }

    /// <summary>
    /// 技能速度
    /// </summary>
    public static string SwitchSkillSpeed(int data)
    {
        if (data == 0)
        {
            return "寻常";
        }
        else if (data >0 && data < 0.2)
        {
            return "灵动";
        }
        else if (data >= 0.2 && data < 0.4)
        {
            return "迅速";
        }
        else if (data >= 0.4 && data <= 0.7)
        {
            return "疾风骤雨";
        }
        else  //小于0或者大于0.7
        {
            return errorString;
        }
    }

    /// <summary>
    /// 攻击范围
    /// </summary>
    public static string SwitchAttackDistance(int data)
    {
        if (data == 0)
        {
            return "妄想";
        }
        else if (data > 0 && data < 2)
        {
            return "咫尺";
        }
        else if (data >=2 && data < 4)
        {
            return "鞭长";
        }
        else if (data >= 4 && data < 7)
        {
            return "没羽箭";
        }
        else if (data >= 7 && data < 999)
        {
            return "漫无边际";
        }
        else  //小于0或者大于999
        {
            return errorString;
        }
    }

    /// <summary>
    /// 生命成长
    /// </summary>
    public static string SwitchHPGrow(int data)
    {
        if (data >= 0 && data < 3)
        {
            return "薄命";
        }
        else if (data >= 3 && data < 5)
        {
            return "寻常";
        }
        else if (data >= 5 && data < 8)
        {
            return "强壮";
        }
        else if (data >= 8 && data <= 12)
        {
            return "异常";
        }
        else  //小于0或者大于12
        {
            return errorString;
        }
    }

    /// <summary>
    /// 魔法成长
    /// </summary>
    public static string SwitchSPGrow(int data)
    {
        if (data >= 0 && data < 3)
        {
            return "枯槁";
        }
        else if (data >= 3 && data < 5)
        {
            return "寻常";
        }
        else if (data >= 5 && data < 8)
        {
            return "萌发";
        }
        else if (data >= 8 && data <= 12)
        {
            return "泉涌般";
        }
        else  //小于0或者大于12
        {
            return errorString;
        }
    }

    /// <summary>
    /// 攻击成长
    /// </summary>
    public static string SwitchATKGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "蚍蜉";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "寻常";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "凶猛";
        }
        else if (data >= 2 && data <= 4)
        {
            return "残暴";
        }
        else  //小于0或者大于4
        {
            return errorString;
        }
    }

    /// <summary>
    /// 防御成长
    /// </summary>
    public static string SwitchDEFGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "纤弱";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "寻常";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "坚固";
        }
        else if (data >= 2 && data <= 3.5)
        {
            return "磐石般";
        }
        else  //小于0或者大于3.5
        {
            return errorString;
        }
    }

    /// <summary>
    /// 精神成长
    /// </summary>
    public static string SwitchPSYGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "愚钝";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "寻常";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "聪明";
        }
        else if (data >= 2 && data <= 4)
        {
            return "深邃";
        }
        else  //小于0或者大于4
        {
            return errorString;
        }
    }

    /// <summary>
    /// 意志成长
    /// </summary>
    public static string SwitchSANGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "薄弱";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "寻常";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "笃定";
        }
        else if (data >= 2 && data <= 3.5)
        {
            return "坚韧";
        }
        else  //小于0或者大于3.5
        {
            return errorString;
        }
    }
}
