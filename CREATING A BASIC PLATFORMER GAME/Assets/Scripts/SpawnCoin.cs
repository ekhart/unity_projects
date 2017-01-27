using UnityEngine;
using System.Collections;

public class SpawnCoin : MonoBehaviour {

	public Transform[] spawnCoins;
	public GameObject coin;

	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Spawn () {
		for (int i = 0; i < spawnCoins.Length; ++i)
		{
			var coinFlip = Random.Range(0, 2);
			if (coinFlip > 0)
				Instantiate(coin, spawnCoins[i].position, Quaternion.identity);
		}
	}
}
