using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoxInformation()
	{
        handMenu.selectedBox.GetComponent<BoxTagInformation>().UpdateInfo(partName, partReference, partLocation);
        handMenu.selectedBox.GetComponent<BoxTagInformation>().tagSet = true;

        repo.Add(handMenu.selectedBox.name, partName, partReference, partLocation);

        handMenu.EndSetInformation(true);
    }

    public void RemoveBoxInformation()
	{
        handMenu.selectedBox.GetComponent<BoxTagInformation>().UpdateInfo("", "", "");
        handMenu.selectedBox.GetComponent<BoxTagInformation>().tagSet = false;

        repo.Remove(handMenu.selectedBox.name);

        handMenu.EndSetInformation(false);
    }
}
