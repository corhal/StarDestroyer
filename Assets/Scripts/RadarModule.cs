using UnityEngine;
using System.Collections;

public class RadarModule : Module {

	//Collider radarCollider;

	public delegate void TargetFoundEventHandler(RadarModule radar, GameObject target);

	public static event TargetFoundEventHandler OnTargetFound;

	void Awake() {
		//radarCollider = GetComponent<SphereCollider> ();
	}

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Module>() != null) {
			OnTargetFound (this, other.gameObject);
		}
	}
}
