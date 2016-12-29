using UnityEngine;
using System.Collections;

public class ThrusterModule : ShipModule { // в сущности сейчас ничего не делает, надо привязать к FlightSystem

	public float Thrust;
	public Vector3 ThrustForce;

	void Start () {
		ThrustForce = transform.forward;
	}

	public void Push (Rigidbody rigidbody) {		
		rigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, Thrust * ThrustForce.z), ForceMode.Force);
	}

	public void Turn (Rigidbody rigidbody) {
		rigidbody.AddRelativeTorque (new Vector3 (0.0f, Thrust * ThrustForce.x, 0.0f), ForceMode.Force);
	}
}
