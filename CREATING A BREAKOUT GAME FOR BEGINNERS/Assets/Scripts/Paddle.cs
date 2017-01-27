using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 1f;
	public float clamp = 8f;

	private Vector3 position = GetPosition(0f);

	// Update is called once per frame
	void Update () {

		var x = transform.position.x + Input.GetAxis("Horizontal") * paddleSpeed;
		position = GetPosition(Mathf.Clamp(x, -clamp, clamp));
		transform.position = position;
	}

	private static Vector3 GetPosition(float x)
	{
		return new Vector3(x, -9.5f, 0);
	}
}
