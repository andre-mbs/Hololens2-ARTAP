using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnBox : MonoBehaviour
{
	public GameObject boxPrefab;
	public List<GameObject> boxesList = new List<GameObject>();
	public Material mat;
	public GameObject handMenu;

	private static int cnt = 0;
	private static System.Random rnd = new System.Random();
	private Vector3 offset;

	public void Start()
	{
		offset = new Vector3(0.0f, 0.0f, 0.5f);
	}

	public void Spawn()
	{
		Vector3 handMenuPos = handMenu.transform.position;

		Debug.Log("Spawn: Box" + cnt.ToString());

		Vector3 position = handMenuPos + offset;
		GameObject spawnedBox = Instantiate(boxPrefab, position, transform.rotation);
		while(GameObject.Find("box" + cnt.ToString()) != null)
		{
			cnt++;
		}
		spawnedBox.name = "box" + cnt.ToString();
		spawnedBox.GetComponent<Renderer>().material = mat;

		boxesList.Add(spawnedBox);
		cnt++;
	}

	public void Spawn(string name)
	{
		Vector3 headPosition = GameObject.Find("Main Camera").transform.position;

		Debug.Log("Spawn: " + name);

		Vector3 position = new Vector3(headPosition.x, headPosition.y - 0.1f, transform.position.z + 0.8f);
		GameObject spawnedBox = Instantiate(boxPrefab, position, transform.rotation);
		spawnedBox.name = name;
		//spawnedBox.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()));
		spawnedBox.GetComponent<Renderer>().material = mat;

		boxesList.Add(spawnedBox);
		cnt++;
	}
}
