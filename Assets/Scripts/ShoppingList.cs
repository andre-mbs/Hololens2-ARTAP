using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public string[] list1;
    public string[] list2;

    public Material greenMat;
    public bool visualizationMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableListItems()
	{
        foreach(string part in list1)
		{
            string boxName = gameObject.GetComponent<BoxInformationRepo>().GetByReference(part);
            foreach (GameObject b in gameObject.GetComponent<BoxInformationRepo>().boxesList)
            {
                if(b.name == boxName)
				{
                    b.GetComponent<Renderer>().material = greenMat;
                    b.SetActive(true);
                }
            }
        }
    }

    public void LoadList()
	{

	}
}
