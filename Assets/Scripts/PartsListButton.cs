using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsListButton : MonoBehaviour
{
    public string partName;
    public string partReference;
    public string partLocation;

    public GameObject partsListMenu;
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
        GameObject selected = partsListMenu.GetComponent<PartsListMenu>().selectedGo;
        
        selected.GetComponent<BoxTagInformation>().partName = partName;
        selected.GetComponent<BoxTagInformation>().partReference = partReference;
        selected.GetComponent<BoxTagInformation>().partLocation = partLocation;

        GameObject.Find("Manager").GetComponent<BoxInformationRepo>().Add(selected.name, partName, partReference, partLocation);

        partsListMenu.GetComponent<PartsListMenu>().selectedGo.GetComponent<BoxTagInformation>().UpdateText();
        partsListMenu.SetActive(false);
    }
}
