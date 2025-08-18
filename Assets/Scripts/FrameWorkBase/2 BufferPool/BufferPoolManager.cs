using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//每个缓存池中的数据
public class poolData
{
    //此类数据容器的在场景中的根节点
    public GameObject fatherObj;
    //对象的容器
    public List<GameObject> poolList ;

    /// <summary>
    /// 初始化一个缓冲分区
    /// 
    /// 创建一个与资源名称相同的缓冲区节点
    /// 作为此类资源父节点,并在整个缓冲池节点下
    /// 并将缓存资源放入此节点
    /// </summary>
    /// <param name="obj">缓冲区的根节点</param>
    /// <param name="poolObj">缓冲池根节点</param>
    public poolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        //设置缓存池为缓存区的父对象
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        //将对象压入缓存区
        PushObj(obj);
    }

    /// <summary>
    /// 向缓冲分区中存储
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //加入分区
        poolList.Add(obj);
        //设置根节点为缓冲分区节点
        obj.transform.parent = fatherObj.transform;
        //设置对象失活
        obj.SetActive(false);
    }

    /// <summary>
    /// 取出一个对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;
        if (poolList.Count > 0)
        {
            //取出一个对象
            obj = poolList[0];
            //设置对象激活
            obj.SetActive(true);
            //从缓冲区节点中取出
            //父节点置空,使得不在缓冲区
            obj.transform.parent = null ;
            //从List中取出
            poolList.RemoveAt(0);
        }
        return obj;
    }
}

/// <summary>
/// 缓冲池模块
/// 将暂时不用的对象储存在此处，用时拿出
/// 避免重复的内存分配带来的性能浪费
/// </summary>
public class BufferPoolManager : SingletonBase<BufferPoolManager>
{
    //<缓存池类型,每个类型的存储对象>
    Dictionary<string, poolData> bufferPool=new Dictionary<string, poolData>();
    //缓存池的场景中的根对象
    private GameObject poolObj;


    /// <summary>
    /// 取出缓冲区的对象
    /// 2次修订:结合资源异步加载使得资源过大也不会明显卡顿
    /// </summary>
    /// <param name="name">对象的名称、缓冲区名称、资源路径名称相同</param>
    /// <param name="callback">回调函数,其参数是加载完成的资源</param>
    public void GetObj(string name,UnityAction<GameObject> callback)
    {
        GameObject obj = null;
        //已经存在这一类型缓冲区并且有内容
        if (bufferPool.ContainsKey(name) && bufferPool[name].poolList.Count > 0) 
        {
            //取出缓冲区的一个,放入回调函数便于后期处理
            callback(bufferPool[name].GetObj());
        }
        else
        {
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            //对象名和缓冲池名字相同,不使用时方面压入对应池
            //obj.name = name;
            ResourceManager.GetInstance().LoadResourceAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callback(o);
            });
        }
        //return obj ;
    }

    /// <summary>
    /// 将暂时不同的对象放入缓存池
    /// 在面板中的缓冲池节点中应该建立节点放入不同的对象，便于直观
    /// </summary>
    /// <param name="name">对象的名称或者缓冲区名称以及路径相同</param>
    /// <param name="obj">要放入的对象</param>
    public void PushObject(string name, GameObject obj)
    {
        if (obj == null)
            return;

        //在场景创建缓存池节点
        if (poolObj == null)
            poolObj = new GameObject("PoolBuffer");

        //设置为资源父对象为缓存池
        obj.transform.SetParent(poolObj.transform);

        //失活,隐藏
        obj.SetActive(false);
        //存在此缓冲区放入
        if (bufferPool.ContainsKey(name))
        {
            bufferPool[name].PushObj(obj);
        }
        //不存在的缓冲区创建并压入
        else

        {
            //压入的缓冲区,并放在公共缓冲池对象poolObj中
            bufferPool.Add(name, new poolData(obj, poolObj)); 
        }
    }

    /// <summary>
    /// 场景切换时使用
    /// 切换场景时应该将上一个场景的引用清除,防止空引用
    /// </summary>
    public void ClearBufferr()
    {
        bufferPool.Clear();
        poolObj = null; 
    }

}
