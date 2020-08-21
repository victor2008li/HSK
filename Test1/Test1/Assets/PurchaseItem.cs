using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    public Text t;
    string orginal_Text;
    public GameObject ConfirmMsg;
    public GameObject purchaseItem;
    // Start is called before the first frame update
    void Start()
    {
        if (orginal_Text == null)
            orginal_Text = t.text;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PurchaseConfirm(GameObject Item)
    {
        purchaseItem = Item;
        t.text = orginal_Text + purchaseItem.transform.GetChild(0).GetComponent<Text>().text;
        ConfirmMsg.SetActive(true);
    }

    public void purchase()
    {

        string price_s = purchaseItem.transform.GetChild(1).GetComponent<Text>().text;
        if (price_s.Contains("Price"))
        {
            int price = int.Parse(price_s.Substring(7, price_s.Length - 7));
            PlayerData.instance.CoinChange(-price);
            ConfirmMsg.SetActive(false);
        }
        else
        {
            if (PlayerData.instance.woodNumber >= 3)
            {
                PlayerData.instance.BuyChair();
                ConfirmMsg.SetActive(false);
            }
            else
            {
                t.text = "Not enough Woods for Chair";
            }
        }
    }
}
