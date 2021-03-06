﻿using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	string planetName;
	GameObject rotationCenter;

	float size;
	float distanceFromSun; // Должна ли планета знать, где находится солнце? Так-то оно всегда по идее в 0,0,0
	public float rotationSpeed;
	public float movementSpeed;

	public void Initialize (GameObject rotationCenter, string planetName, float size, float distanceFromSun, float rotationSpeed, float movementSpeed) {
		this.rotationCenter = rotationCenter;
		this.planetName = planetName;
		gameObject.name = this.planetName;
		this.size = size;
		this.distanceFromSun = distanceFromSun;
		this.rotationSpeed = rotationSpeed;
		this.movementSpeed = movementSpeed;

		transform.localScale = Vector3.one * this.size;
		transform.position = new Vector3 (this.distanceFromSun, rotationCenter.transform.position.y, this.distanceFromSun);

		if (Random.Range(0.0f, 1.0f) < 2.0f) {
			CreateTradePost ();
		}

		gameObject.layer = LayerMask.NameToLayer ("Navigation");
	}

	void Update () {
		transform.Rotate (transform.up, rotationSpeed * Time.deltaTime); // вращаемся вокруг себя
		Vector3 newPosition = Utility.RotateAroundPivot(rotationCenter.transform.position, transform.position, movementSpeed * Time.deltaTime);
		transform.position = newPosition;
	}

	void CreateTradePost () {
		CargoModule cargoModule = gameObject.AddComponent<CargoModule> ();
		cargoModule.InitialMaxWeight = 9000;
		cargoModule.InitializeFromPlanet ();

		gameObject.AddComponent<CargoSystem> ();
		TradeSystem tradeSystem = gameObject.AddComponent<TradeSystem> ();
		tradeSystem.InitialMoney = 900000;
		gameObject.AddComponent<SystemManager> ();
	}
}
