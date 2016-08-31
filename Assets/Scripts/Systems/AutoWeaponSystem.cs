using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoWeaponSystem : ShipSystem {
	
	List<GameObject> targets;
	List<TurretModule> turrets;

	Dictionary<float, GameObject> distances;

	void Awake() {
		turrets = new List<TurretModule>(GetComponentsInChildren<TurretModule> ()); // =\
		targets = new List<GameObject>();
		distances = new Dictionary<float, GameObject> ();
	}

	public void UpdateTargets(GameObject target, bool found) {	
		if (found) {
			targets.Add (target);
		} else {
			targets.Remove (target);
		}

		//List<GameObject> targetsToRemove = new List<GameObject> ();

		/*foreach (var targetObj in targets) {
			if (targetObj == null || !targetObj.GetComponent<ShipModule>().Alive) {
				targetsToRemove.Add (targetObj);
			}
		}

		foreach (var targetObj in targetsToRemove) {
			targets.Remove (targetObj);
			Debug.Log ("Smth got removed via AutoWeaponSystem disposer");
		}*/

		foreach (var turret in turrets) {
			if (turret.Target != null && !turret.Target.GetComponent<ShipModule>().Alive || !targets.Contains(turret.Target)) {
				turret.SetTarget (null);
			}

			if (turret.Target == null && targets.Count > 0) {
				distances.Clear ();
				foreach (var targetObj in targets) {
					distances.Add (Vector3.Distance (turret.transform.position, targetObj.transform.position), targetObj);
				}
				float[] distanceValues = new float[distances.Count];
				distances.Keys.CopyTo (distanceValues, 0);
				float minDistance = Mathf.Min (distanceValues);
				turret.SetTarget(distances[minDistance]);
			}
		}
	}
}
