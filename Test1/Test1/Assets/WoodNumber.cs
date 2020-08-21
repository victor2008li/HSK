using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GetComponent<RawImage>().enabled = PlayerData.instance.woodNumber == 0 ? false : true;
        GetComponentInChildren<Text>(true).enabled = GetComponent<RawImage>().enabled;
        GetComponentInChildren<Text>(true).text = "x " + PlayerData.instance.woodNumber.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
