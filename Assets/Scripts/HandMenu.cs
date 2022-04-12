using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class HandMenu : MonoBehaviour
{
    public GameObject manager;
    public BoxInformationRepo repo;

    public GameObject mainMenu;
    public GameObject configurationMenu;
    public GameObject visualizationMenu;
    public GameObject partsListMenu;
    public GameObject infoPanel;
    public GameObject qrMenu;

    public GameObject selectedBox;
    public Material transparentBlueMat;
    public Material transparentBlackMat;

    private GameObject selectedMenu;
    private bool handActivated;

    public bool removeBoxFlag;
    public bool showPartsListFlag;
    public bool setInformationFlag;
    public bool configurationMode;
    public bool visualizationMode;
    public bool userTestsMode;
    public bool seqPickMode;

    public TextAsset partsListDefaultFile;
    public TextAsset partsListUserTestFile;

    private List<GameObject> partsListButtonsDefault;
    private List<GameObject> partsListButtonsUserTest;

    public GameObject partsListButtonPrefab;

    public MyQRCodeManager myQRCodeManager;

    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        selectedMenu = mainMenu;

        partsListButtonsDefault = new List<GameObject>();
        partsListButtonsUserTest = new List<GameObject>();

        LoadPartsLists();
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
        visualizationMode = false;
        configurationMode = false;

        manager.GetComponent<Anchors>().isStoreLoaded = true;
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
        visualizationMenu.SetActive(false);
        partsListMenu.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void StartConfiguration()
    {
        repo.UpdateSceneBoxesList();
        foreach (GameObject b in repo.boxesList)
        {
            b.GetComponent<MoveAxisConstraint>().enabled = false;
            b.GetComponent<RotationAxisConstraint>().enabled = false;
            b.GetComponent<ObjectManipulator>().AllowFarManipulation = true;
            if (b.GetComponent<BoxTagInformation>().tagSet)
			{
                b.GetComponent<Renderer>().material = transparentBlueMat;
			}
			else
			{
                b.GetComponent<Renderer>().material = transparentBlackMat;
            }
            b.SetActive(true);
        }

        selectedMenu = configurationMenu;
        configurationMode = true;
        EnableMenu();
    }

    public void StartVisualization()
    {
        repo.UpdateSceneBoxesList();
        foreach (GameObject b in repo.boxesList)
        {
            b.GetComponent<MoveAxisConstraint>().enabled = true;
            b.GetComponent<RotationAxisConstraint>().enabled = true;
            b.GetComponent<ObjectManipulator>().AllowFarManipulation = false;
            b.GetComponent<Renderer>().material = transparentBlackMat;
        }

        selectedMenu = visualizationMenu;
        visualizationMode = true;
        EnableMenu();
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
        if(selectedBox != null)
		{
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
        }
        setInformationFlag = false;

        SetMenu(configurationMenu);
    }

    public void ShowInfoPanel(string panel)
	{
        if(string.Equals(panel, "remove"))
		{
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "Remove Box";
            infoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "Select the box you want to remove";
        }else if(string.Equals(panel, "setBoxInfo"))
		{
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "Set Box Information";
            infoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "Select the box you want to set the tag information";
        }
        else if (string.Equals(panel, "qrcode_wait"))
        {
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "QRCode Detection";
            infoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "Get closer to a QR Code tag";
            //infoPanel.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (string.Equals(panel, "qrcode_detected"))
        {
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "QRCode Detection";
            infoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "Tag detected: " + myQRCodeManager.CheckNearQR();
            //infoPanel.transform.GetChild(2).gameObject.SetActive(true);
        }

        selectedMenu = infoPanel;

        if (handActivated)
        {
            EnableMenu();
        }
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
        ShowInfoPanel("remove");
    }
    public void SetShowPartsListFlag()
    {
        showPartsListFlag = true;
    }

    public void SetInformationFlag()
	{
        setInformationFlag = true;
        ShowInfoPanel("setBoxInfo");

    }

    public void StartQrDetection()
    {
        //startQrDetection = true;
        ShowInfoPanel("qrcode_wait");
        myQRCodeManager.StartScan();
    }

    public void EndQrDetection()
    {
        //startQrDetection = false;
        ShowInfoPanel("qrcode_detected");
    }

    public void ShowQrMenu(Pose pose)
	{
        GameObject qrMenuParent = qrMenu.transform.parent.gameObject;
        qrMenuParent.transform.position = pose.position;

        //Vector3 newRot = new Vector3(qrMenuParent.transform.rotation.x, qrMenuParent.transform.rotation.y + 180, qrMenuParent.transform.rotation.z);
        //qrMenuParent.transform.rotation = Quaternion.Euler(newRot);
        //qrMenuParent.transform.LookAt(head.transform);
        //or3 newRot = new Vector3(head.transform.rotation.x, pose.rotation.y, head.transform.rotation.z);

        //qrMenuParent.transform.rotation = pose.rotation;
        qrMenuParent.transform.LookAt(head.transform, Vector3.down);

        qrMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = myQRCodeManager.CheckNearQR();

        qrMenuParent.SetActive(true);
    }

    public void AddBoxFromQR()
    {
        manager.GetComponent<SpawnBox>().Spawn();
        GameObject spawnedBox = manager.GetComponent<SpawnBox>().lastSpawnedBox;

        string qrString = myQRCodeManager.CheckNearQR();
        string[] qrData = qrString.Replace(" ", "").Split('/');
        Pose qrPose;
        myQRCodeManager.qrCodeList.TryGetValue(qrString, out qrPose);

        spawnedBox.transform.position = qrPose.position;
        spawnedBox.GetComponent<BoxTagInformation>().UpdateInfo("", qrData[0], qrData[1]);
        spawnedBox.GetComponent<BoxTagInformation>().tagSet = true;
        repo.AddInfo(spawnedBox.name, "", qrData[0], qrData[1]);

        spawnedBox.GetComponent<Renderer>().material = transparentBlueMat;

        qrMenu.transform.parent.gameObject.SetActive(false);
        EndSetInformation(false);
    }

    public void SetMenu(GameObject menu)
	{
        selectedMenu = menu;
		if (handActivated)
		{
            EnableMenu();
		}
	}

    public void DisableObjectsManipulation()
	{
        foreach (GameObject b in repo.boxesList)
        {
            b.GetComponent<ObjectManipulator>().ManipulationType = 0;
        }
    }

    public void ToggleCheckBoxState()
	{
        userTestsMode = !userTestsMode;

		if (userTestsMode)
		{
            foreach(GameObject btn in partsListButtonsDefault)
			{
                btn.SetActive(false);
			}
            foreach (GameObject btn in partsListButtonsUserTest)
            {
                btn.SetActive(true);
            }
        }else{
            foreach (GameObject btn in partsListButtonsDefault)
            {
                btn.SetActive(true);
            }
            foreach (GameObject btn in partsListButtonsUserTest)
            {
                btn.SetActive(false);
            }
        }
        partsListMenu.transform.GetChild(0).gameObject.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    public void ToggleSequencialPicking()
	{
        seqPickMode = !seqPickMode;
	}

    public void LoadPartsLists()
	{
        string[] lines = partsListDefaultFile.text.Split('\n');
        foreach (string line in lines)
        {
            string[] fields = line.Split(';');

            GameObject spawnedButton = Instantiate(partsListButtonPrefab, partsListMenu.transform.GetChild(0));
            spawnedButton.GetComponent<PartsListButton>().UpdateButtonInfo(fields[1], fields[0], fields[2]);
            partsListButtonsDefault.Add(spawnedButton);
        }

        lines = partsListUserTestFile.text.Split('\n');
        foreach (string line in lines)
        {
            string[] fields = line.Split(';');

            GameObject spawnedButton = Instantiate(partsListButtonPrefab, partsListMenu.transform.GetChild(0));
            spawnedButton.SetActive(false);
            spawnedButton.GetComponent<PartsListButton>().UpdateButtonInfo(fields[1], fields[0], fields[2]);
            partsListButtonsUserTest.Add(spawnedButton);
        }

        partsListMenu.transform.GetChild(0).gameObject.GetComponent<GridObjectCollection>().UpdateCollection();
    }
}
