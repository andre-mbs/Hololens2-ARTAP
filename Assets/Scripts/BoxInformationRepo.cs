using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInformationRepo : MonoBehaviour
{
    public List<GameObject> boxesList;
    private Dictionary<string, string[]> repo;
    // Start is called before the first frame update
    void Start()
    {
        boxesList = new List<GameObject>();
        repo = new Dictionary<string, string[]>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBox(GameObject box)
	{
        Debug.Log(box.name + " added");
        boxesList.Add(box);
        if (PlayerPrefs.HasKey(box.name))
        {
            string[] boxInfo = PlayerPrefs.GetString(box.name).Split(';');
            repo.Add(box.name, boxInfo);
            Debug.Log("Info found for " + box.name + "->" + boxInfo[0] + ";" + boxInfo[1] + ";" +boxInfo[2]);
            box.GetComponent<BoxTagInformation>().UpdateInfo(boxInfo[0], boxInfo[1], boxInfo[2]);
        }
	}

    public void RemoveBox(GameObject box)
	{
        boxesList.Remove(box);
        RemoveInfo(box.name);
	}

    public void AddInfo(string boxName, string partName, string partReference, string partLocation)
	{
		if (repo.ContainsKey(boxName))
		{
            repo.Remove(boxName);
		}

        string[] valueArray = { partName, partReference, partLocation };
        string valueString = partName + ";" + partReference + ";" + partLocation;
        repo.Add(boxName, valueArray);
        PlayerPrefs.SetString(boxName, valueString);

        Debug.Log("Info saved: " + boxName + "->" + valueString);
	}

    public void RemoveInfo(string boxName)
	{
        PlayerPrefs.DeleteKey(boxName);
        repo.Remove(boxName);
    }

    public string GetByReference(string partRef)
	{
        string box = null;
        foreach( KeyValuePair<string, string[]> kvp in repo)
		{
            if (kvp.Value[1] == partRef)
			{
                box = kvp.Key;
            }
		}

        return box;
	}
}
