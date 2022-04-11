using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SceneSelector : MonoBehaviour
{
    public int selectedScene;

    // Start is called before the first frame update
    void Start()
    {
        RadioControl(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RadioControl(int idx)
	{
        ResetRadioGroup();
        gameObject.transform.GetChild(1).gameObject.transform.GetChild(idx).gameObject.GetComponent<Interactable>().IsToggled = true;
        selectedScene = idx;
        
	}

    public void ResetRadioGroup()
	{
        for(int i = 0; i < 4; i++)
		{
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Interactable>().IsToggled = false;
        }
        
	}
}
