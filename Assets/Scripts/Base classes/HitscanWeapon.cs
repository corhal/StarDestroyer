using UnityEngine;
using System.Collections;

public class HitscanWeapon : Weapon {

	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;    
	LineRenderer gunLine;
	GameObject victim;
	Light gunLight;

	void Awake() {
		shootableMask = LayerMask.GetMask("Shootable");
		gunLine = GetComponentInChildren<LineRenderer>();
		gunLight = GetComponentInChildren<Light> ();
	}

	protected override void Shoot() {			
		gunLine.SetVertexCount (base.ShotsAtOnce * 2);

		gunLine.enabled = true;
		gunLight.enabled = true;

		int j = 0;
		for (int i = 0; i < base.ShotsAtOnce; i++) {
			gunLine.SetPosition (j, base.WeaponEnd.position);
			j++;
			shootRay.origin = base.WeaponEnd.position;
			shootRay.direction = new Vector3 (base.WeaponEnd.forward.x + Random.Range (-base.Inaccuracy, base.Inaccuracy), base.WeaponEnd.forward.y, base.WeaponEnd.forward.z + Random.Range (-base.Inaccuracy, base.Inaccuracy));

			if (Physics.Raycast (shootRay, out shootHit, base.Range, shootableMask)) {
				victim = shootHit.collider.gameObject;
				IShootable shootable = victim.GetComponent<IShootable> ();
				if (shootable != null) {				
					shootable.TakeDamage (base.DamagePerShot, shootHit.point);
				} 
				gunLine.SetPosition (j, shootHit.point);	
			} else {
				gunLine.SetPosition (j, shootRay.origin + shootRay.direction * base.Range);
			}
			j++;
		}
	}

	protected override void StopShooting() {
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
}
