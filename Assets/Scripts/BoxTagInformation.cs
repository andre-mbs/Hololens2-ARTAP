using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxTagInformation : MonoBehaviour
{
	public string partName;
    public string partReference;
    public string partLocation;

    public bool tagSet;

    public GameObject textMeshPro;
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

        textMeshPro.GetComponent<TextMeshPro>().text = partReference;
    }

    public void UpdateText()
	{
        textMeshPro.GetComponent<TextMeshPro>().text = partReference;
	}

    public void ShowText()
	{
        textMeshPro.SetActive(true);
	}

    public void HideText()
	{
        textMeshPro.SetActive(false);
    }
}
