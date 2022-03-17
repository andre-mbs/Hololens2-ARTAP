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
        boxesList.Add(box);
	}

    public void RemoveBox(GameObject box)
	{
        boxesList.Remove(box);
	}

    public void AddInfo(string boxName, string partName, string partReference, string partLocation)
	{
		if (repo.ContainsKey(boxName))
		{
            repo.Remove(boxName);
		}

        string[] value = { partName, partReference, partLocation };
        repo.Add(boxName, value);
	}

    public void RemoveInfo(string boxName)
	{
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

    public void WriteToFile()
	{

	}
}
