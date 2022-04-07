using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class PartsListButton : MonoBehaviour
{
    public string partName;
    public string partReference;
    public string partLocation;

    public BoxInformationRepo repo;
    public HandMenu handMenu;
    // Start is called before the first frame update
    void Start()
    {
        repo = GameObject.Find("Manager").GetComponent<BoxInformationRepo>();
        handMenu = GameObject.Find("HandMenu").GetComponent<HandMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoxInformation()
	{
        handMenu.selectedBox.GetComponent<BoxTagInformation>().UpdateInfo(partName, partReference, partLocation);
        handMenu.selectedBox.GetComponent<BoxTagInformation>().tagSet = true;

        repo.AddInfo(handMenu.selectedBox.name, partName, partReference, partLocation);

        handMenu.EndSetInformation(true);
    }

    public void RemoveBoxInformation()
	{
        handMenu.selectedBox.GetComponent<BoxTagInformation>().UpdateInfo("", "", "");
        handMenu.selectedBox.GetComponent<BoxTagInformation>().tagSet = false;

        repo.RemoveInfo(handMenu.selectedBox.name);

        handMenu.EndSetInformation(false);
    }

    public void UpdateButtonInfo(string partName, string partRef, string partLocation)
	{
        this.partName = partName;
        this.partReference = partRef;
        this.partLocation = partLocation;

        gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = partRef + "\n" + partName + "\n" + partLocation;
    }
}
