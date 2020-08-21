using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public double TestGPSx, TestGPSy, TestAttitude;
    public GameObject Camera;
    public ReferrenceData[] referrenceDatas;
    public ReferrenceData referrencepoint;
    public double refx, refy, refz;
    // Start is called before the first frame update

    public Vector3 previousPosition;
    public Vector3 deltaPosition;
    private float redirectTime = 2f;
    private float MaxMovingRange = 300;
    public TestLocationService TLS;
    public Transform SpawnPoint;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

    }
    void Start()
    {
        AverageGPSDAta();
        //refx = (dataA.transform.position.x - dataB.transform.position.x) / (dataA.GPSrefy - dataB.GPSrefy);
        //refy = (dataA.transform.position.z - dataB.transform.position.z) / (dataA.GPSrefx - dataB.GPSrefx);
        //refz = (dataC.transform.position.y - dataD.transform.position.y) / (dataC.attitude - dataD.attitude);
        previousPosition = Camera.transform.position;
        LoadData();
        InvokeRepeating("GPSLocation", 10f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (TLS != null)
        {
            if (Camera)
            {
                float distance = Vector3.Distance(Camera.transform.position, previousPosition + deltaPosition);
                if (distance > 1 & distance < MaxMovingRange)
                    Camera.transform.position += deltaPosition * Time.deltaTime / redirectTime;
                else if (distance > MaxMovingRange)
                    Camera.transform.position += deltaPosition;
            }
        }
        else
        {
            if (GameObject.Find("SceneManager") && GameObject.Find("SceneManager").GetComponent<TestLocationService>())
                TLS = GameObject.Find("SceneManager").GetComponent<TestLocationService>();
            else
                CancelInvoke();
        }
    }

    public void LocatePlayer(double GPSx, double GPSy, double GPSa)
    {
        float distance = Vector3.Distance(Camera.transform.position, previousPosition + deltaPosition);
        if (Vector3.Distance(Camera.transform.position, previousPosition + deltaPosition) > 1)
            return;
        else
            previousPosition = Camera.transform.position;

        double Refx = GPSy - referrencepoint.GPSrefy;
        double Refy = GPSx - referrencepoint.GPSrefx;
        double Refz = GPSa - referrencepoint.attitude;
        double posx = referrencepoint.transform.position.x + refx * Refx;
        double posy = referrencepoint.transform.position.z + refy * Refy;
        //double posz = referrencepoint.transform.position.y + refz * Refz;
        double posz = 10;
        deltaPosition = new Vector3((float)posx, (float)posz, (float)posy) - previousPosition;
    }

    void AverageGPSDAta()
    {
        if (referrenceDatas.Length < 1)
            return;
        ReferrenceData temp = referrenceDatas[0];

        for (int i = 0; i < referrenceDatas.Length; i++)
        {
            if (referrenceDatas[i].transform.position.x < temp.transform.position.x)
                temp = referrenceDatas[i];
        }
        referrencepoint = temp;
        double tempX = 0, tempY = 0, tempZ = 0, tempGPSX = 0, tempGPSY = 0, tempGPSA = 0;
        for (int i = 0; i < referrenceDatas.Length; i++)
        {
            tempX += referrenceDatas[i].transform.position.x - temp.transform.position.x;
            tempY += referrenceDatas[i].transform.position.y - temp.transform.position.y;
            tempZ += referrenceDatas[i].transform.position.z - temp.transform.position.z;
            tempGPSX += referrenceDatas[i].GPSrefx - temp.GPSrefx;
            tempGPSY += referrenceDatas[i].GPSrefy - temp.GPSrefy;
            tempGPSA += referrenceDatas[i].attitude - temp.attitude;

        }
        refx = tempX / tempGPSY;
        refy = tempZ / tempGPSX;
        refz = tempY / tempGPSA;
    }

    void GPSLocation()
    {
        LocatePlayer(TLS.latitude, TLS.longitude, TLS.altitude);
    }

    public void PickUpObject(GameObject item)
    {
        item.SetActive(false);
        PlayerData.instance.woodNumber += 1;
    }

    void LoadData()
    {
        string filePath = Application.dataPath;

        string fileName = "/itemData.csv";
        string tmp = filePath + fileName;
        //Check Txt Path exist or not
        FileInfo fileInfo = new FileInfo(tmp);
        if (fileInfo.Exists)
        {
            Read(tmp);
        }
    }

    void Read(string path)
    {
        GameObject temp = Resources.Load("prefabs/Wood") as GameObject;
        StreamReader reader = new StreamReader(path);
        string[] DATA = reader.ReadToEnd().Split(',');
        if (DATA.Length > 3)
            for (int i = 3; i < DATA.Length; i += 3)
            {
                print(DATA[i] + " " + DATA[i + 1] + " " + DATA[i + 2]);
                GameObject Item = Instantiate(temp);
                Item.transform.parent = SpawnPoint;
                Item.transform.position = ItemLocation(float.Parse(DATA[i + 1]), float.Parse(DATA[i + 2]));
                Item.transform.localPosition -= new Vector3(0, 0, Item.transform.localPosition.z);
                Item.transform.localScale = temp.transform.localScale;
                Item.GetComponent<Button>().onClick.AddListener(() => PickUpObject(Item));
            }
        reader.Close();
    }

    Vector3 ItemLocation(float x, float y)
    {
        double Refx = y - referrencepoint.GPSrefy;
        double Refy = x - referrencepoint.GPSrefx;
        double posx = referrencepoint.transform.position.x + refx * Refx;
        double posy = referrencepoint.transform.position.z + refy * Refy;
        return (new Vector3((float)posx, 0, (float)posy));
    }
    void Write(StreamWriter w, string item, string data)
    {
        w.WriteLine(item);
        w.WriteLine(data);
        w.WriteLine();
    }
}

