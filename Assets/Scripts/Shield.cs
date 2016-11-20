using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour, IShootable {

	ParticleSystem shieldParticles;

	public int InitialHp;
	int maxHp;
	int hp;

	bool recharging;
	public bool Recharging { get { return recharging; } }

	public float RechargeSpeed;
	float rechargeTimer;

	public delegate void HitEventHandler(Shield sender);
	public static event HitEventHandler OnHit;

	void Awake() {
		shieldParticles = GetComponentInChildren<ParticleSystem> ();
	}

	void Start() {
		maxHp = InitialHp;
		hp = maxHp;
	}

	void Update() {
		if (recharging && hp < maxHp) {
			rechargeTimer += Time.deltaTime;
			if (rechargeTimer >= RechargeSpeed) {
				rechargeTimer = 0.0f;
				hp += 1;
				Debug.Log (hp);
				if (hp >= maxHp) {
					hp = maxHp;
					recharging = false;
				}
			}
		}
	}

	public void TakeDamage(int amount, Vector3 hitPoint) {
		OnHit (this);
		hp -= amount;
		shieldParticles.transform.position = hitPoint;
		shieldParticles.Play ();
		if (hp <= 0) {
			gameObject.SetActive (false);
		}
	}

	public GameObject ShootableGameObject() {
		return gameObject;
	}

	public bool IsAlive() {
		return gameObject.activeSelf;
	}

	public void Recharge() {
		Debug.Log ("Called recharge shield");
		recharging = true;
		rechargeTimer = 0.0f;
	}
}
