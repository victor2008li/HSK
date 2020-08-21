using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class HomeItemMOving : MonoBehaviour
{
    public GameObject selectedItem;
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
                if (!selectedItem && hit.transform.tag == "PickItems")
                {
                    //selectedItem = hit.transform.parent.gameObject;
                    selectedItem = hit.transform.gameObject;
                   // Camera.main.GetComponent<Functionalities>().enabled = false;
                }
            }

        }
        if (selectedItem && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.name == "floor")
                {

                    selectedItem.transform.parent.position = new Vector3(hit.point.x, selectedItem.transform.parent.position.y, hit.point.z);
                    // selectedItem.transform.position += new Vector3(0, 0, -selectedItem.transform.position.z + hit.point.z);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.transform.name);
        /*if (other.transform.name == "Cube")
        {

            GetComponent<MeshCollider>().enabled = false;
            selectedItem.transform.position = other.transform.parent.position;
            selectedItem = null;
            Camera.main.GetComponent<Functionalities>().enabled = true;
            other.transform.parent.gameObject.SetActive(false);
        }*/

    }
}
