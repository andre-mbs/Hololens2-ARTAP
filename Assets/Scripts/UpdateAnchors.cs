using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.WorldLocking.Core;
using UnityEngine.XR.WSA.Persistence;

public class UpdateAnchors : MonoBehaviour
{
    private WorldLockingManager manager;
    private ManagerSettings settings;
    private WorldAnchorStore store;

    // Start is called before the first frame update
    void Start()
    {
        manager = WorldLockingManager.GetInstance();
        settings = manager.Settings;

        //WorldAnchorStore.GetAsync(StoreLoaded);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWL()
	{
        settings.Enabled = false;
        WorldLockingManager.GetInstance().Settings = settings;


    }
    public void EnableWL()
    {
        settings.Enabled = true;
        WorldLockingManager.GetInstance().Settings = settings;

    }

    public void SaveWL()
    {
        manager.Save();
    }

    private void StoreLoaded(WorldAnchorStore store)
	{
        this.store = store;
	}
}
