using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretModule : ShipModule {

	IShootable target;
	public IShootable Target { get { return target; } }

	Weapon weapon;

	void Awake() {
		weapon = GetComponentInChildren<Weapon> ();
	}

	void Update() {
		if (target != null) {
			transform.LookAt (target.ShootableGameObject().transform.position);
		} else {
			if (weapon.IsShooting) {
				weapon.ToggleShooting (false);
			}
		}
	}

	/// <summary>
	/// Sets the target and starts shooting if it is not null.
	/// </summary>
	/// <param name="newTarget">New target.</param>
	public void SetTarget(IShootable newTarget) {
		target = newTarget;
		if (target != null) {
			weapon.ToggleShooting (true);
		}
	}
}
