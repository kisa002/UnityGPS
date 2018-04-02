using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    public Camera Cam;
    public GameObject objScreen;

    WebCamTexture m_WebcamTexture = null;
    ScreenOrientation m_ScreenOrientation = ScreenOrientation.Portrait;
    CameraClearFlags m_CameraClearFlags;

    void Awake()
    {
        foreach (Camera c in Camera.allCameras)
        {
            if (c != Cam)
                c.cullingMask = ~(1 << objScreen.layer);
        }
        Cam.gameObject.SetActive(false);
        objScreen.SetActive(false);
        Cam.farClipPlane = Cam.nearClipPlane + 1f;
        objScreen.transform.localPosition = new Vector3(0f, 0f, Cam.farClipPlane * .5f);
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            m_WebcamTexture = new WebCamTexture(Screen.width, Screen.height);
            objScreen.GetComponent<Renderer>().material.mainTexture = m_WebcamTexture;
        }
        m_ScreenOrientation = Screen.orientation;
        setOrientation(m_ScreenOrientation);
        StartCoroutine(coroutineOrientation());

        show(true);
    }

    void setOrientation(ScreenOrientation sc)
    {
        float h = Mathf.Tan(Cam.fieldOfView * Mathf.Deg2Rad * .5f) * objScreen.transform.localPosition.z * .2f;
        if (Cam.orthographic) h = Screen.height / Cam.pixelHeight;
        if (ScreenOrientation.Landscape == sc)
        {
            objScreen.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
            objScreen.transform.localScale = new Vector3(Cam.aspect * h, 1f, h);
        }
        else if (ScreenOrientation.LandscapeLeft == sc)
        {
            objScreen.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
            objScreen.transform.localScale = new Vector3(Cam.aspect * h, 1f, h);
        }
        else if (ScreenOrientation.LandscapeRight == sc)
        {
            objScreen.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            objScreen.transform.localScale = new Vector3(Cam.aspect * h, 1f, h);
        }
        else if (ScreenOrientation.Portrait == sc)
        {
            objScreen.transform.localRotation = Quaternion.Euler(0f, -90f, 90f);
            objScreen.transform.localScale = new Vector3(h, 1f, Cam.aspect * h);
        }
        else if (ScreenOrientation.PortraitUpsideDown == sc)
        {
            objScreen.transform.localRotation = Quaternion.Euler(0f, 90f, -90f);
            objScreen.transform.localScale = new Vector3(h, 1f, Cam.aspect * h);
        }
    }

    IEnumerator coroutineOrientation()
    {
        while (true)
        {
            if (m_ScreenOrientation != Screen.orientation)
            {
                m_ScreenOrientation = Screen.orientation;
                setOrientation(m_ScreenOrientation);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    public void show(bool flag)
    {
        if (null == m_WebcamTexture)
            return;
        if (flag)
        {
            if (Camera.main != Cam)
            {
                m_CameraClearFlags = Camera.main.clearFlags;
                Camera.main.clearFlags = CameraClearFlags.Depth;
            }
            Cam.gameObject.SetActive(true);
            objScreen.SetActive(true);
            m_WebcamTexture.Play();
        }
        else
        {
            if (Camera.main != Cam)
                Camera.main.clearFlags = m_CameraClearFlags;
            m_WebcamTexture.Pause();
            objScreen.SetActive(false);
            Cam.gameObject.SetActive(false);
        }
    }
}
