using UnityEngine;
using System.Collections;

public class BatteryModule : ShipModule {

	public float InitialMaxEnergy;
	float maxEnergy;
	public float MaxEnergy { get { return maxEnergy; } }
	float currentEnergy;
	public float CurrentEnergy { get { return currentEnergy; } }

	void Awake() {
		maxEnergy = InitialMaxEnergy;
		currentEnergy = 0.0f;
	}

	public void TakeEnergy(float amount) {
		if (currentEnergy + amount <= maxEnergy) {
			currentEnergy += amount;
		} else {
			currentEnergy = maxEnergy;
		}
	}

	public float GiveEnergy(float amount) {
		if (currentEnergy - amount >= 0.0f) {
			currentEnergy -= amount;
			return amount;
		} else {
			amount = currentEnergy;
			currentEnergy = 0.0f;
			return amount;
		}
	}
}
