using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ShipSystem : MonoBehaviour {

	protected List<ShipModule> modules;

	void Start() {
		modules = new List<ShipModule>(GetComponentsInChildren<ShipModule> ()); // Я еще поблагодарю себя за это решение...
	}
}
