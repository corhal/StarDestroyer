using UnityEngine;
using System.Collections;

public abstract class ShipModule : MonoBehaviour {	
	public int StartingHp;
	int maxHp;
	int hp;
	bool operational;

	// Use this for initialization
	void Start () {
		maxHp = StartingHp;
		hp = maxHp;
		operational = true;
	}
	
	public void TakeDamage(int damage) {
		hp -= damage;
		if () {
			
		}
	}
}
