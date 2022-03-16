using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnBox : MonoBehaviour
{
	public GameObject boxPrefab;
	public List<GameObject> boxesList = new List<GameObject>();
	private static int cnt = 0;
	private static System.Random rnd = new System.Random();

	public void Spawn()
	{
		Vector3 headPosition = GameObject.Find("Main Camera").transform.position;

		Debug.Log("Spawn: Box" + cnt.ToString());

		Vector3 position = new Vector3(headPosition.x, headPosition.y - 0.1f, transform.position.z + 0.8f);
		GameObject spawnedBox = Instantiate(boxPrefab, position, transform.rotation);
		while(GameObject.Find("box" + cnt.ToString()) != null)
		{
			cnt++;
		}
		spawnedBox.name = "box" + cnt.ToString();
		spawnedBox.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()));

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
		spawnedBox.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()));

		boxesList.Add(spawnedBox);
		cnt++;
	}
}
