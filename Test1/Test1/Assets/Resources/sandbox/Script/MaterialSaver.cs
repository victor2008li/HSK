using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialSaver : MonoBehaviour
{
    List<Renderer> rendererList;
    Material[] material;

    private void Awake()
    {
        rendererList = transform.GetComponentsInChildren<Renderer>().ToList();
    }

    void Start()
    {
        GetRendererMaterial();
    }

    private void GetRendererMaterial()
    {
        material = new Material[rendererList.Count];
        for (int i = 0; i < material.Length; i++)
        {
            material[i] = rendererList[i].material;
        }
    }


    public void Restore()
    {
        foreach(Renderer rend in rendererList)
        {
            foreach(Material mats in material)
            {
                rend.material = mats;
            }
        }
    }
}
