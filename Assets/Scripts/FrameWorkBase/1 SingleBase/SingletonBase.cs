using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**说明
 * 单例模式往往都做相同的事情,保证实例唯一
 * 并在调用某方法返回那个唯一的实例
 * 单例基类减少了单例模式的代码量
 */

/// <summary>
/// [单例模式基类]
/// 继承此类即可将子类变为单例模式
/// 要求必须子类有构造方法
/// </summary>
/// <typeparam name="T">单例类的类型</typeparam>
public class SingletonBase<T> where T:class,new()
{
    //唯一的实例对象
    private static T instance;
    //非必须的,但可以保障安全
    //private SingletonBase() { }

    public static T GetInstance() 
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}
