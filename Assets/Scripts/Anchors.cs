using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine.XR.WSA;

public class Anchors : MonoBehaviour
{
    public GameObject selectedObject;
    private string anchorId;
    private WorldAnchorStore store;
    private bool isStoreLoaded;

    // Start is called before the first frame update
    void Start()
    {
        Debug.developerConsoleVisible = false;
        WorldAnchorStore.GetAsync(StoreLoaded);    
    }

    // Update is called once per frame
    void Update()
    {
		if (isStoreLoaded)
		{
            Debug.Log(store.anchorCount.ToString() + " anchors on store");

            foreach (string anchorId in store.GetAllIds())
            {
                gameObject.GetComponent<SpawnBox>().Spawn(anchorId);
                LoadAnchor(anchorId);

            }

            isStoreLoaded = false;
		}
    }

    void StoreLoaded(WorldAnchorStore store)
	{
        this.store = store;
        isStoreLoaded = true;
	}

    public void SaveAnchor(string gameObjName)
	{
        if(GameObject.Find(gameObjName) != null)
		{
            Debug.Log(gameObjName + " anchor saved");
            WorldAnchor anchor = selectedObject.AddComponent<WorldAnchor>();
            store.Save(gameObjName, anchor);
		}
        
	}

    public void LoadAnchor(string anchorId)
	{
        Debug.Log(anchorId + " anchor loaded");
        store.Load(anchorId, GameObject.Find(anchorId));
        GameObject.Find(anchorId).SetActive(false);
    }

    public void DeleteAnchor(string gameObjName)
	{
        Destroy(selectedObject.GetComponent<WorldAnchor>());
        store.Delete(gameObjName);
    }
}
