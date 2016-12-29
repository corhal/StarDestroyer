using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StorageCell: System.IComparable<StorageCell> {

	public int maxLoad;
	public int currentLoad;
	public int CurrentLoad { get { return currentLoad; } }

	public Item StoredItem;

	public StorageCell (int maxLoad) {
		this.maxLoad = maxLoad;
		currentLoad = 0;
	}

	/// <summary>
	/// Takes amount of items, returns amount that could not be taken.
	/// </summary>
	/// <returns>Amount of items that could not be taken.</returns>
	/// <param name="item">Item.</param>
	/// <param name="amount">Amount.</param>
	public int TakeItems (Item item, int amount) {
		if (item != StoredItem) {
			Debug.Log ("Changing items!");
			currentLoad = 0;
			StoredItem = item;
		}
		int load = item.SpaceTaken * amount;
		int freeSpace = maxLoad - currentLoad;
		int itemsToTake = freeSpace / load;
		currentLoad += itemsToTake * item.SpaceTaken;
		return amount - itemsToTake;
	}

	public int GiveItems (Item item, int amount) {
		if (item != StoredItem) {
			return -1;
		}
		int load = item.SpaceTaken * amount;
		int remnant = Mathf.Max(0, (load - currentLoad) / item.SpaceTaken);

		return remnant;
	}

	public int CompareTo(StorageCell compareCell) {
		// A null value means that this object is greater.
		if (compareCell == null)
			return 1;

		else
			return this.currentLoad.CompareTo(compareCell.currentLoad);
	}
}
