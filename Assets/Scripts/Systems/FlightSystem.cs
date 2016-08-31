using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlightSystem : ShipSystem {

	List<ThrusterModule> thrusters;
	public Rigidbody MyBody; // :(

	void Awake() {
		thrusters = new List<ThrusterModule>(GetComponentsInChildren<ThrusterModule> ()); // =\
	}

	void Start() {
		foreach (var thruster in thrusters) {
			thruster.Push (MyBody);
		}
	}
}
