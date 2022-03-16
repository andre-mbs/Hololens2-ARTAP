using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInformationRepo : MonoBehaviour
{
    private Dictionary<string, string[]> repo;
    // Start is called before the first frame update
    void Start()
    {
        repo = new Dictionary<string, string[]>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(string boxName, string partName, string partReference, string partLocation)
	{
        string[] value = { partName, partReference, partLocation };
        repo.Add(boxName, value);
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
