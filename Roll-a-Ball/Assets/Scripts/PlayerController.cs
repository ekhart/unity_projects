using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rigidbody;
	private int count;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		count = 0;
		countText.text = GetCountText ();
		winText.text = GetWinText ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {

#if UNITY_EDITOR
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
#elif UNITY_ANDROID
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;
		//needed speed multiply
		speed *= 2;
#endif
		Vector3 movement = new Vector3(moveHorizontal, .0f, moveVertical);

		rigidbody.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		var gameObject = other.gameObject;
		if (gameObject.CompareTag("Pick Up"))
		{
			gameObject.SetActive(false);
			count++;
			countText.text = GetCountText();
			winText.text = GetWinText ();
		}
	}

	string GetCountText ()
	{
		return "Count: " + count;
	}

	string GetWinText ()
	{
		return count == 12 ? "You win!" : "";
	}
}
