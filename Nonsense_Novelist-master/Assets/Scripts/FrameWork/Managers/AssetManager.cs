using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;

public class AssetManager
{
    protected static BattleEffectSO mEffectSO = null;
    protected static CommonSO mCommonSO = null;
    
    public static T Load<T>(string path, string assetName) where T : Object
    {
        return Load<T>(Path.Combine(path, assetName));
    }
    
    public static T Load<T>(string assetPath) where T : Object
    {
        T asset = Resources.Load<T>(assetPath);

        if (asset == null)
        {
            Debug.LogError($"failed to load {typeof(T).ToString()} asset : {assetPath}");
            return null;
        }
        else
        {
            if (asset is GameObject)
            {
                return Object.Instantiate(asset);
            }

            return asset;
        }
    }

    public static void UnLoad(Object asset)
    {
        Resources.UnloadAsset(asset);
    }

    public static BattleEffectSO GetEffectAsset()
    {
        if (mEffectSO == null)
        {
            mEffectSO = Load<BattleEffectSO>("SO/Effect", "EffectSO");
        }
            
        return mEffectSO;
    }
    
    public static CommonSO GetCommonAsset()
    {
        if (mCommonSO == null)
        {
            mCommonSO = Load<CommonSO>("SO", "CommonSO");
        }
            
        return mCommonSO;
    }
}
