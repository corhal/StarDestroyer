using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemManager : MonoBehaviour {

	/*
	 * Вероятно, все, зачем нужен sysManager - это помогать системам в нужное время узнать друг о друге?
	 */

	ShipSystem[] systems;

	AutoWeaponSystem autoWeaponSystem;
	DetectionSystem detectionSystem;
	FlightSystem flightSystem;

	void Awake() {
		systems = GetComponentsInChildren<ShipSystem> ();
		autoWeaponSystem = GetComponentInChildren<AutoWeaponSystem> ();
		flightSystem = GetComponentInChildren<FlightSystem> ();
		detectionSystem = GetComponentInChildren<DetectionSystem> ();
		flightSystem.MyBody = GetComponent<Rigidbody> (); // Ужас!

		if (detectionSystem != null && autoWeaponSystem != null) {
			autoWeaponSystem.SubscribeToDetectionSystem (detectionSystem);
		}
	}
}
