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

	void Awake () {
		maxWeight = InitialMaxWeight;
		storedAmountsByItems = new Dictionary<Item, int> ();
		foreach (Item item in System.Enum.GetValues(typeof(Item))) {
			storedAmountsByItems.Add (item, 0);
		}
	}

	void Start () {
		
	}

	public void TakeItems(Item item, int amount) {	
		int itemWeight = GameController.instance.ItemWeightsByItems [item];	
		int itemsWeight = itemWeight * amount;
		if (FreeWeight >= itemsWeight) {
			currentWeight += itemsWeight;
			storedAmountsByItems [item] += amount;
		}

		Debug.Log (item + ": " + storedAmountsByItems [item]);
	}

	public void GiveItems(Item item, int amount) {

	}
}
