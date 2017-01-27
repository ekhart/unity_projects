using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	void Awake() 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

			// double jump
		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(horizontal));

		var speed = rb2d.velocity.x;
		if (horizontal * speed < maxSpeed)
			rb2d.AddForce(Vector2.right * horizontal * moveForce);

		if (Mathf.Abs(speed) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(speed) * maxSpeed, rb2d.velocity.y);

		var isGoingRight = horizontal > 0;
		var isGoingLeft = horizontal < 0;
		if ((isGoingRight && !facingRight) || (isGoingLeft && facingRight))
			Flip();

		if (jump)
		{
			anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0, jumpForce));
			jump = false;
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
