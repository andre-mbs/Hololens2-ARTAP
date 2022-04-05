using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public BoxInformationRepo repo;

    public string[] list1;
    public string[] list2;
    public string[] list3;

    public List<string> partsListDefault;
    public List<string> partsList1;
    public List<string> partsList2;

    public Material greenMat;
    public Material transparentBlackMat;

    public bool visualizationMode;

    public TextAsset partsFileDefault;
    public TextAsset partsFileList1;
    public TextAsset partsFileList2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Read File");
        LoadList();
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
        string[] lines = partsFileList1.text.Split('\n');
        foreach(string line in lines)
		{
            partsList1.Add(line.Split(';')[0]);
		}
        Debug.Log(lines[0]);
	}
}
