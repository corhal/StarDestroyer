using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour {

	public GameObject PlanetPrefab;
	public GameObject Star;

	Planet[] planets;
	public TextAsset csv;
	string[,] strings;

	void Start () {
		strings = CSVReader.SplitCsvGrid (csv.text);
		for (int i = 1; i < strings.GetLength(1) - 1; i++) { // Х - хардкодий
			GameObject newPlanetObject = Instantiate (PlanetPrefab) as GameObject;
			Planet newPlanet = newPlanetObject.GetComponent<Planet> ();
			newPlanet.Initialize (Star, strings [0, i], Utility.StringToFloat (strings [1, i]), Utility.StringToFloat (strings [2, i]), Utility.StringToFloat (strings [3, i]), Utility.StringToFloat (strings [4, i]));
		}
	}
}
