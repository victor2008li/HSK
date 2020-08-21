using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;


public class database : MonoBehaviour
{
    private const string MONGO_URI = "mongodb://admin:victor2008li@ds012889.mlab.com:12889/euclideonhk";
    private const string DATABASE_NAME = "Items";
    private MongoClient client;
    private IMongoDatabase db;

    
    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        print(db+" ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
