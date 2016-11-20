using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoWeaponSystem : ShipSystem {
	
	List<IShootable> targets;
	List<TurretModule> turrets;

	Dictionary<IShootable, float> distancesByTargets;

	void Awake() {
		turrets = new List<TurretModule>(GetComponentsInChildren<TurretModule> ()); // =\
		targets = new List<IShootable>();
		distancesByTargets = new Dictionary<IShootable, float> ();
	}

	public void SubscribeToDetectionSystem(DetectionSystem detectionSystem) {
		detectionSystem.OnTargetStatusChanged += DetectionSystem_OnTargetStatusChanged;
	}

	public void UnsubscribeFromDetectionSystem(DetectionSystem detectionSystem) {
		detectionSystem.OnTargetStatusChanged -= DetectionSystem_OnTargetStatusChanged;
	}

	void DetectionSystem_OnTargetStatusChanged (GameObject target, bool found)	{
		IShootable shootable = target.GetComponent<IShootable> ();
		if (shootable != null) {			
			UpdateTargetStatus (shootable, found);
		}
	}

	void UpdateTargetStatus(IShootable target, bool found) {
		if (found) {
			targets.Add (target);
		} else {
			targets.Remove (target);
		}

		UpdateTargets ();
	}

	void UpdateTargets() {
		foreach (var turret in turrets) {
			if (turret.Target != null && !turret.Target.IsAlive() || !targets.Contains(turret.Target)) {
				turret.SetTarget (null);
			}

			if (turret.Target == null && targets.Count > 0) {
				distancesByTargets.Clear ();
				foreach (var target in targets) {
					distancesByTargets.Add (target, Vector3.Distance (turret.transform.position, target.ShootableGameObject().transform.position));
				}

				turret.SetTarget(FindClosestTarget());
			}
		}
	}

	IShootable FindClosestTarget() {
		IShootable closestTarget = null;
		float closestDistance = 1000.0f;
		foreach (var distanceByTarget in distancesByTargets) {
			closestTarget = (distanceByTarget.Value < closestDistance) ? distanceByTarget.Key : closestTarget;
		}
		return closestTarget;
	}
}
