using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeWindow : MonoBehaviour {

	public Text InfoLabel;
	public GameObject TradeElementPrefab;
	public GameObject Window;
	public Transform TradeElementsParent;
	public List<GameObject> TradeElementObjects;
	public List<TradeElement> TradeElements;

	void Awake () {
		TradeSystem.OnTradeSystemClicked += TradeSystem_OnTradeSystemClicked;
		TradeElementObjects = new List<GameObject> ();
		TradeElements = new List<TradeElement> ();
	}

	void TradeSystem_OnTradeSystemClicked (TradeSystem tradeSystem) {		
		if (tradeSystem.gameObject.tag != "Player") {
			Window.SetActive (true);
			UpdateWindow (tradeSystem);
		}
	}

	public void UpdateWindowInfo () {
		TradeSystem player = null;
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Player");
		foreach (var obj in objects) {
			if (obj.GetComponent<TradeSystem> () != null) {
				player = obj.GetComponent<TradeSystem> ();
			}
		}
		InfoLabel.text = "Cargo: " + player.TotalCurrentWeight + "/" + player.TotalMaxWeight + "    $" + player.Money;
	}

	public void UpdateWindow (TradeSystem trader) {		
		TradeSystem player = null;
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Player");
		foreach (var obj in objects) {
			if (obj.GetComponent<TradeSystem> () != null) {
				player = obj.GetComponent<TradeSystem> ();
			}
		}
		InfoLabel.text = "Cargo: " + player.TotalCurrentWeight + "/" + player.TotalMaxWeight + "    $" + player.Money;
		foreach (var tradeElementObject in TradeElementObjects) {			
			Destroy (tradeElementObject);
		}
		TradeElementObjects.Clear ();
		TradeElements.Clear ();
		List<Item> items = new List<Item> ();
		foreach (var amountByItem in player.TotalStoredAmountsByItems) {
			if (amountByItem.Value != 0 && !items.Contains(amountByItem.Key)) {
				items.Add (amountByItem.Key);
			}
		}
		foreach (var amountByItem in trader.TotalStoredAmountsByItems) {
			if (amountByItem.Value != 0 && !items.Contains(amountByItem.Key)) {
				items.Add (amountByItem.Key);
			}
		}
		foreach (var item in items) {
			GameObject newTradeElementObject = Instantiate (TradeElementPrefab, TradeElementsParent);
			newTradeElementObject.transform.localScale = Vector2.one;
			TradeElement newTradeElement = newTradeElementObject.GetComponent<TradeElement> ();
			newTradeElement.ItemToTrade = item;
			newTradeElement.PlayerTrader = player;
			newTradeElement.Trader = trader;
			TradeElementObjects.Add (newTradeElementObject);
			TradeElements.Add (newTradeElement);
		}
	}
}
