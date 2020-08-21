using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTest : MonoBehaviour
{
    public GameObject notice;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {

                if (hit.transform.tag == "PickItems") { 
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                    notice.SetActive(true);
                    notice.GetComponentInChildren<Text>().text = "You if pick up a " + hit.transform.name;
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
