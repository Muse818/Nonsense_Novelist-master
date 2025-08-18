using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;

    private float count;
    private float nowSize;
    private Coroutine coroutineSize;
    private Coroutine coroutineY;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        
    }
    bool pausing = false;

    public void SetCameraSize(float _size)
    {
        if(camera==null)
            camera = GetComponent<Camera>();
        camera.orthographicSize = _size;
    }
    public void SetCameraSizeTo(float _size)
    {

        //print("SetCameraSizeTo");
        camera = GetComponent<Camera>();
        nowSize = _size;

        if (Time.timeScale == 0)
        {
            pausing = true;
        }


        if (Mathf.Abs(_size- camera.orthographicSize ) > 0.001f)
        {
            if(coroutineSize!=null)
            StopCoroutine(coroutineSize);
            count =_size -camera.orthographicSize  ;
            coroutineSize=StartCoroutine(ChangeCameraSize());
        }
    }
    IEnumerator ChangeCameraSize()
    {
      if(pausing)Time.timeScale = GameMgr.instance.timeSpeed;
        //print("ChangeCameraSize");
        while (Mathf.Abs(camera.orthographicSize - nowSize) > 0.001f)
        { 
            yield return new WaitForFixedUpdate();
    
            camera.orthographicSize += (count) * 0.02f; 
            //count = camera.orthographicSize - nowSize;
        }
        if (pausing)
        {
            Time.timeScale = 0f;
            pausing = false;
        }
      
    }
    float nowY = 0;
    float countY = 0;
    public void SetCameraYTo(float _Y)
    {
        if (camera == null)
            camera = GetComponent<Camera>();
        nowY = _Y;

        if (Time.timeScale == 0)
        {
            pausing = true;
        }


        if (Mathf.Abs(_Y - camera.transform.position.y) > 0.001f)
        {
            if (coroutineY != null)
                StopCoroutine(coroutineY);
            countY = _Y - camera.transform.position.y;
            coroutineY=StartCoroutine(ChangeCameraY());
        }
    }


    IEnumerator ChangeCameraY()
    {
        if (pausing) Time.timeScale = GameMgr.instance.timeSpeed;

        while (Mathf.Abs(camera.transform.position.y - nowY) > 0.001f)
        {
            yield return new WaitForFixedUpdate();

            camera.transform.position += new Vector3(0,(countY) * 0.02f,0);
            //count = camera.orthographicSize - nowSize;
        }
        if (pausing)
        {
            Time.timeScale = 0f;
            pausing = false;
        }

    }
}
