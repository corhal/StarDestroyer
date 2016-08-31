using UnityEngine;
using System.Collections;

public class ReactorModule : ShipModule {

	public float InitialOutputPerSecond;
	float outputPerSecond;

	void Awake() {
		outputPerSecond = InitialOutputPerSecond;
	}

	public float ProduceEnergy() {
		return outputPerSecond * Time.deltaTime;
	}
}
