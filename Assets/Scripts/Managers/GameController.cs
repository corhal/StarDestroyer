using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public TextAsset[] SolarSystemTables;
	public SolarSystem CurrentSolarSystem;

	void Start () {
		CurrentSolarSystem.LoadSolarSystem (SolarSystemTables [0]);
	}

	public void ChangeSolarSystem (TextAsset solarSystemTable) {
		CurrentSolarSystem.LoadSolarSystem (solarSystemTable);
	}
}
