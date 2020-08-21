using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int coin = 10000;
    public Text[] coinsText;
    public float woodNumber = 0;
    public bool chair = false;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        foreach (Text t in coinsText)
            t.text = coin.ToString();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F2))
            CoinChange(200);
    }

    public void CoinChange(int num)
    {
        coin += num;
        foreach (Text t in coinsText)
            t.GetComponent<CoinChange>().ChangeValue(coin);
    }

    public void BuyChair()
    {
        woodNumber -= 3;
        chair = true;
    }
}
