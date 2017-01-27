using UnityEngine;
using System.Collections;

public class Claw : MonoBehaviour {

	public Transform origin;
	public float speed = 4f;
	public Gun gun;
	public ScoreManager scoreManager; 

	private Vector3 target;
	private int jewelValue = 100;
	private GameObject childObject;
	private LineRenderer lineRenderer;
	private bool 	hitJewel, 
					retracting;

	void Awake () 
	{
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update () 
	{
		float step = speed * Time.deltaTime;
		//var position = transform.position;	//position is copy so it wont, affect transform.position
		transform.position = Vector3.MoveTowards (transform.position,  target,  step);

		lineRenderer.SetPosition(0, origin.position);
		lineRenderer.SetPosition (1, transform.position);

		if (transform.position == origin.position && retracting)
		{
			gun.CollectedObject();
			if (hitJewel)
			{
				scoreManager.AddPoints(jewelValue);
				hitJewel = false;
			}
			Destroy(childObject);
			gameObject.SetActive(false);
		}
	}

	public void ClawTarget(Vector3 position)
	{
		target = position;
	}

	void OnTriggerEnter(Collider other)
	{
		retracting = true;
		target = origin.position;

		if (other.gameObject.CompareTag("Jewel"))
		{
			hitJewel = true;
			PullThing (other);
		}
		else if (other.gameObject.CompareTag("Rock"))
		{
			PullThing (other);
		}
	}

	void PullThing (Collider other)
	{
		childObject = other.gameObject;
		other.transform.SetParent (transform);
	}
}
