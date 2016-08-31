using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergySystem : ShipSystem {

	List<ReactorModule> reactors;
	List<BatteryModule> batteries;

	float totalOutputPerUpdate;
	float maxEnergy;
	float currentEnergy;

	void Awake() {
		reactors = new List<ReactorModule> (GetComponentsInChildren<ReactorModule> ());
		batteries = new List<BatteryModule> (GetComponentsInChildren<BatteryModule> ());
	}

	void Start() {
		foreach (var reactor in reactors) {
			totalOutputPerUpdate += reactor.ProduceEnergy ();
		}
		foreach (var battery in batteries) {
			maxEnergy += battery.MaxEnergy;
		}
	}

	void Update() {
		foreach (var battery in batteries) { // Как правильно - заряжать все батареи понемногу одновременно, или быстро по очереди?
			Debug.Log ("Reactor output: " + totalOutputPerUpdate.ToString());
			if (battery.CurrentEnergy < battery.MaxEnergy) { 
				battery.TakeEnergy(totalOutputPerUpdate);
				break; // Для начала попробуем быстро по очереди
			}
		}
		foreach (var battery in batteries) {
			Debug.Log (battery.CurrentEnergy + "/" + battery.MaxEnergy);
		}
	}
}
