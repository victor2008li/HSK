using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChooseItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { GetComponentInParent<PurchaseItem>().PurchaseConfirm(gameObject); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
