﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeElement : MonoBehaviour {

	public Text ItemLabel;
	public Text CostLabel;
	public Text PlayersAmountLabel;
	public Text TradersAmountLabel;

	public Text TradeLabel;

	public InputField AmountInput;

	public TradeSystem PlayerTrader;
	public TradeSystem Trader;

	public Item ItemToTrade;
	public int AmountToTrade;
	public int CostPerUnit;

	public void SetAmountFromInputField () {
		float floatAmount = Utility.StringToFloat (AmountInput.text);
		AmountToTrade = (int)floatAmount;
		UpdateElement ();
	}

	void Start () {
		CostPerUnit = (int)((float) GameController.instance.ItemCostsByItems [ItemToTrade] * Trader.CostCoefsByItems [ItemToTrade]);
		CostLabel.text = "$" + CostPerUnit;
		ItemLabel.text = ItemToTrade.ToString ();
		UpdateElement ();
	}

	public void ChangeInput (int amount) {
		float floatAmount = 0;
		if (AmountInput.text != "") {
			floatAmount = Utility.StringToFloat (AmountInput.text);
		}
		int num = (int)floatAmount;
		num += amount;
		AmountInput.text = num.ToString ();
		SetAmountFromInputField ();
	}

	public void Trade () {
		if (AmountToTrade >= 0) {
			Buy (AmountToTrade);
		} else {
			Sell (-AmountToTrade);
		}
		UpdateElement ();
	}

	void Buy (int amount) {
		if (PlayerTrader.CheckIfCanBuy(ItemToTrade, amount, CostPerUnit) && Trader.CheckIfCanSell(ItemToTrade, amount)) {
			PlayerTrader.BuyItems (ItemToTrade, amount, CostPerUnit);
			Trader.SellItems (ItemToTrade, amount, CostPerUnit);
		}
	}

	void Sell (int amount) {
		if (PlayerTrader.CheckIfCanSell(ItemToTrade, amount) && Trader.CheckIfCanBuy(ItemToTrade, amount, CostPerUnit)) {
			PlayerTrader.SellItems (ItemToTrade, amount, CostPerUnit);
			Trader.BuyItems (ItemToTrade, amount, CostPerUnit);
		}
	}

	void UpdateElement () {
		PlayersAmountLabel.text = PlayerTrader.TotalStoredAmountsByItems [ItemToTrade].ToString();
		TradersAmountLabel.text = Trader.TotalStoredAmountsByItems [ItemToTrade].ToString();
		if (AmountToTrade >= 0) {
			TradeLabel.text = "Buy";
		} else {
			TradeLabel.text = "Sell";
		}

		// dirty hax time

		GetComponentInParent<TradeWindow> ().UpdateWindowInfo ();
	}
}
