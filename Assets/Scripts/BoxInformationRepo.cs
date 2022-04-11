using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInformationRepo : MonoBehaviour
{
    public List<GameObject> allBoxesList;
    public List<GameObject> boxesList;
    public SceneSelector sceneSelector;

    private Dictionary<string, string[]> repo;

    // Start is called before the first frame update
    void Start()
    {
        boxesList = new List<GameObject>();
        allBoxesList = new List<GameObject>();
        repo = new Dictionary<string, string[]>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBox(GameObject box)
	{
        Debug.Log("REPO: " + box.name + " added");
        allBoxesList.Add(box);
        boxesList.Add(box);
        if (PlayerPrefs.HasKey(box.name))
        {
            string[] boxInfo = PlayerPrefs.GetString(box.name).Split(';');
            repo.Add(box.name, boxInfo);
            Debug.Log("REPO: " + "Info found for " + box.name + "->" + boxInfo[0] + ";" + boxInfo[1] + ";" +boxInfo[2]);
            box.GetComponent<BoxTagInformation>().UpdateInfo(boxInfo[0], boxInfo[1], boxInfo[2]);
        }
	}

    public void RemoveBox(GameObject box)
	{
        allBoxesList.Remove(box);
        boxesList.Remove(box);
        PlayerPrefs.DeleteKey(box.name+"_scene");
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

        //Debug.Log("REPO: " + "Info saved: " + boxName + "->" + valueString);
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

    public void UpdateSceneBoxesList()
	{
        boxesList.Clear();
        foreach(GameObject b in allBoxesList)
		{
            if(PlayerPrefs.GetInt(b.name + "_scene") == sceneSelector.selectedScene)
			{
                boxesList.Add(b);
			}
		}
	}

    public bool ContainsBoxByName(string name)
	{
        foreach(GameObject b in allBoxesList)
		{
            if(b.name == name)
			{
                return true;
			}
		}
        return false;
	}
}
