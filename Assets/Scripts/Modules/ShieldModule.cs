using UnityEngine;
using System.Collections;

public class ShieldModule : ShipModule {

	Shield shield;
	public float RechargeCooldown;

	float rechargeTimer;
	bool canRecharge = true;

	void Awake() {
		shield = GetComponentInChildren<Shield> ();
		Shield.OnHit += Shield_OnHit;
	}

	void Update() {
		if (!canRecharge) {
			rechargeTimer += Time.deltaTime;
			if (rechargeTimer >= RechargeCooldown && !shield.Recharging) {
				canRecharge = true;
				if (!shield.gameObject.activeSelf) {
					shield.gameObject.SetActive (true);
				}
				shield.Recharge ();
			}
		}
	}

	void Shield_OnHit (Shield sender) {
		if (shield == sender) {
			rechargeTimer = 0;
			canRecharge = false;
		}
	}
}
