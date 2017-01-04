using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlightSystem : ShipSystem {

	List<ThrusterModule> thrusters;

	float maxSpeed;
	float maxRotationSpeed;

	float moveHorizontal;
	float moveVertical;

	public Rigidbody MyBody; // :(

	void Awake () {
		thrusters = new List<ThrusterModule>(GetComponentsInChildren<ThrusterModule> ()); // =\

		maxSpeed = thrusters [0].Thrust / 10.0f;
		maxRotationSpeed = thrusters [0].Thrust / 35.0f;
	}

	void Update () {
		
	}

	void FixedUpdate () {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
			moveVertical = Input.GetAxis ("Vertical");
		} else if (moveVertical != 0.0f) {
			if (moveVertical > 0.0f) {
				moveVertical -= 0.02f;
			}
			if (moveVertical < 0.0f) {
				moveVertical += 0.02f;
			}
			if ((int)(moveVertical * 100) == 0) {
				moveVertical = 0.0f;
			}
		}
		moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (transform.forward.x * moveVertical * maxSpeed, 0.0f, transform.forward.z * moveVertical * maxSpeed);
		MyBody.velocity = movement;
		MyBody.angularVelocity = new Vector3 (0.0f, moveHorizontal * maxRotationSpeed, 0.0f);
	}
}
