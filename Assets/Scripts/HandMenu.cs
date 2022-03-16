using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenu : MonoBehaviour
{
    public GameObject manager;
    public GameObject mainMenu;
    public GameObject configurationMenu;
    private GameObject selectedMenu;

    public bool removeBoxFlag;
    public bool showPartsListFlag;
    public bool setInformationFlag;
    // Start is called before the first frame update
    void Start()
    {
        selectedMenu = mainMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMain()
	{
        HideAllBoxes();

        selectedMenu = mainMenu;
        EnableMenu();
    }

    public void EnableMenu()
	{
        DisableAll();
        selectedMenu.SetActive(true);
	}

    public void DisableAll()
	{
        mainMenu.SetActive(false);
        configurationMenu.SetActive(false);
    }

    public void StartConfiguration()
    {
        foreach (GameObject b in manager.GetComponent<SpawnBox>().boxesList)
        {
            b.SetActive(true);
        }

        selectedMenu = configurationMenu;
        EnableMenu();
    }

    public void StartVisualization()
    {
        configurationMenu.SetActive(false);

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("box");
        if (boxes.Length > 0)
        {
            foreach (GameObject b in boxes)
            {
                b.SetActive(false);
            }
        }

        manager.GetComponent<ShoppingList>().EnableListItems();
    }

    public void HideAllBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("box");
        if (boxes.Length > 0)
        {
            foreach (GameObject b in boxes)
            {
                b.SetActive(false);
            }
        }
    }
    public void SetRemoveFlag()
    {
        removeBoxFlag = true;
    }
    public void SetShowPartsListFlag()
    {
        showPartsListFlag = true;
    }

    public void SetInformationFlag()
    {
        setInformationFlag = true;
    }
}
