using UnityEngine;
using System.Collections;

public class SpawnJewels : MonoBehaviour {

	public int 	xRange = 10,
				yRange = -10,
				numObjects = 16;

	public GameObject[] objects;

	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Spawn() 
	{
		for (int i = 0; i < numObjects; i++)
		{
			var spawnLocation = new Vector3(Range(xRange), Range(yRange), 0);
			var objectPick = Random.Range(0, objects.Length);
			Instantiate(objects[objectPick], spawnLocation, Random.rotation);
		}
	}

	static int Range(int minmax) 
	{
		return Random.Range(-minmax, minmax);
	}
}
