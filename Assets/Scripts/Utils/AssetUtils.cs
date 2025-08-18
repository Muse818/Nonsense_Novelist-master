

using UnityEngine;

public static class AssetUtils
{
    public static Sprite ToSprite(Texture2D texture2D)
    {
        if (texture2D == null)
            return null;
        
        return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    }
}