using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CargoModule : MonoBehaviour {

	StorageCell[] storageCells;

	int InitialStorageCellsAmount;
	int storageCellsAmount;
	int currentLoad;

	Dictionary<Item, int> storedAmountsByItems;

	void Awake () {
		storageCellsAmount = InitialStorageCellsAmount;
	}

	void Start () {
		storageCells = new StorageCell [storageCellsAmount];
		for (int i = 0; i < storageCellsAmount; i++) {
			storageCells[i] = new StorageCell (10);
		}
	}

	public void TakeItems(Item item, int amount) {
		List<StorageCell> suitableCells = new List<StorageCell> ();
		foreach (var storageCell in storageCells) {
			if (storageCell.StoredItem == item || storageCell.StoredItem == null || storageCell.CurrentLoad == 0) {
				suitableCells.Add (storageCell);
			}
		}
		suitableCells.Sort ();
		foreach (var cell in suitableCells) {
			amount = cell.TakeItems (item, amount);
			if (amount == 0) {
				break;
			}
		}
	}

	public void GiveItems(Item item, int amount) {

	}
}
