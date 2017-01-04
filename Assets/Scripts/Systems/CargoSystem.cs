using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CargoSystem : MonoBehaviour {
	
	List<CargoModule> cargoModules;

	int totalMaxWeight;
	public int TotalMaxWeight { get { return totalMaxWeight; } }
	int totalCurrentWeight;
	public int TotalCurrentWeight { get { return totalCurrentWeight; } }

	Dictionary<Item, int> totalStoredAmountsByItems;
	public Dictionary<Item, int> TotalStoredAmountsByItems { get { return totalStoredAmountsByItems; } }

	void Awake () {
		cargoModules = new List<CargoModule>(GetComponentsInChildren<CargoModule> ()); // =\
		totalStoredAmountsByItems = new Dictionary<Item, int> ();
		foreach (Item item in System.Enum.GetValues(typeof(Item))) {
			totalStoredAmountsByItems.Add (item, 0);
		}
		UpdateInfo ();
	}

	void Start() {
		totalMaxWeight = 0;
		foreach (var cargoModule in cargoModules) {
			totalMaxWeight += cargoModule.MaxWeight;
		}
	}

	/// <summary>
	/// Tries to take amount of items. Returns leftover amount.
	/// </summary>
	/// <returns>Leftover.</returns>
	/// <param name="item">Item.</param>
	/// <param name="amount">Amount.</param>
	public int TakeItems (Item item, int amount) {
		int itemWeight = GameController.instance.ItemWeightsByItems [item];
		foreach (var cargoModule in cargoModules) {
			int canTakeAmount = cargoModule.FreeWeight / itemWeight;
			int amountToTake = Mathf.Min (amount, canTakeAmount);
			cargoModule.TakeItems (item, amountToTake);
			totalCurrentWeight += itemWeight * amountToTake;
			if (amountToTake <= amount) {
				amount -= amountToTake;
			}
			if (amount == 0) {
				break;
			}
		}
		UpdateInfo ();
		return amount;
	}

	public bool CheckIfCanTake (Item item, int amount) {
		int itemWeight = GameController.instance.ItemWeightsByItems [item];
		foreach (var cargoModule in cargoModules) {
			int canTakeAmount = cargoModule.FreeWeight / itemWeight;
			int amountToTake = Mathf.Min (amount, canTakeAmount);
			if (amountToTake <= amount) {
				amount -= amountToTake;
			}
			if (amount == 0) {
				break;
			}
		}
		return amount == 0;
	}

	/// <summary>
	/// Tries to give amount of items. Returns leftover amount.
	/// </summary>
	/// <returns>Leftover amount.</returns>
	/// <param name="item">Item.</param>
	/// <param name="amount">Amount.</param>
	public int GiveItems (Item item, int amount) {
		int itemWeight = GameController.instance.ItemWeightsByItems [item];
		foreach (var cargoModule in cargoModules) {
			int canGiveAmount = cargoModule.StoredAmountsByItems[item];
			int amountToGive = Mathf.Min (amount, canGiveAmount);
			cargoModule.GiveItems (item, amountToGive);
			totalCurrentWeight -= itemWeight * amountToGive;
			if (amountToGive <= amount) {
				amount -= amountToGive;
			}
			if (amount == 0) {
				break;
			}
		}
		UpdateInfo ();
		return amount;
	}

	public bool CheckIfCanGive (Item item, int amount) {
		foreach (var cargoModule in cargoModules) {
			int canGiveAmount = cargoModule.StoredAmountsByItems[item];
			int amountToGive = Mathf.Min (amount, canGiveAmount);
			if (amountToGive <= amount) {
				amount -= amountToGive;
			}
			if (amount == 0) {
				break;
			}
		}
		return amount == 0;
	}

	void UpdateInfo () {
		foreach (Item item in System.Enum.GetValues(typeof(Item))) {
			totalStoredAmountsByItems [item] = 0;
		}
		foreach (var cargoModule in cargoModules) {
			foreach (var amountByItem in cargoModule.StoredAmountsByItems) {
				totalStoredAmountsByItems [amountByItem.Key] = amountByItem.Value;
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
