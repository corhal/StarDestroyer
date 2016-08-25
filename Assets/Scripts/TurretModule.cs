using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretModule : Module {

	List<GameObject> targets;
	GameObject target;
	IShooter weapon;
	Dictionary<float, GameObject> distances;

	void Awake() {
		weapon = GetComponentInChildren<IShooter> ();
		distances = new Dictionary<float, GameObject> ();
		targets = new List<GameObject> ();
		Module.OnModuleDestroyed += Module_OnModuleDestroyed;
	}

	void Module_OnModuleDestroyed (Module module) {
		if (target == module.gameObject) {
			target = null;
		}
		if (targets.Contains(module.gameObject)) {
			Debug.Log ("removing target");
			RemoveTarget (module.gameObject);
		}
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

	public void RemoveTarget(GameObject newTarget) {
		targets.Remove (newTarget);

		UpdateTargets();
	}

	public void AddTarget(GameObject newTarget) {
		targets.Add (newTarget);

		UpdateTargets ();
	}

	void UpdateTargets() {	
		List<GameObject> targetsToRemove = new List<GameObject> ();

		foreach (var targetObj in targets) {
			if (targetObj == null || !targetObj.GetComponent<Module>().Alive) {
				targetsToRemove.Add (targetObj);
			}
		}

		foreach (var targetObj in targetsToRemove) {
			targets.Remove (targetObj);
		}

		if (target == null && targets.Count > 0) {
			distances.Clear ();
			foreach (var targetObj in targets) {
				distances.Add (Vector3.Distance (transform.position, targetObj.transform.position), targetObj);
			}
			float[] distanceValues = new float[distances.Count];
			distances.Keys.CopyTo (distanceValues, 0);
			float minDistance = Mathf.Min (distanceValues);
			target = distances [minDistance];
			weapon.ToggleShooting (true);
		}
	}
}
