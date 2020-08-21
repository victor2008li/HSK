using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePage : MonoBehaviour
{
    public GameObject[] OpenPage;
    public GameObject[] ClosePage;
    public bool closed = false;
    [SerializeField] Button saveButton;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            gameObject.SetActive(closed);
            if (OpenPage.Length > 0)
            {
                foreach (GameObject g in OpenPage)
                {
                    g.SetActive(true);
                    if (saveButton)
                        saveButton.gameObject.SetActive(true);
                }
            }


            if (ClosePage.Length > 0)
            {
                foreach (GameObject g in ClosePage)
                {
                    g.SetActive(false);

                    //saveButton.gameObject.SetActive(false);
                }

            }

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
