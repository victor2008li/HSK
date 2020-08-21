using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoveItemBehaviour : MonoBehaviour
{
    public GameObject selectedItem;
    // Start is called before the first frame update
    public static MoveItemBehaviour script;
    [SerializeField] Button btn;
    public enum colliderStatus { canPut, CannotPut, canPutSurface};
    public enum itemSurface { Null, KitchenTable, Table};
    Renderer render = null;
    Material tempMaterial = null;
    public colliderStatus status;
    public itemSurface onSurface;
    GameObject temp;

    private void Awake()
    {
        if (script == null)
        {
            script = this;
            status = colliderStatus.canPut;
            onSurface = itemSurface.Null;
        }
    }

    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            CheckSurface();
        });
    }

    private void CheckSurface()
    {
        if (onSurface == itemSurface.KitchenTable)
        {
            selectedItem.transform.parent.position = new Vector3(7.939f, 9.053f, -3.881f);
            onSurface = itemSurface.Null;
            btn.gameObject.SetActive(false);
        }
        else if (onSurface == itemSurface.Table)
        {
            selectedItem.transform.parent.position = new Vector3(8.436f, 9.053f, -1.641f);
            onSurface = itemSurface.Null;
            btn.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (!selectedItem && hit.transform.tag == "PickItems")
                {
                    selectedItem = hit.transform.gameObject;
                    temp = hit.transform.gameObject;
                  //  Camera.main.GetComponent<Functionalities>().enabled = false;
                }
                if (selectedItem && hit.transform.tag == "MoveAble")
                    selectedItem = null;
            }

        }
        if (selectedItem && Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //  if (hit.collider.name == "floor")
                //{
                /* if (Input.GetMouseButton(0))
                 {
                     selectedItem.transform.parent.rotation = Camera.main.transform.localRotation;
                 }*/
                if (onSurface == itemSurface.KitchenTable)
                {
                    if(hit.point.x > 8 || hit.point.x < 7)
                    {
                        return;
                    }
                    selectedItem.transform.parent.position = new Vector3(hit.point.x, selectedItem.transform.parent.position.y, selectedItem.transform.parent.position.z);
                    
                }else if(onSurface == itemSurface.Table)
                {
                    if(hit.point.z < -2.4 || hit.point.z > -0.3)
                    {
                        return;
                    }
                    selectedItem.transform.parent.position = new Vector3(selectedItem.transform.parent.position.x, selectedItem.transform.parent.position.y, hit.point.z);
                }
                else
                {
                    selectedItem.transform.parent.position = new Vector3(hit.point.x, selectedItem.transform.parent.position.y, hit.point.z);
                }
            }

        }
    }

    public void saveItems()
    {
        if (temp)
        {
            temp.transform.parent.transform.parent = GameObject.Find("part7 living room").transform;
            temp.transform.parent.tag = "MoveAble";
            temp.transform.tag = "MoveAble";
            temp = null;
        }
        else
        {
            selectedItem.transform.parent.transform.parent = GameObject.Find("part7 living room").transform;
            selectedItem.transform.parent.tag = "MoveAble";
            selectedItem.transform.tag = "MoveAble";
            selectedItem = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "floor")
        {
            return;
        }
        if(other.gameObject.name=="Kitchen Table")
        {
            selectedItem.transform.parent.position = new Vector3(7.373f, 10.021f, -3.02f);
            onSurface = itemSurface.KitchenTable;
            btn.gameObject.SetActive(true);
        }else if(other.gameObject.name == "Table")
        {
            selectedItem.transform.parent.position = new Vector3(9.756f, 9.437f, -2.223f);
            onSurface = itemSurface.Table;
            btn.gameObject.SetActive(true);
        }
        status = colliderStatus.CannotPut;
        ChangeMaterialColorByCollider(other);

    }

    private void OnTriggerExit(Collider other)
    {
        RestoreMaterial(other);
    }

    private void RestoreMaterial(Collider other)
    {
        status = colliderStatus.canPut;
        other.gameObject.GetComponent<MaterialSaver>().Restore();
    }

    private void ChangeMaterialColorByCollider(Collider other)
    {
        if(status== colliderStatus.CannotPut)
        {
            List<Renderer> renderer;
            renderer = other.gameObject.GetComponentsInChildren<Renderer>().ToList();
            foreach(Renderer renders in renderer)
            {
                renders.enabled = true;
                tempMaterial = renders.sharedMaterial;
                Material newMat = Resources.Load("sandbox/Red", typeof(Material)) as Material;
                renders.sharedMaterial = newMat;
            }
          /*  render.enabled = true;
            tempMaterial = render.sharedMaterial;
            Material newMat = Resources.Load("sandbox/Red", typeof(Material)) as Material;
            render.sharedMaterial = newMat;     */
        }

    }

    /*void switchToDefaultMaterial()
    {
        if(status== colliderStatus.canPut)
        {
            if(render && tempMaterial)
            {
                render.sharedMaterial = tempMaterial;
                render = null;
                tempMaterial = null;
            }
        }

    }*/

}
