using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCamera : MonoBehaviour
{
    WebCamTexture camTexture;
    Renderer renderer;

	void Start ()
    {
        camTexture = new WebCamTexture();
        renderer = GetComponent<Renderer>();

        renderer.material.mainTexture = camTexture;

        camTexture.Play();
	}

    void Update ()
    {
		
	}
}
