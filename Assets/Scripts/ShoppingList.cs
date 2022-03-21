using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public string[] list1;
    public string[] list2;
    public string[] list3;

    public Material greenMat;
    public Material transparentBlack;

    public bool visualizationMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableListItems(string list)
	{
        string[] selectedList;
		switch (list)
		{
            case "list1":
                selectedList = list1;
                break;
            case "list2":
                selectedList = list2;
                break;
            case "list3":
                selectedList = list3;
                break;
            default:
                selectedList = list1;
                break;
        }

        foreach(string part in selectedList)
		{
            string boxName = gameObject.GetComponent<BoxInformationRepo>().GetByReference(part);
            foreach (GameObject b in gameObject.GetComponent<BoxInformationRepo>().boxesList)
            {
                if (b.name == boxName)
				{
                    b.GetComponent<Renderer>().material = greenMat;
				}
				else
				{
                    b.GetComponent<Renderer>().material = transparentBlack;
                }
                b.SetActive(true);
            }
        }
    }

    public void LoadList()
	{

	}
}
