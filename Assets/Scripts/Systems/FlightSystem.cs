using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlightSystem : ShipSystem {

	List<ThrusterModule> thrusters;

	List<ThrusterModule> backThrusters;
	List<ThrusterModule> frontThrusters;
	List<ThrusterModule> rightThrusters;
	List<ThrusterModule> leftThrusters;

	float maxSpeed;
	float maxRotationSpeed;

	float moveHorizontal;
	float moveVertical;

	public Rigidbody MyBody; // :(

	void Awake () {
		thrusters = new List<ThrusterModule>(GetComponentsInChildren<ThrusterModule> ()); // =\
		backThrusters = new List<ThrusterModule>();
		frontThrusters = new List<ThrusterModule> ();
		rightThrusters = new List<ThrusterModule> ();
		leftThrusters = new List<ThrusterModule> ();
		foreach (var thruster in thrusters) {
			int yAngle = (int)thruster.transform.rotation.eulerAngles.y;
			switch (yAngle) {
			case 0:
				backThrusters.Add (thruster);
				break;
			case 90:
				leftThrusters.Add (thruster);
				break;
			case 270:
				rightThrusters.Add (thruster);
				break;
			case 180:
				frontThrusters.Add (thruster);
				break;
			default:
				throw new UnityException ("Tried to add thruster to the ship which is not front, back, right or left");
			}
		}
		maxSpeed = backThrusters [0].Thrust / 10.0f;
		maxRotationSpeed = leftThrusters [0].Thrust / 10.0f;
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
