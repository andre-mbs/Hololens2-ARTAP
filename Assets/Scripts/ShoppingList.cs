using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public BoxInformationRepo repo;
    public HandMenu handMenu;

    public List<string> partsListDefault;
    public List<string> partsList1;
    public List<string> partsList2;
    public List<string> partsList3;

    private List<string> selectedList;

    public string boxToPickRef;
    public string lastBoxToPickRef;
    private int boxToPickIndex;

    public Material greenMat;
    public Material transparentBlackMat;

    public TextAsset partsFileDefault;
    public TextAsset partsFileList1;
    public TextAsset partsFileList2;
    public TextAsset partsFileList3;

    public UserTestsLog userTestsLog;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Read File");
        LoadLists();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableListItems(int index)
	{
        foreach (GameObject b in repo.boxesList)
        {
            b.GetComponent<Renderer>().material = transparentBlackMat;
        }

        switch (index)
        {
            case 1:
                selectedList = partsList1;
                break;
            case 2:
                selectedList = partsList2;
                break;
            case 3:
                selectedList = partsList3;
                break;
            default:
                selectedList = partsListDefault;
                break;
        }

        lastBoxToPickRef = selectedList[selectedList.Count-1];
        if (handMenu.seqPickMode)
		{
            foreach (GameObject b in repo.boxesList)
            {
                if (b.GetComponent<BoxTagInformation>().partReference == selectedList[0])
                {
                    b.GetComponent<Renderer>().material = greenMat;
                    boxToPickIndex = 0;
                    boxToPickRef = selectedList[0];
                }
                b.SetActive(true);
            }
		}
		else
		{
            foreach (string part in selectedList)
		    {
			    //string boxName = repo.GetByReference(part);
			    foreach (GameObject b in repo.boxesList)
			    {
				    if (b.GetComponent<BoxTagInformation>().partReference == part)
				    {
					    b.GetComponent<Renderer>().material = greenMat;
				    }
				    b.SetActive(true);
			    }
		    }
		}

        userTestsLog.StartTimer(true);
    }

	public void LoadLists()
	{
        string[] lines = partsFileDefault.text.Split('\n');

        foreach (string line in lines)
        {
            partsListDefault.Add(line.Split(';')[0]);
        }

        lines = partsFileList1.text.Split('\n');
        foreach (string line in lines)
		{
            partsList1.Add(line.Split(';')[0]);
        }

        lines = partsFileList2.text.Split('\n');
        foreach (string line in lines)
        {
            partsList2.Add(line.Split(';')[0]);
        }

        lines = partsFileList3.text.Split('\n');
        foreach (string line in lines)
        {
            partsList3.Add(line.Split(';')[0]);
        }
    }

    public void UpdateNextBoxRef()
	{
        boxToPickIndex++;
        boxToPickRef = selectedList[boxToPickIndex];

        foreach (GameObject b in repo.boxesList)
        {
            if (b.GetComponent<BoxTagInformation>().partReference == boxToPickRef)
            {
                b.GetComponent<Renderer>().material = greenMat;
            }
            b.SetActive(true);
        }
    }
}
