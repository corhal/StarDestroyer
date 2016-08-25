using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleManager : MonoBehaviour {
	public List<Module> Modules;
	ThrusterModule thruster;
	Rigidbody myBody;

	void Awake() {
		Modules = new List<Module>(GetComponentsInChildren<Module> ());
		thruster = GetComponentInChildren<ThrusterModule> ();
		myBody = GetComponentInChildren<Rigidbody> ();
	}

	void Start() {
		myBody.AddForce (thruster.ThrustForce, ForceMode.Force);
	}
}
