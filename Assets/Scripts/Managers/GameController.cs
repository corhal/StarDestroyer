using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public int[] InitialWeights;
	public int[] InitialCosts;
	public Dictionary<Item, int> ItemWeightsByItems;
	public Dictionary<Item, int> ItemCostsByItems;

	public TextAsset[] SolarSystemTables;
	public SolarSystem CurrentSolarSystem;

	public static GameController instance;

	void Awake () {
		instance = this;
		ItemWeightsByItems = new Dictionary<Item, int> ();
		ItemCostsByItems = new Dictionary<Item, int> ();
		for (int i = 0; i < InitialWeights.Length; i++) {
			Item item = (Item)i;
			ItemWeightsByItems.Add (item, InitialWeights [i]);
			ItemCostsByItems.Add (item, InitialCosts [i]);
		}
	}

	void Start () {
		CurrentSolarSystem.LoadSolarSystem (SolarSystemTables [0]);
	}

	public void ChangeSolarSystem (TextAsset solarSystemTable) {
		CurrentSolarSystem.LoadSolarSystem (solarSystemTable);
	}
}

public enum Item {
	Water,
	Food,
	Iron,
	Uranium
}
