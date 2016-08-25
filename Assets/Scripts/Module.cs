using UnityEngine;
using System.Collections;

public abstract class Module : MonoBehaviour {	
	public int StartingHp;
	public bool Alive { get { return operational; } }
	int maxHp;
	int hp;
	protected bool operational;

	public delegate void ModuleDestroyedEventHandler(Module module);

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

	void Die() {
		OnModuleDestroyed (this);
		Destroy (gameObject); // temporary
	}
}
