using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class BoxTagInformation : MonoBehaviour
{
	public string partName;
    public string partReference;
    public string partLocation;

    public GameObject toolTip;

    public bool tagSet;

	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateInfo(string partName, string partReference, string partLocation)
	{
        this.partName = partName;
        this.partReference = partReference;
        this.partLocation = partLocation;

        toolTip.GetComponent<ToolTip>().ToolTipText = partReference;
    }

	public void EnableTooltip()
	{
		if (tagSet)
		{
            toolTip.SetActive(true);
        }
	}
}
