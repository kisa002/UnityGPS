using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{

    public static GPS Instance { set; get; }

    public float latitude;
    public float longitude;

    public string result = "NULL";

	void Start ()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
	}

    private void Update()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            result = "GPS가 활성화되있지 않습니다";

            yield break;
        }

        Input.location.Start();

        //int maxWait = 20;

        //while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        //{
        //    yield return new WaitForSeconds(1);
        //    maxWait--;
        //}

        //if (maxWait <= 0)
        //{
        //    result = "시간 초과";

        //    yield break;
        //}

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            result = "위치를 찾을 수 없습니다";

            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        result = "Success";

        yield break;
    }
}
