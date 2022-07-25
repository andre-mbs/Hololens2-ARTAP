using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Timers;
using TMPro;

public class BoxInteraction : MonoBehaviour
{
	private static Timer timer;

	private Anchors managerAnchors;
	private ShoppingList managerShoppingList;
	private BoxInformationRepo repo;
	public HandMenu handMenu;
	public Arrows managerArrows;
	public GameObject partsListGo;
	public Material greenMat;
	public Material transparentBlueMat;
	public Material transparentBlackMat;

	private UserTestsLog userTestsLog;
	private bool allowPickPart;

	private void Start()
	{
		managerAnchors = GameObject.Find("Manager").GetComponent<Anchors>();
		managerShoppingList = GameObject.Find("Manager").GetComponent<ShoppingList>();
		repo = GameObject.Find("Manager").GetComponent<BoxInformationRepo>();
		handMenu = GameObject.Find("HandMenu").GetComponent<HandMenu>();
		managerArrows = GameObject.Find("Manager").GetComponent<Arrows>();
		userTestsLog = GameObject.Find("Manager").GetComponent<UserTestsLog>();

		allowPickPart = true;
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
		if (handMenu.visualizationMode && allowPickPart)
		{
			//Debug.Log("Box Ref: " + gameObject.GetComponent<BoxTagInformation>().partReference + "; Next Ref: " + managerShoppingList.boxToPickRef);
			if (handMenu.seqPickMode)
			{
				if (gameObject.GetComponent<BoxTagInformation>().partReference == managerShoppingList.boxToPickRef)
				{
					//userTestsLog.StopTimer();
					gameObject.GetComponent<Renderer>().material = transparentBlackMat;
					gameObject.GetComponent<MeshRenderer>().enabled = false;
					managerShoppingList.numberPickedParts++;

					if (managerShoppingList.boxToPickIndex != managerShoppingList.selectedList.Count - 1)
					{
						managerShoppingList.UpdateNextBoxRef();
						//userTestsLog.StartTimer(false);
					}
					else
					{
						userTestsLog.StopTimer();
						userTestsLog.WriteToFile();
						handMenu.StartVisualization();
						managerShoppingList.topInfoBar.SetActive(true);

						Invoke("DisableFinishedConfirmation", 4);

						managerShoppingList.floatingTag.SetActive(false);
						managerShoppingList.floatingTagBlue.SetActive(false);
						managerShoppingList.glow.SetActive(false);

						managerArrows.arrowsEnabled = false;
					}

					//allowPickPart = false;
					//Invoke("SetPickPart", 3);

					managerShoppingList.topInfoBar2.transform.GetChild(0).GetComponent<TextMeshPro>().text = gameObject.GetComponent<BoxTagInformation>().partName + " picked";
					managerShoppingList.topInfoBar2.SetActive(true);
					Invoke("DisablePickConfirmation", 3);
				}
			}
			else
			{
				if (managerShoppingList.selectedList.Contains(gameObject.GetComponent<BoxTagInformation>().partReference))
				{
					gameObject.GetComponent<Renderer>().material = transparentBlackMat;
					gameObject.GetComponent<ObjectManipulator>().enabled = false;
					for(int i = 0; i<managerShoppingList.floatingTagsList.Count; i++)
					//foreach(GameObject tag in managerShoppingList.floatingTagsList)
					{
						GameObject tag = managerShoppingList.floatingTagsList[i];
						if(tag.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshPro>().text == gameObject.GetComponent<BoxTagInformation>().partReference)
						{
							managerShoppingList.floatingTagBlue.transform.parent.transform.position = tag.transform.position;
							managerShoppingList.floatingTagBlue.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = tag.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshPro>().text;
							managerShoppingList.floatingTagBlue.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = tag.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshPro>().text;
							managerShoppingList.floatingTagBlue.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = tag.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshPro>().text;
							managerShoppingList.floatingTagBlue.SetActive(true);

							managerShoppingList.floatingTagsList.Remove(tag);
							Destroy(tag);
						}
					}

					for (int i = 0; i < managerShoppingList.glowsList.Count; i++)
					//foreach (GameObject g in managerShoppingList.glowsList)
					{
						GameObject g = managerShoppingList.glowsList[i];
						if (g.name == gameObject.name)
						{
							managerShoppingList.glowsList.Remove(g);
							Destroy(g);
						}
					}

					managerShoppingList.numberPickedParts++;
					if(managerShoppingList.numberPickedParts == managerShoppingList.selectedList.Count)
					{
						userTestsLog.StopTimer();
						userTestsLog.WriteToFile();
						managerShoppingList.numberPickedParts = 0;
						handMenu.StartVisualization();
						managerShoppingList.topInfoBar.SetActive(true);

						Invoke("DisableFinishedConfirmation", 4);

						managerArrows.arrowsEnabled = false;
						managerShoppingList.floatingTagBlue.SetActive(false);
					}

					//allowPickPart = false;
					//Invoke("SetPickPart", 3);

					managerShoppingList.topInfoBar2.transform.GetChild(0).GetComponent<TextMeshPro>().text = gameObject.GetComponent<BoxTagInformation>().partName + " picked";
					managerShoppingList.topInfoBar2.SetActive(true);
					Invoke("DisablePickConfirmation", 3);
				}

			}
		}
	}

	public void SetPickPart()
	{
		allowPickPart = true;
	}

	public void DisablePickConfirmation()
	{
		managerShoppingList.topInfoBar2.SetActive(false);
	}

	public void DisableFinishedConfirmation()
	{
		managerShoppingList.topInfoBar.SetActive(false);
	}
}
