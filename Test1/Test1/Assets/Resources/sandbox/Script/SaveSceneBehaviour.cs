using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveSceneBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MoveItemBehaviour roomBehaviour;
    [SerializeField] List<GameObject> moveAbleObject;
    [SerializeField] Text textMessage;
    [SerializeField] GameObject DownBtn, RotateBtn;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            roomBehaviour = MoveItemBehaviour.script;
            moveAbleObject = GameObject.FindGameObjectsWithTag("MoveAble").ToList();
            SetMessage();

        });
    }

    private void SetMessage()
    {
        bool canSave = true;
        textMessage.gameObject.SetActive(true);
        if (roomBehaviour)
        {
            if (roomBehaviour.status != MoveItemBehaviour.colliderStatus.canPut)
            {
                canSave = false;
            }
        }
        foreach(GameObject go in moveAbleObject)
        {
            if (go.GetComponent<Test>().status == Test.colliderStatus.CannotPut)
                canSave = false;
        }
        if (canSave)
        {
            textMessage.text = "Room settings saved";
            textMessage.color = Color.green;
            roomBehaviour.saveItems();
            roomBehaviour.gameObject.GetComponent<MoveItemBehaviour>().enabled = false;
            GameObject saveBtn = GameObject.Find("Save_Btn");
            GameObject.Find("GameManager").GetComponent<EditSceneItem>().disableSelectedItem();
            saveBtn.SetActive(false);
            DownBtn.SetActive(false);
            RotateBtn.SetActive(false);
        }
        else
        {
            textMessage.text = "Cannot Saved: Obstructed by objects"; ;
            textMessage.color = Color.red;
        }
        Invoke("DisableMessage", 2.5f);
    }

    void DisableMessage()
    {
        textMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
