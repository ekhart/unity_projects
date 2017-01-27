using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score;
	public int targetScore = 400;

	public Text scoreText,
				timeText; 
	public int timePerLevel = 30;

	public GameObject youWon, gameOver;


	private float clockSpeed = 1f;

	void SetScoreText ()
	{
		scoreText.text = "Score: " + score + "/" + targetScore;
	}

	void Awake () 
	{
		SetScoreText ();
		InvokeRepeating("Clock", 0, clockSpeed);
	}

	void Clock()
	{
		timeText.text = "Time: " + --timePerLevel;

		if (timePerLevel == 0)
		{
			CheckGameOver();
		}
	}

	public void AddPoints(int points)
	{
		score += points;
		SetScoreText ();
	}

	void CheckGameOver()
	{
		if (score >= targetScore)
		{
			Time.timeScale = 0;
			youWon.SetActive(true);
		}
		else 
		{
			Time.timeScale = 0;
			gameOver.SetActive(true);
		}
	}
}
