using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectionSystem : ShipSystem {
	
	List<GameObject> targets;

	public delegate void TargetsChangedEventHandler(DetectionSystem sender, GameObject target, bool found);
	public static event TargetsChangedEventHandler OnTargetsChanged;

	void Awake() {		
		RadarModule.OnTargetChanged += RadarModule_OnTargetChanged;
		ShipModule.OnModuleDestroyed += ShipModule_OnModuleDestroyed; // не очень изящно, по-хорошему это должен делать радар
		targets = new List<GameObject> ();
	}

	void ShipModule_OnModuleDestroyed (ShipModule module) {		
		if (targets.Contains(module.gameObject)) {
			targets.Remove (module.gameObject);
			OnTargetsChanged (this, module.gameObject, false);
		}
	}

	void RadarModule_OnTargetChanged (RadarModule radar, GameObject target, bool found) {		
		if (modules.Contains(radar)) {			
			if (!targets.Contains(target) && found) {
				targets.Add (target);
				OnTargetsChanged (this, target, true);
			} else {
				Debug.Log ("removing target from radar system");
				targets.Remove (target);
				OnTargetsChanged (this, target, false);
			}
		}
	}
}
