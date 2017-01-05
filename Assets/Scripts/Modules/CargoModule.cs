using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CargoModule : MonoBehaviour {

	public int InitialMaxWeight;

	int maxWeight;
	public int MaxWeight { get { return maxWeight; } }
	int currentWeight;
	public int CurrentWeight { get { return currentWeight; } }
	public int FreeWeight {	get { return maxWeight - currentWeight; } }

	Dictionary<Item, int> storedAmountsByItems;
	public Dictionary<Item, int> StoredAmountsByItems { get { return storedAmountsByItems; } }

	void Awake () {
		maxWeight = InitialMaxWeight;
		storedAmountsByItems = new Dictionary<Item, int> ();
		foreach (Item item in System.Enum.GetValues(typeof(Item))) {
			storedAmountsByItems.Add (item, 0);
		}
	}

	public void InitializeFromPlanet () { // дичайший костыль
		if (gameObject.tag != "Player") {
			maxWeight = InitialMaxWeight;
			foreach (Item item in System.Enum.GetValues(typeof(Item))) {
				Debug.Log (item);
				TakeItems (item, 100);
			}
		}
	}

	public void TakeItems(Item item, int amount) {	
		int itemWeight = GameController.instance.ItemWeightsByItems [item];	
		int itemsWeight = itemWeight * amount;
		if (FreeWeight >= itemsWeight) {
			currentWeight += itemsWeight;
			storedAmountsByItems [item] += amount;
		}
	}

	public void GiveItems(Item item, int amount) {
		int itemWeight = GameController.instance.ItemWeightsByItems [item];	
		int itemsWeight = itemWeight * amount;
		if (storedAmountsByItems[item] >= amount) {
			currentWeight -= itemsWeight;
			storedAmountsByItems [item] -= amount;
		}
	}
}
