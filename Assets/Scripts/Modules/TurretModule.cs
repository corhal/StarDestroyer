using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretModule : ShipModule {

	GameObject target;
	public GameObject Target { get { return target; } }
	IShooter weapon;

	void Awake() {
		weapon = GetComponentInChildren<IShooter> ();
		//ShipModule.OnModuleDestroyed += Module_OnModuleDestroyed;
	}

	void Update() {
		if (target != null) {
			transform.LookAt (target.transform.position);
			//Debug.Log(Quaternion.LookRotation(transform.position - target.transform.position));
		} else {
			if (weapon.IsShooting()) {
				weapon.ToggleShooting (false);
			}
		}
	}

	public void SetTarget(GameObject newTarget) {
		target = newTarget;
		if (target != null) {
			weapon.ToggleShooting (true);
		}
	}
}
