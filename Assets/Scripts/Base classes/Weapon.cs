using UnityEngine;
using System.Collections;

public class Weapon: MonoBehaviour {

	public Transform WeaponEnd;
	public int DamagePerShot;
	public int ShotsAtOnce;

	public float TimeBetweenShots;
	public float Range;
	public float Inaccuracy;

	public bool IsShooting;
	float timer;    

	public float EffectsDisplayTime;

	void Update() {
		timer += Time.deltaTime;

		if (IsShooting && timer >= TimeBetweenShots && Time.timeScale != 0) {
			timer = 0f;
			Shoot();
		}

		if (timer >= TimeBetweenShots * EffectsDisplayTime) {
			StopShooting ();
		}
	}

	public void ToggleShooting(bool shouldShoot) {
		IsShooting = shouldShoot;
	}

	protected virtual void Shoot() {
		
	}

	protected virtual void StopShooting() {

	}
}
