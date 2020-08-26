using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject desk, bagPage;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            desk.SetActive(true);
            bagPage = GameObject.Find("MyBagPage");
            bagPage.SetActive(false);
            GetComponent<Button>().gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
