using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int min, max;

		public Count(int min, int max)
		{
			this.min = min;
			this.max = max;
		}
	}

	public int columns = 8, rows = 8;
	public Count wallCount = new Count(5, 9),
		foodCount = new Count(1, 5);
	public GameObject exit;
	public GameObject[] floorTiles,
		wallTiles,
		foodTiles,
		enemyTiles,
		outerWallTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitialiseList()
	{
		for (var x = 1; x < columns + 1; x++)
			for (var y = 1; y < rows + 1; y++)
				gridPositions.Add(new Vector3(x, y, 0f));
	}

	void BoardSetup() 
	{
		for (var x = -1; x < columns + 1; x++)
			for (var y = -1; y < rows + 1; y++)
			{
				var toInstantiate = GetToInstantiate(floorTiles);
				if (x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = GetToInstantiate(outerWallTiles);

				var instance = (GameObject) Instantiate(toInstantiate, 
			                                        new Vector3(x, y, 0f), 
			                                        Quaternion.identity);
				instance.transform.SetParent(boardHolder);
			}
	}

	Vector3 RandomPostion()
	{
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	GameObject GetToInstantiate(GameObject[] list) 
	{
		return list[Random.Range(0, list.Length)];
	}

	void LayoutObjectAtRandom (GameObject[] tileArray, int min, int max)
	{
		int objectCount = Random.Range(min, max);
		for (var i = 0; i < objectCount; ++i)
		{
			Vector3 randomPosition = RandomPostion();
			GameObject tileChoice = GetToInstantiate(tileArray);
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitialiseList();
		LayoutObjectAtRandom(wallTiles, wallCount.min, wallCount.max);
		LayoutObjectAtRandom(foodTiles, foodCount.min, foodCount.max);

		int enemyCount = (int) Mathf.Log(level, 2f);
		LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

		Instantiate (exit, new Vector3(columns-1, rows-1, 0f), Quaternion.identity);
	}
}
