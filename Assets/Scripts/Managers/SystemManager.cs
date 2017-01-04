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
	CargoSystem cargoSystem;
	TradeSystem tradeSystem;

	void Awake() {
		systems = GetComponentsInChildren<ShipSystem> ();
		autoWeaponSystem = GetComponentInChildren<AutoWeaponSystem> ();
		flightSystem = GetComponentInChildren<FlightSystem> ();
		detectionSystem = GetComponentInChildren<DetectionSystem> ();
		cargoSystem = GetComponentInChildren<CargoSystem> ();
		tradeSystem = GetComponentInChildren<TradeSystem> ();
		if (flightSystem != null) {
			flightSystem.MyBody = GetComponent<Rigidbody> (); // Ужас!
		}

		if (detectionSystem != null && autoWeaponSystem != null) {
			autoWeaponSystem.SubscribeToDetectionSystem (detectionSystem);
		}
		if (tradeSystem != null && cargoSystem != null) {
			tradeSystem.SubscribeToCargoSystem (cargoSystem);
		}
	}
}
