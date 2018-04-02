using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
    [SerializeField]
    private Text textGps;
    	
	void Update ()
    {
        textGps.text = "Latitude : " + GPS.Instance.latitude + "\nLongitude : " + GPS.Instance.longitude;
	}
}
