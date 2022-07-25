using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ShoppingList : MonoBehaviour
{
    public BoxInformationRepo repo;
    public HandMenu handMenu;

    public List<string> partsListDefault;
    public List<string> partsList1;
    public List<string> partsList2;
    public List<string> partsList3;
    public List<string> partsList4;

    public List<string> selectedList;

    public string boxToPickRef;
    public string lastBoxToPickRef;
    public int boxToPickIndex;
    public int numberPickedParts;

    public Material greenMat;
    public Material transparentBlackMat;

    public TextAsset partsFileDefault;
    public TextAsset partsFileList1;
    public TextAsset partsFileList2;
    public TextAsset partsFileList3;
    public TextAsset partsFileList4;

    public UserTestsLog userTestsLog;
    public GameObject topInfoBar;
    public GameObject topInfoBar2;

    public GameObject floatingTag;
    public GameObject floatingTagBlue;
    public GameObject glow;

    public List<GameObject> floatingTagsList;
    public GameObject lastTag;
    public List<GameObject> glowsList;

    public Arrows arrows; 
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
            case 4:
                selectedList = partsList4;
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
                    floatingTag.transform.parent.transform.position = new Vector3(b.transform.position.x, b.transform.position.y+0.25f, b.transform.position.z);
                    floatingTag.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partName;
                    floatingTag.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partReference;
                    floatingTag.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partLocation;
                    floatingTag.SetActive(true);
                    glow.transform.position = b.transform.position;
                    glow.SetActive(true);
                    arrows.nextBoxPosition = glow.transform.position;
                    arrows.arrowsEnabled = true;

                    boxToPickIndex = 0;
                    boxToPickRef = selectedList[0];
                }
                b.SetActive(true);
                b.GetComponent<MeshRenderer>().enabled = false;
            }
		}
		else
		{
            arrows.arrowsEnabled = false;
            foreach (string part in selectedList)
		    {
                //string boxName = repo.GetByReference(part);
                floatingTag.SetActive(false);
                glow.SetActive(false);
			    foreach (GameObject b in repo.boxesList)
			    {
				    if (b.GetComponent<BoxTagInformation>().partReference == part)
				    {
                        b.GetComponent<Renderer>().material = greenMat;

                        GameObject newFloatingTagRoot = Instantiate(floatingTag.transform.parent).gameObject;
                        floatingTagsList.Add(newFloatingTagRoot);
                        GameObject newGlow = Instantiate(glow);
                        newGlow.name = b.name;
                        glowsList.Add(newGlow);

                        newFloatingTagRoot.transform.position = new Vector3(b.transform.position.x, b.transform.position.y + 0.25f, b.transform.position.z);
                        newFloatingTagRoot.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partName;
                        newFloatingTagRoot.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partReference;
                        newFloatingTagRoot.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partLocation;
                        newFloatingTagRoot.transform.GetChild(0).gameObject.SetActive(true);

                        newGlow.transform.position = b.transform.position;
                        newGlow.SetActive(true);
                    }
                    b.SetActive(true);
                    b.GetComponent<MeshRenderer>().enabled = false;
                }
		    }
		}

        numberPickedParts = 0;
        //topInfoBar.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "Next piece: " + repo.GetNameByReference(boxToPickRef) + ", quantity remaining: 2";
        //topInfoBar.SetActive(true);
        userTestsLog.StartTimer(true);
        userTestsLog.SetKitName("KIT" + index);
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
        lines = partsFileList4.text.Split('\n');
        foreach (string line in lines)
        {
             partsList4.Add(line.Split(';')[0]);
        }

    }
    public void LoadLists2()
    {
        string[] lines = partsFileDefault.text.Split('\n');

        foreach (string line in lines)
        {
            partsListDefault.Add(line.Split(';')[0]);
        }

        lines = partsFileList1.text.Split('\n');
        foreach (string line in lines)
        {
            int qtd = int.Parse(line.Split(';')[3]);
            for (int i = 0; i < qtd; i++)
            {
                partsList1.Add(line.Split(';')[0]);
            }
        }

        lines = partsFileList2.text.Split('\n');
        foreach (string line in lines)
        {
            int qtd = int.Parse(line.Split(';')[3]);
            for (int i = 0; i < qtd; i++)
            {
                partsList2.Add(line.Split(';')[0]);
            }
        }

        lines = partsFileList3.text.Split('\n');
        foreach (string line in lines)
        {
            int qtd = int.Parse(line.Split(';')[3]);
            for (int i = 0; i < qtd; i++)
            {
                partsList3.Add(line.Split(';')[0]);
            }
        }
        lines = partsFileList4.text.Split('\n');
        foreach (string line in lines)
        {
            int qtd = int.Parse(line.Split(';')[3]);
            for (int i = 0; i < qtd; i++)
            {
                partsList4.Add(line.Split(';')[0]);
            }
        }

    }

    public void UpdateNextBoxRef()
	{
        floatingTagBlue.transform.parent.transform.position = floatingTag.transform.parent.transform.position;
        floatingTagBlue.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = floatingTag.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text;
        floatingTagBlue.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = floatingTag.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text;
        floatingTagBlue.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = floatingTag.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text;
        floatingTagBlue.SetActive(true);

        boxToPickIndex++;
        boxToPickRef = selectedList[boxToPickIndex];

        foreach (GameObject b in repo.boxesList)
        {
            if (b.GetComponent<BoxTagInformation>().partReference == boxToPickRef)
            {
                b.GetComponent<Renderer>().material = greenMat;
                floatingTag.transform.parent.transform.position = new Vector3(b.transform.position.x, b.transform.position.y+0.25f, b.transform.position.z);
                floatingTag.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partName;
                floatingTag.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partReference;
                floatingTag.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = b.GetComponent<BoxTagInformation>().partLocation;
                floatingTag.SetActive(true);
                glow.transform.position = b.transform.position;
                glow.SetActive(true);
                arrows.nextBoxPosition = glow.transform.position;
            }
            b.SetActive(true);
            b.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
