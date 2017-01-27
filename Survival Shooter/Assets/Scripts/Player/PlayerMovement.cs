using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float cameraRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
	
		Move(h, v);
		Turning();
		Animating(h, v);
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast(cameraRay, out floorHit, cameraRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float h, float v)
	{
		const float EPSILON = .1f;
		//Debug.Log(h + ", " + v);
		bool walking = System.Math.Abs (h - 0f) > EPSILON
			|| System.Math.Abs (v - 0f) > EPSILON;
		anim.SetBool("IsWalking", walking);
	}
}
