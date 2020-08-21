using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBagManager : MonoBehaviour
{
    public GameObject desk;
    // Start is called before the first frame update
    void OnEnable()
    {
        desk.gameObject.SetActive(PlayerData.instance.chair);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pickDesk()
    {
        PlayerData.instance.chair = false;
    }
}
