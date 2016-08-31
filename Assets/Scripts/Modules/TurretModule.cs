using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretModule : ShipModule {

	GameObject target;
	public GameObject Target { get { return target; } }

	IShooter weapon;

	void Awake() {
		weapon = GetComponentInChildren<IShooter> ();
	}

	void Update() {
		if (target != null) {
			transform.LookAt (target.transform.position);
		} else {
			if (weapon.IsShooting()) {
				weapon.ToggleShooting (false);
			}
		}
	}

	/// <summary>
	/// Sets the target and starts shooting if it is not null.
	/// </summary>
	/// <param name="newTarget">New target.</param>
	public void SetTarget(GameObject newTarget) {
		target = newTarget;
		if (target != null) {
			weapon.ToggleShooting (true);
		}
	}
}
