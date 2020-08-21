using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinChange : MonoBehaviour
{
    int coin = 0;
    bool update = false;
    int updateCoin = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (update)
            GetComponent<Text>().text = updateCoin.ToString();
        coin = int.Parse(GetComponent<Text>().text);
        update = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!update)
            return;
        float newcoin = (updateCoin - coin) * Time.deltaTime + int.Parse(GetComponent<Text>().text);
        GetComponent<Text>().text = ((int)newcoin).ToString();


        if ((updateCoin > coin && int.Parse(GetComponent<Text>().text) >= updateCoin) || (updateCoin < coin && int.Parse(GetComponent<Text>().text) <= updateCoin))
        {
            GetComponent<Text>().text = updateCoin.ToString();
            update = false;
            coin = updateCoin;
        }

    }

    public void ChangeValue(int num)
    {
        updateCoin = num;
        update = true;
    }
}
