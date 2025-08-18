using UnityEngine;
using UnityEngine.UI;

public static class UIStatics
{
    static Camera mUICamera;
    static Canvas mMainCanvas;
    static float mCanvasWidth = 0f;
    static float mCanvasHeight = 0f;

    public static Camera uiCamera => mUICamera;

    public static Canvas mainCanvas => mMainCanvas;

    public static float canvasWidth => mCanvasWidth;

    public static float canvasHeight => mCanvasHeight;

    static AudioSource mMenuAudio;
    public static AudioSource menuAudio => mMenuAudio;

    public static void ResetCanvas()
    {
        mMainCanvas = null;
        mUICamera = null;
        {
            GameObject go = GameObject.FindGameObjectWithTag("MainCanvas");
            if (go != null)
            {
                mMainCanvas = go.GetComponent<Canvas>();
            }
        }

        {
            GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
            if (go != null)
            {
                mUICamera = go.GetComponent<Camera>();
            }
        }

        mMenuAudio = mMainCanvas.GetComponent<AudioSource>();
        
        CanvasScaler scaler = mainCanvas.GetComponent<CanvasScaler>();

        mCanvasWidth = scaler.referenceResolution.x;
        mCanvasHeight = scaler.referenceResolution.y;
    }

    public static Vector3 WorldToUIPosition(Vector3 worldPos)
    {
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(worldPos);
            pos.x = (pos.x - 0.5f) * UIStatics.canvasWidth;
            pos.y = (pos.y - 0.5f) * UIStatics.canvasHeight;
            pos.z = 0f;

            return pos;
        }
        else
            return worldPos;
    }

    public static Vector3 WorldToUIPositionInWorldSpace(Vector3 worldPos)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(worldPos);
        pos.x = (pos.x - 0.5f) * UIStatics.canvasWidth;
        pos.y = (pos.y - 0.5f) * UIStatics.canvasHeight;
        pos.z = 0f;
        
        return mMainCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(pos);
    }

    public static Vector3 ScreenToUIPosition(Vector2 screenPos)
    {
        Vector3 pos = new Vector3((screenPos.x - Screen.width * 0.5f) / (float)mUICamera.pixelWidth, 
            (screenPos.y - Screen.height * 0.5f) / (float)mUICamera.pixelHeight, 0f);

        pos.x = pos.x * UIStatics.canvasWidth;
        pos.y = pos.y * UIStatics.canvasHeight;

        return pos;
    }

    public static Vector2 ScreenToUIVector(Vector2 screenVec)
    {
        return new Vector2(screenVec.x * canvasWidth / (float)mUICamera.pixelWidth,
                           screenVec.y * canvasHeight / (float)mUICamera.pixelHeight);
    }

    public static Vector2 UIToScreenVector(Vector2 uiVec)
    {
        return new Vector2(uiVec.x * (float)mUICamera.pixelWidth / canvasWidth,
                           uiVec.y * (float)mUICamera.pixelHeight / canvasHeight);
    }

    public static bool PointInViewport(Vector2 screenPos)
    {
        return screenPos.x > -canvasWidth * 0.5f && screenPos.x < canvasWidth * 0.5f && 
            screenPos.y > -canvasHeight * 0.5f && screenPos.y < canvasHeight * 0.5f;
    }
}
