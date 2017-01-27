using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	const float None = 0.0f;

	public float speed, 
		tilt, 
		fireRate;
	public Boundary boundry;

	public GameObject shot;
	public Transform shotSpawn;

	private float nextFire;

	void Update()
	{
		#if UNITY_EDITOR
		var fire = Input.GetButton("Fire1");
		#elif UNITY_ANDROID
		var fire = Input.touchCount > 0; 
		#endif

		if (fire && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
		 	//GameObject clone = 
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//as GameObject;
			GetComponent<AudioSource>().Play();
		}
	}

	float GetSpeed() {
		#if UNITY_EDITOR
		return speed;
		#elif UNITY_ANDROID
		return 10;
		#endif
	}

	void FixedUpdate()
	{
	#if UNITY_EDITOR
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
	#elif UNITY_ANDROID
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;
	#endif
		var movement = new Vector3(moveHorizontal, None, moveVertical);

		var rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = movement * GetSpeed();

		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundry.xMin, boundry.xMax), 
			None,
			Mathf.Clamp(rigidbody.position.z, boundry.zMin, boundry.zMax)
		);

		rigidbody.rotation = Quaternion.Euler(None, None, rigidbody.velocity.x * -tilt);
	}
}
