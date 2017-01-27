using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public int maxPlatforms = 20;
	public GameObject platform;
	public float horizontalMin = 6.5f;
	public float horizontalMax = 14f;
	public float verticalMin = -6f;
	public float verticalMax = 6f;

	private Vector2 originPosition;

	// Use this for initialization
	void Start () {
		originPosition = transform.position;
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn() 
	{
		var noRotation = Quaternion.identity;
		for (int i = 0; i < maxPlatforms; ++i)
		{
			var randomPosition = originPosition + GetRandomPosition();
			Instantiate (platform, randomPosition, noRotation);
			originPosition = randomPosition;
		}
	}

	private Vector2 GetRandomPosition()
	{
		var horizontal = Random.Range(horizontalMin, horizontalMax);
		var vertical = Random.Range(verticalMin, verticalMax);
		return new Vector2(horizontal, vertical);
	}
}
