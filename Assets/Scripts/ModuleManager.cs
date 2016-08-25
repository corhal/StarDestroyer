using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleManager : MonoBehaviour {
	public List<Module> Modules;
	ThrusterModule thruster;
	TurretModule turret;
	Rigidbody myBody;

	void Awake() {
		Modules = new List<Module>(GetComponentsInChildren<Module> ());
		thruster = GetComponentInChildren<ThrusterModule> ();
		turret = GetComponentInChildren<TurretModule> ();
		myBody = GetComponent<Rigidbody> ();
		RadarModule.OnTargetFound += RadarModule_OnTargetFound;
	}

	void RadarModule_OnTargetFound (RadarModule radar, GameObject target) {
		if (Modules.Contains(radar)) {
			turret.AddTarget (target);
		}
	}

	void Start() {
		thruster.Push (myBody);
	}
}
