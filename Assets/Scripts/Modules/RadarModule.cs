using UnityEngine;
using System.Collections;

public class RadarModule : ShipModule {

	float radarRadius;

	public float InitialRadarRadius;

	SphereCollider radarCollider;

	//public delegate void TargetStatusChangedEventHandler(GameObject target, bool found);
	public event TargetStatusChangedEventHandler OnTargetStatusChanged;

	void Awake() {
		radarCollider = gameObject.GetComponent<SphereCollider> ();
		radarRadius = InitialRadarRadius;
	}

	void Start() {
		radarCollider.radius = radarRadius;
	}

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<ShipModule>() != null) {			
			OnTargetStatusChanged (other.gameObject, true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<ShipModule>() != null) {			
			OnTargetStatusChanged (other.gameObject, false);
		}
	}
}
