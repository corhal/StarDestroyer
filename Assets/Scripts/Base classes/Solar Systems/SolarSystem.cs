using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour {

	public GameObject PlanetPrefab;

	Planet[] planets;
	public TextAsset csv;
	string[,] strings;

	void Start () {
		strings = CSVReader.SplitCsvGrid (csv.text);
		for (int i = 1; i < strings.GetLength(1) - 1; i++) { // Х - хардкодий
			GameObject newPlanetObject = Instantiate (PlanetPrefab) as GameObject;
			Planet newPlanet = newPlanetObject.GetComponent<Planet> ();
			newPlanet.Initialize (strings [0, i], Utility.ParseString (strings [1, i]), Utility.ParseString (strings [2, i]), Utility.ParseString (strings [3, i]), Utility.ParseString (strings [4, i]));
		}
	}
}
