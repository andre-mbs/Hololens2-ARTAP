using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class BoxInteraction : MonoBehaviour
{
	private Anchors managerAnchors;
	private ShoppingList managerShoppingList;
	private BoxInformationRepo repo;
	public HandMenu handMenu;
	public GameObject partsListGo;
	public Material greenMat;
	public Material transparentBlueMat;
	public Material transparentBlackMat;

	private void Start()
	{
		managerAnchors = GameObject.Find("Manager").GetComponent<Anchors>();
		managerShoppingList = GameObject.Find("Manager").GetComponent<ShoppingList>();
		repo = GameObject.Find("Manager").GetComponent<BoxInformationRepo>();
		handMenu = GameObject.Find("HandMenu").GetComponent<HandMenu>();
	}
	public void BeginInteraction()
	{
		//Debug.Log("BOXINTERACTION: " + name + " anchor deleted");
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
			//Debug.Log(name + " deleted");
			managerAnchors.DeleteAnchor(name);
			handMenu.removeBoxFlag = false;
			handMenu.SetMenu(handMenu.configurationMenu);

			repo.RemoveBox(gameObject);
			Destroy(gameObject);
		}
	}

	public void ShowPartsList()
	{
		if (handMenu.setInformationFlag)
		{
			// Set ManipulationType to "Nothing" (01 bitwise and 10), so the box dosen't move when selecting it to set tag information
			// More at: https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.utilities.manipulationhandflags
			gameObject.GetComponent<ObjectManipulator>().ManipulationType =
					Microsoft.MixedReality.Toolkit.Utilities.ManipulationHandFlags.OneHanded &
					Microsoft.MixedReality.Toolkit.Utilities.ManipulationHandFlags.TwoHanded;

			// Check if there was a previously selected box
			if (handMenu.selectedBox && handMenu.selectedBox != gameObject)
			{
				if (handMenu.selectedBox.GetComponent<BoxTagInformation>().tagSet)
				{
					handMenu.selectedBox.GetComponent<Renderer>().material = transparentBlueMat;
				}
				else
				{
					handMenu.selectedBox.GetComponent<Renderer>().material = transparentBlackMat;
				}

				// Set ManipulationType to "Everything" (01 bitwise or 10), so the box can be moved again
				// More at: https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.utilities.manipulationhandflags
				handMenu.selectedBox.GetComponent<ObjectManipulator>().ManipulationType = 
					Microsoft.MixedReality.Toolkit.Utilities.ManipulationHandFlags.OneHanded | 
					Microsoft.MixedReality.Toolkit.Utilities.ManipulationHandFlags.TwoHanded;
			}

			// Set the box color to green when selected to set tag information
			gameObject.GetComponent<Renderer>().material = greenMat;
			handMenu.selectedBox = gameObject;
			handMenu.StartSetInformation();
		}

	}

	public void PickPart()
	{
		if (handMenu.visualizationMode)
		{
			//Debug.Log("Box Ref: " + gameObject.GetComponent<BoxTagInformation>().partReference + "; Next Ref: " + managerShoppingList.boxToPickRef);
			if (handMenu.seqPickMode)
			{
				if (gameObject.GetComponent<BoxTagInformation>().partReference == managerShoppingList.boxToPickRef)
				{
					gameObject.GetComponent<Renderer>().material = transparentBlackMat;
					managerShoppingList.UpdateNextBoxRef();
				}
			}
			else
			{
				gameObject.GetComponent<Renderer>().material = transparentBlackMat;
			}
		}
	}
}
