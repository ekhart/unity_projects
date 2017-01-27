using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	public void Start() {
		var rigidbody = GetComponent<Rigidbody>();
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
