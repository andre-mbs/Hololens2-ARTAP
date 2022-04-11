using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public BoxInformationRepo repo;

    public List<string> partsListDefault;
    public List<string> partsList1;
    public List<string> partsList2;

    private List<string> selectedList;

    public string boxToPickRef;
    private int boxToPickIndex;

    public Material greenMat;
    public Material transparentBlackMat;

    public TextAsset partsFileDefault;
    public TextAsset partsFileList1;
    public TextAsset partsFileList2;
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
            default:
                selectedList = partsListDefault;
                break;
        }

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

        //foreach (string part in selectedList)
        //{
        //    //string boxName = repo.GetByReference(part);
        //    foreach (GameObject b in repo.boxesList)
        //    {
        //        if (b.GetComponent<BoxTagInformation>().partReference == part)
        //        {
        //            b.GetComponent<Renderer>().material = greenMat;
        //        }
        //        b.SetActive(true);
        //    }
        //}
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
