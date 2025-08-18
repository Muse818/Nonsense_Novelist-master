using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** 说明
 * 对于Unity来说继承Mono的类也会用到基类
 * 继承Mono的单例在每次挂载脚本时,都在Awake中赋值instance
 * 注意:
 * 开发者必须保证,场景中只有一个对象挂按载了此脚本
 * 场景中多个对象都挂在此脚本,其中的this只会指向最后挂载的那个,造成浪费
 */


/// <summary>
/// 继承MonoBehaviour的单例基类
/// 继承了此类后子类会变为继承了MonoBehaviour的类,且具有单例特征
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoBase<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    //保护类型的虚函数保证子类不会顶掉父类的逻辑
    protected virtual void Awake()
    {
        instance = this as T;
    }

    private T GetInstance()
    {
        return instance;
    }
}