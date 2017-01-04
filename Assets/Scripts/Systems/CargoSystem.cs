using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CargoSystem : MonoBehaviour {
	
	List<CargoModule> cargoModules;

	int totalMaxWeight;
	int totalCurrentWeight;

	Dictionary<Item, int> totalStoredAmountsByItems;

	void Awake () {
		cargoModules = new List<CargoModule>(GetComponentsInChildren<CargoModule> ()); // =\
	}

	public void TakeItems (Item item, int amount) {
		int itemWeight = GameController.instance.ItemWeightsByItems [item];
		foreach (var cargoModule in cargoModules) {
			int canTakeAmount = cargoModule.FreeWeight / itemWeight;
			int amountToTake = Mathf.Min (amount, canTakeAmount);
			cargoModule.TakeItems (item, amountToTake);
			if (amountToTake <= amount) {
				amount -= amountToTake;
			}
			if (amount == 0) {
				break;
			}
		}
	}

	/*
	 * Ниже - тестовые функции
	 */ 

	public void TakeWater (int amount) {
		TakeItems (Item.Water, amount);
	}

	public void TakeFood (int amount) {
		TakeItems (Item.Food, amount);
	}

	public void TakeIron (int amount) {
		TakeItems (Item.Iron, amount);
	}
}
