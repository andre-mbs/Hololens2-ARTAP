using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public BoxInformationRepo repo;

    public string[] list1;
    public string[] list2;
    public string[] list3;

    public Material greenMat;
    public Material transparentBlackMat;

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
        foreach (GameObject b in repo.boxesList)
        {
            b.GetComponent<Renderer>().material = transparentBlackMat;
        }
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
            string boxName = repo.GetByReference(part);
            foreach (GameObject b in repo.boxesList)
            {
                if (b.name == boxName)
				{
                    b.GetComponent<Renderer>().material = greenMat;
				}
                b.SetActive(true);
            }
        }
    }

    public void LoadList()
	{

	}
}
