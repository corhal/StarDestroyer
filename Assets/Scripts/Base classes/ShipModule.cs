using UnityEngine;
using System.Collections;

public abstract class ShipModule : MonoBehaviour, IShootable {	
	
	public int StartingHp;
	public string Name { get { return this.ToString(); } }
	public bool Alive { get { return operational; } } // Интересно, почему Alive возвращает operational
	int maxHp;
	int hp;
	protected bool operational;

	public delegate void ModuleDestroyedEventHandler(ShipModule module);

	public static event ModuleDestroyedEventHandler OnModuleDestroyed;
	
	void Start () {
		maxHp = StartingHp;
		hp = maxHp;
		operational = true;
	}

	public void TakeDamage(int amount, Vector3 hitPoint) {			
		hp -= amount;
		if (hp <= 0) {
			hp = 0;
			operational = false;
			Die ();
		}
	}

	public bool IsAlive() {
		return operational;
	}

	public GameObject ShootableGameObject() {
		return gameObject;
	}

	void Die() {
		OnModuleDestroyed (this);
		Destroy (gameObject); // temporary
	}
}
