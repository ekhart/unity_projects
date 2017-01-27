using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int lives = 3;
	public int bricks = 20;
	public float resetDelay = 1f;
	public Text livesText;
	public GameObject gameOver, youWon,
		bricksPrefab, 
		paddle, 
		deathParticles;
	public static GameManager instance = null;

	private GameObject clonePaddle;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this	;
		else if (instance != null)
			Destroy(gameObject);

		Setup();
	}

	private void Setup()
	{
		var position = transform.position;
		var identity = Quaternion.identity;
		SetClonePaddle ();
		Instantiate (bricksPrefab, position, identity);
	}

	void SetClonePaddle ()
	{
		var position = transform.position;
		var identity = Quaternion.identity;
		clonePaddle = Instantiate (paddle, position, identity) as GameObject;
	}

	private void CheckGameOver()
	{
		if (bricks < 1)
		{
			youWon.SetActive(true);
			TimeScaleAndReset ();
		}	

		if (lives < 1)
		{
			gameOver.SetActive(true);
			TimeScaleAndReset ();
		}
	}

	void Reset()
	{
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
	}

	void TimeScaleAndReset ()
	{
		Time.timeScale = .25f;
		Invoke ("Reset", resetDelay);
	}

	public void LoseLife()
	{
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy(clonePaddle);
		Invoke("SetClonePaddle", resetDelay);
		CheckGameOver();
	}

	public void DestroyBrick()
	{
		bricks--;
		CheckGameOver();
	}
}
