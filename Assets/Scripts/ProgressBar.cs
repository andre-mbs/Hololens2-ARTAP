using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public ShoppingList shoppingList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3((float)shoppingList.numberPickedParts / (float)shoppingList.selectedList.Count, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = shoppingList.numberPickedParts + "/" + shoppingList.selectedList.Count;
    }

    public void ResetProgress()
	{
        gameObject.transform.localScale = new Vector3(0, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}
