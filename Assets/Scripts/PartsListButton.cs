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
        handMenu.selectedBox.GetComponent<BoxTagInformation>().partName = partName;
        handMenu.selectedBox.GetComponent<BoxTagInformation>().partReference = partReference;
        handMenu.selectedBox.GetComponent<BoxTagInformation>().partLocation = partLocation;
        handMenu.selectedBox.GetComponent<BoxTagInformation>().tagSet = true;

        repo.Add(handMenu.selectedBox.name, partName, partReference, partLocation);

        handMenu.selectedBox.GetComponent<BoxTagInformation>().UpdateText();

        handMenu.EndSetInformation();
        //partsListMenu.SetActive(false);
    }
}
