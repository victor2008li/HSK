using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffSwitchButton : MonoBehaviour
{
    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (items.Length > 0)
                foreach (GameObject g in items)
                    g.SetActive(!g.activeInHierarchy);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }


}
