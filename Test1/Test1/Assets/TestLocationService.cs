using UnityEngine;
using System.Collections;
using System.Net;

public class TestLocationService : MonoBehaviour
{
    public static TestLocationService Instance { set; get; }

    public double latitude= 22.259346;
    public double longitude= 114.130259;
    public double altitude=20;
    private void Start()
    {
        Instance = this;
        StartCoroutine((StartLocationService()));
    }
    public IEnumerator StartLocationService()
    {

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            print("NotEnabled");
           // yield break;
        }


        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        print(Input.location.status);
        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            //latitude = Input.location.lastData.latitude;
            //longitude = Input.location.lastData.longitude;
            //altitude = Input.location.lastData.altitude;
            yield break;
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }
}