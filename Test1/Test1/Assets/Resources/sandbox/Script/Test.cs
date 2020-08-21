using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    Renderer render = null;
    Material tempMaterial = null;
    public static Test script;
    public enum colliderStatus { canPut, CannotPut, canPutSurface };
    public colliderStatus status;
    // Start is called before the first frame update
    void Start()
    {
        status = colliderStatus.canPut;
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "floor") { return; }
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
        if (status == colliderStatus.CannotPut)
        {
            List<Renderer> renderer;
            renderer = other.gameObject.GetComponentsInChildren<Renderer>().ToList();
            foreach (Renderer renders in renderer)
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
}
