using UnityEngine;
using System.Collections;

public abstract class Module : MonoBehaviour {	
	public int StartingHp;
	int maxHp;
	int hp;
	protected bool operational;
	
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
			Destroy (gameObject);
		}
	}
}
