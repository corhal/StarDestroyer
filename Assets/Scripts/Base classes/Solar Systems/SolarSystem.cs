using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarSystem : MonoBehaviour {

	public GameObject PlanetPrefab;
	public GameObject Star;

	List<Planet> planets;

	void Awake() {
		planets = new List<Planet> ();
	}

	void Start () {
		
	}

	public void LoadSolarSystem(TextAsset csvTable) {
		foreach (var planet in planets) {
			Destroy (planet.gameObject);
		}
		planets.Clear ();
		string[,] strings = CSVReader.SplitCsvGrid (csvTable.text);
		for (int i = 1; i < strings.GetLength(1) - 1; i++) { // Х - хардкодий
			GameObject newPlanetObject = Instantiate (PlanetPrefab) as GameObject;
			Planet newPlanet = newPlanetObject.GetComponent<Planet> ();
			newPlanet.Initialize (Star, strings [0, i], Utility.StringToFloat (strings [1, i]), Utility.StringToFloat (strings [2, i]), Utility.StringToFloat (strings [3, i]), Utility.StringToFloat (strings [4, i]));
			planets.Add (newPlanet);
		}
	}
}
