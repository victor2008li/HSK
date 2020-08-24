using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditSceneItem : MonoBehaviour
{
    bool canEdit = false;
    [SerializeField] Button btn;
    List<GameObject> moveAbleObject;
    [SerializeField] GameObject selectedItem;
    Renderer render = null;
    Material tempMaterial = null;
    [SerializeField] Button saveBtn;
    [SerializeField] EditSceneItem script;
    [SerializeField] Button RotateBtn;
    GameObject temp;
    // Start is called before the first frame update
    public enum items { Null, sofa, table, tv };
    items currentItem;

    private void Awake()
    {
        currentItem = items.Null;
    }
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            script = this;
            moveAbleObject = GameObject.FindGameObjectsWithTag("MoveAble").ToList();
            canEdit = true;
            saveBtn.gameObject.SetActive(true);
        });
        RotateBtn.onClick.AddListener(() =>
        {
            if (selectedItem)
                selectedItem.transform.Rotate(Vector3.up * 90);
        });
    }

    // Update is called once per frame
    void Update()
    {
        SwitchItem();
        if (selectedItem && Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (Input.GetKeyUp(KeyCode.Space)){
                    selectedItem.transform.Rotate(Vector3.up * 90);
                }
                if (currentItem == items.sofa || currentItem == items.table)
                {
                    selectedItem.transform.position = new Vector3(hit.point.x, selectedItem.transform.position.y, hit.point.z);
                }
                else if(currentItem == items.tv)
                {
                    selectedItem.transform.position = new Vector3(selectedItem.transform.position.x, selectedItem.transform.position.y, hit.point.z);
                }
            }

        }
        if (selectedItem)
        {
            SetOutlineShader();
            RotateBtn.gameObject.SetActive(true);
        }
    }

    void DisableSofaShader()
    {
        List<Renderer> renderer = GameObject.Find("Sofa").GetComponentsInChildren<Renderer>().ToList();
        Shader shader = Shader.Find("Standard");
        foreach (Renderer renders in renderer)
        {
            renders.material.shader = shader;
        }
    }

    void DisableTableShader()
    {
        List<Renderer> renderer = GameObject.Find("Table").GetComponentsInChildren<Renderer>().ToList();
        Shader shader = Shader.Find("Standard");
        foreach (Renderer renders in renderer)
        {
            renders.material.shader = shader;
        }
    }

    void DisableTelevisionShader()
    {
        List<Renderer> renderer = GameObject.Find("Television").GetComponentsInChildren<Renderer>().ToList();
        Shader shader = Shader.Find("Standard");
        foreach (Renderer renders in renderer)
        {
            renders.material.shader = shader;
        }
    }

    void DisableAllShader()
    {
        DisableTableShader();
        DisableSofaShader();
        DisableTelevisionShader();
    }

    private void SetOutlineShader()
    {
        List<Renderer> renderer = selectedItem.gameObject.GetComponentsInChildren<Renderer>().ToList();
        Shader shader = Shader.Find("Outlined/Regular");
        foreach (Renderer renders in renderer)
        {
            renders.material.shader = shader;
        }
    }

    public void disableSelectedItem()
    {
        selectedItem = null;
        canEdit = false;
    }

    private void SwitchItem()
    {
        if (canEdit)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (selectedItem && hit.transform.tag == "PickItems")
                    {
                        selectedItem = null;
                        DisableAllShader();
                    }
                    if (hit.transform.tag == "MoveAble")
                    {
                        temp = selectedItem;
                        switch (hit.transform.gameObject.name)
                        {
                            case "Sofa":
                                currentItem = items.sofa;
                                selectedItem = moveAbleObject[0];
                                temp = moveAbleObject[0];
                                DisableTableShader();
                                DisableTelevisionShader();
                                break;
                            case "Table":
                                currentItem = items.table;
                                temp = moveAbleObject[1];
                                selectedItem = moveAbleObject[1];
                                DisableSofaShader();
                                DisableTelevisionShader();
                                break;
                            case "Television":
                                currentItem = items.tv;
                                temp = moveAbleObject[2];
                                DisableSofaShader();
                                DisableTableShader();
                                selectedItem = moveAbleObject[2];
                                break;
                        }
                    }
                }

            }
        }
    }

}
