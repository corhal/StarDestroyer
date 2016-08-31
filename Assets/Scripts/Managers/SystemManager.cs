using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemManager : MonoBehaviour {

	ShipSystem[] systems;

	AutoWeaponSystem autoWeaponSystem;
	FlightSystem flightSystem;

	void Awake() {
		systems = GetComponentsInChildren<ShipSystem> ();
		autoWeaponSystem = GetComponentInChildren<AutoWeaponSystem> ();
		flightSystem = GetComponentInChildren<FlightSystem> ();
		flightSystem.MyBody = GetComponent<Rigidbody> ();
		DetectionSystem.OnTargetsChanged += DetectionSystem_OnTargetsChanged;
	}

	void DetectionSystem_OnTargetsChanged (DetectionSystem sender, GameObject target, bool found) {
		if (UnityEditor.ArrayUtility.Contains(systems, sender)) {			
			autoWeaponSystem.UpdateTargets (target, found);
		}
	}
}
