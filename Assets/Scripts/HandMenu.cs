using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;

public class HandMenu : MonoBehaviour
{
    public GameObject manager;
    public BoxInformationRepo repo;
    public GameObject mainMenu;
    public GameObject configurationMenu;
    public GameObject partsListMenu;
    public GameObject selectedBox;
    public Material transparentBlueMat;
    public Material transparentBlackMat;

    private GameObject selectedMenu;
    private bool handActivated;

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

    public void HandActive(bool flag)
	{
        handActivated = flag;
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
        partsListMenu.SetActive(false);
    }

    public void StartConfiguration()
    {
        foreach (GameObject b in repo.boxesList)
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

    public void StartSetInformation()
    {
        selectedMenu = partsListMenu;
		if (handActivated)
		{
            EnableMenu();
        }
    }

    public void EndSetInformation(bool confDone)
    {
        selectedMenu = configurationMenu;
		if (confDone || (!confDone && selectedBox.GetComponent<BoxTagInformation>().tagSet))
		{
            selectedBox.GetComponent<Renderer>().material = transparentBlueMat;
        }
        else
		{
            selectedBox.GetComponent<Renderer>().material = transparentBlackMat;
        }
        selectedBox.GetComponent<ObjectManipulator>().ManipulationType = 
            Microsoft.MixedReality.Toolkit.Utilities.ManipulationHandFlags.OneHanded | 
            Microsoft.MixedReality.Toolkit.Utilities.ManipulationHandFlags.TwoHanded;

        setInformationFlag = false;
        
        EnableMenu();
    }

    public void HideAllBoxes()
    {
        foreach (GameObject b in repo.boxesList)
        {
            b.SetActive(false);
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

    public void DisableObjectsManipulation()
	{
        foreach (GameObject b in repo.boxesList)
        {
            b.GetComponent<ObjectManipulator>().ManipulationType = 0;
        }
    }
}
