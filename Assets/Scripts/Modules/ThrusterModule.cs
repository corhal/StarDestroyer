using UnityEngine;
using System.Collections;

public class ThrusterModule : ShipModule {

	public Vector3 ThrustForce;

	public void Push (Rigidbody rigidbody) {
		rigidbody.AddForce (ThrustForce, ForceMode.Force);
	}
}
