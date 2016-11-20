using UnityEngine;
using System.Collections;

public class BeamCannon : MonoBehaviour, IShooter {

	public GameObject GunPoint;

	public int damagePerShot = 1;                   // The damage inflicted by each bullet.
	public float timeBetweenBullets = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.

	bool shooting;
	float timer;                                    // A timer to determine when to fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	LineRenderer gunLine;                           // Reference to the line renderer.
	Light gunLight;                                 // Reference to the light component.
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

	void Awake () {
		shootableMask = LayerMask.GetMask ("Shootable");
		gunLine = GetComponentInChildren <LineRenderer> ();
		gunLight = GetComponentInChildren <Light> ();
	}

	public bool IsShooting() {
		return shooting;
	}

	public void ToggleShooting(bool shouldShoot) {
		shooting = shouldShoot;
	}

	void Update () {		
		timer += Time.deltaTime;

		if(shooting && timer >= timeBetweenBullets) {
			Shoot ();
		}

		if(timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}
	}

	public void DisableEffects () {
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	void Shoot () {
		timer = 0f;

		gunLight.enabled = true;

		gunLine.enabled = true;
		gunLine.SetPosition (0, GunPoint.transform.position);

		shootRay.origin = GunPoint.transform.position;
		shootRay.direction = GunPoint.transform.forward;

		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
			IShootable taker = shootHit.collider.GetComponent <IShootable> ();

			if(taker != null) {
				taker.TakeDamage (damagePerShot, shootHit.point);
			}

			gunLine.SetPosition (1, shootHit.point);
		}
		else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
