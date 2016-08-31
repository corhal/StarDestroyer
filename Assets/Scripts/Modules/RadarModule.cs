using UnityEngine;
using System.Collections;

public class RadarModule : ShipModule {

	public delegate void TargetChangedEventHandler(RadarModule radar, GameObject target, bool found);
	public static event TargetChangedEventHandler OnTargetChanged;

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<ShipModule>() != null) {			
			OnTargetChanged (this, other.gameObject, true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<ShipModule>() != null) {			
			OnTargetChanged (this, other.gameObject, false);
		}
	}
}
