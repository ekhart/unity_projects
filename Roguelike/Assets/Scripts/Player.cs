using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MovingObject {

	public int wallDamage = 1;
	public int pointsPerFood = 10,
		pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	public Text foodText;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;

	private Animator animator;
	private GameManager gameManager;
	private SoundManager soundManager;
	private int food;

	private Vector2 touchOrigin = -Vector2.one;

	void UpdateFoodText()
	{
		foodText.text = "Food: " + food;
	}

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator>();
		gameManager = GameManager.instance;
		soundManager = SoundManager.instance;

		food = gameManager.playerFoodPoints;

		UpdateFoodText ();

		base.Start();
	}

	private void OnDisable()
	{
		gameManager.playerFoodPoints = food;
	}

	// Update is called once per frame
	void Update () {
		if (!gameManager.playersTurn) return;

		int horizontal = 0;
		int vertical = 0;

	#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = (int) Input.GetAxis("Horizontal");
		vertical = (int) Input.GetAxis("Vertical");

		if (horizontal != 0)
			vertical = 0;		//prevent diagonal move
	
	#else

		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Began)
			{
				touchOrigin = touch.position;
			}
			else if (touch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				var touchEnd = touch.position;
				var x = touchEnd.x - touchOrigin.x;
				var y = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1;
			
				if (Mathf.Abs(x) > Mathf.Abs(y))
					horizontal = x > 0 ? 1 : -1;
				else 
					vertical = y > 0 ? 1 : -1;
			}
		}

	#endif

		if (horizontal != 0 || vertical != 0)
			AttemptMove<Wall>(horizontal, vertical);
	}

	protected override void OnCantMove<T>(T component) 
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall(wallDamage);
		animator.SetTrigger("PlayerChop");
	}

	private void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger("PlayerHit");
		food -= loss;
		foodText.text = "-" + loss + " Food: " + food;
		CheckIfGameOver();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Exit")
		{
			Invoke("Restart", restartLevelDelay);
			enabled = false;
		}
		else if (collider.tag == "Food")
		{
			food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + " Food: " + food;
			soundManager.RandomizeSfx(eatSound1, eatSound2);
			collider.gameObject.SetActive(false);
		}
		else if (collider.tag == "Soda")
		{
			food += pointsPerSoda;
			foodText.text = "+" + pointsPerSoda + " Food: " + food;
			soundManager.RandomizeSfx(drinkSound1, drinkSound2);
			collider.gameObject.SetActive(false);
		}
	}

	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		food--;
		UpdateFoodText();

		base.AttemptMove<T>(xDir, yDir);

		RaycastHit2D hit;
		if (Move(xDir, yDir, out hit))
			soundManager.RandomizeSfx(moveSound1, moveSound2);

		CheckIfGameOver();

		gameManager.playersTurn = false;
	}

	private void CheckIfGameOver()
	{
		if (food <= 0)
		{
			soundManager.PlaySingle(gameOverSound);
			soundManager.musicSource.Stop();
			gameManager.GameOver();
		}
	}
}
