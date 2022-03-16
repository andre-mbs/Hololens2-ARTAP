using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
	private Anchors managerAnchors;
	//private MenuInteraction managerMenuInteraction;
	public HandMenu handMenu;
	public GameObject partsListGo;

	private void Start()
	{
		managerAnchors = GameObject.Find("Manager").GetComponent<Anchors>();
		handMenu = GameObject.Find("HandMenu").GetComponent<HandMenu>();
		//managerMenuInteraction = GameObject.Find("Manager").GetComponent<MenuInteraction>();
	}
	public void BeginInteraction()
	{
		Debug.Log(name + " anchor deleted");
		managerAnchors.selectedObject = gameObject;
		managerAnchors.DeleteAnchor(name);
	}

	public void EndInteraction()
	{
		managerAnchors.selectedObject = gameObject;
		managerAnchors.SaveAnchor(name);
	}

	public void DeleteBox()
	{
		if (handMenu.removeBoxFlag)
		{
			Debug.Log(name + " deleted");
			managerAnchors.DeleteAnchor(name);
			handMenu.removeBoxFlag = false;
			//Destroy(gameObject);
			gameObject.SetActive(false);
		}
	}

	public void ShowPartsList()
	{
		//Debug.Log(managerMenuInteraction.showPartsListFlag);
		if (handMenu.showPartsListFlag)
		{
			//Vector3 offset = new Vector3(0.1f, 0.1f, 0.1f);
			//Vector3 pos = transform.position + offset;
			//Vector3 pos = new Vector3(transform.position.x+0.5f, transform.position.y+0.5f, transform.position.z+0.5f);

			//partsListGo.transform.position = pos;
			//partsListGo.GetComponent<PartsListMenu>().selectedGo = gameObject;
			//partsListGo.SetActive(true);

			//handMenu.showPartsListFlag = false;
		}

	}

	public void ShowInformation()
	{

	}
}
