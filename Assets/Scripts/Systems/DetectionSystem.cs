using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void TargetStatusChangedEventHandler(GameObject target, bool found); // eh

public class DetectionSystem : ShipSystem {

	/*
	 * Нужен ли посредник в лице SystemManager'а?
	 * Или логично, что радарная система знает об оружейной?
	 * Или логично, что оружейная система подпишется на события радарной? <--
	 */
	
	List<GameObject> targets;
	List<RadarModule> radars;

	public event TargetStatusChangedEventHandler OnTargetStatusChanged;

	void Awake() {		
		radars = new List<RadarModule> (gameObject.GetComponentsInChildren<RadarModule> ());
		foreach (var radar in radars) {
			radar.OnTargetStatusChanged += Radar_OnTargetStatusChanged;
		}
		ShipModule.OnModuleDestroyed += ShipModule_OnModuleDestroyed; // не очень изящно, по-хорошему это должен делать радар
		targets = new List<GameObject> ();
	}

	void Radar_OnTargetStatusChanged (GameObject target, bool found) {				
		if (!targets.Contains (target) && found) {
			targets.Add (target);
			OnTargetStatusChanged (target, true);
		} else {
			Debug.Log ("removing target from radar system");
			targets.Remove (target);
			OnTargetStatusChanged (target, false);
		}
	}

	void ShipModule_OnModuleDestroyed (ShipModule module) {		
		if (targets.Contains(module.gameObject)) {
			targets.Remove (module.gameObject);
			OnTargetStatusChanged (module.gameObject, false);
		}
	}
}
