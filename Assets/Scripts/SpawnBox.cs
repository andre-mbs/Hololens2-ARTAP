using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnBox : MonoBehaviour
{
	public GameObject boxPrefab;
	public Material transparentBlack;
	public Material transparentBlue;
	public GameObject head;
	public BoxInformationRepo repo;

	public GameObject lastSpawnedBox;


	private static int cnt = 0;

	public void Start()
	{
	}

	public void Spawn()
	{
		Vector3 headPos = head.transform.position;

		Vector3 position = headPos + head.transform.forward * 0.6f;
		GameObject spawnedBox = Instantiate(boxPrefab, position, transform.rotation);
		lastSpawnedBox = spawnedBox;
		while (GameObject.Find("box" + cnt.ToString()) != null)
		{
			cnt++;
		}
		spawnedBox.name = "box" + cnt.ToString();
		spawnedBox.GetComponent<Renderer>().material = transparentBlack;

		repo.AddBox(spawnedBox);
		cnt++;
	}

	public void Spawn(string name)
	{
		Vector3 position = new Vector3(0f, 0f, 0f);
		GameObject spawnedBox = Instantiate(boxPrefab, position, transform.rotation);
		lastSpawnedBox = spawnedBox;
		spawnedBox.name = name;
		spawnedBox.GetComponent<Renderer>().material = transparentBlack;

		repo.AddBox(spawnedBox);
		if (spawnedBox.GetComponent<BoxTagInformation>().tagSet)
		{
			spawnedBox.GetComponent<Renderer>().material = transparentBlue;
		}
		cnt++;
	}
}
