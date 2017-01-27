using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	int score;
	public Text scoreText, restartText, gameOverText;
	private bool gameOver, restart;

	void Start () {
		score = 0;
		gameOver = restart = false;
		gameOverText.text = restartText.text = "";
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
				Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; ++i) {
				var range = Random.Range(-spawnValues.x, spawnValues.x);
				var spawnPosition = new Vector3(range, spawnValues.y, spawnValues.z);
				var spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		gameOverText.text = "Game over";
		gameOver = true;
	}
}