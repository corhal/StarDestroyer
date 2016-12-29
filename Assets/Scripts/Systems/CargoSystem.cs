using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CargoSystem : MonoBehaviour {

	public int DefaultItemsInOneStorageCell;

	List<CargoModule> cargoModules;

	int totalMaxLoad;
	int totalCurrentLoad;

	Dictionary<Item, int> totalStoredAmountsByItems;

	void Awake () {
		cargoModules = new List<CargoModule>(GetComponentsInChildren<CargoModule> ()); // =\
	}
}
