using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystem : MonoBehaviour {

	public delegate void TradeSystemClickedEventHandler(TradeSystem tradeSystem);
	public static event TradeSystemClickedEventHandler OnTradeSystemClicked;

	public int InitialMoney;
	int money;
	public int Money { get { return money; } }

	CargoSystem cargoSystem;
	public Dictionary<Item,float> CostCoefsByItems;

	public Dictionary<Item, int> TotalStoredAmountsByItems { get { return cargoSystem.TotalStoredAmountsByItems; } }
	public int TotalMaxWeight { get { return cargoSystem.TotalMaxWeight; } }
	public int TotalCurrentWeight { get { return cargoSystem.TotalCurrentWeight; } }

	void Awake () {
		CostCoefsByItems = new Dictionary<Item, float> ();
	}

	void Start () {
		money = InitialMoney;
		foreach (Item item in System.Enum.GetValues(typeof(Item))) {
			CostCoefsByItems.Add (item, Random.Range(0.8f, 1.2f));
		}
	}

	public void SubscribeToCargoSystem (CargoSystem cargoSystem) {
		this.cargoSystem = cargoSystem;
	}

	public void BuyItems (Item item, int amount, int costPerUnit) {	
		int transactionCost = amount * costPerUnit;
		if (money >= transactionCost) { // получается, даже если в хранилище влезает меньше, проверяться будет эта цена :(
			int leftoverAmount = cargoSystem.TakeItems (item, amount);
			amount -= leftoverAmount;
			transactionCost = amount * costPerUnit;
			money -= transactionCost;
		}
	}

	public bool CheckIfCanBuy (Item item, int amount, int costPerUnit) {
		if (cargoSystem.CheckIfCanTake(item, amount) && money >= (amount * costPerUnit)) {
			return true;
		}
		return false;
	}

	public void SellItems (Item item, int amount, int costPerUnit) {
		int leftoverAmount = cargoSystem.GiveItems (item, amount);
		amount -= leftoverAmount;
		int transactionCost = (amount * costPerUnit);
		money += transactionCost;
	}

	public bool CheckIfCanSell (Item item, int amount) {
		if (cargoSystem.CheckIfCanGive(item, amount)) {
			return true;
		}
		return false;
	}

	void OnMouseDown () {
		OnTradeSystemClicked (this);
	}
}
