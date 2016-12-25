using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	string planetName;
	GameObject rotationCenter;

	float size;
	float distanceFromSun; // Должна ли планета знать, где находится солнце? Так-то оно всегда по идее в 0,0,0
	float rotationSpeed;
	float movementSpeed;

	public void Initialize (GameObject rotationCenter, string planetName, float size, float distanceFromSun, float rotationSpeed, float movementSpeed) {
		this.rotationCenter = rotationCenter;
		this.planetName = planetName;
		gameObject.name = this.planetName;
		this.size = size;
		this.distanceFromSun = distanceFromSun;
		this.rotationSpeed = rotationSpeed;
		this.movementSpeed = movementSpeed;

		transform.localScale = Vector3.one * this.size;
		transform.position = new Vector3 (this.distanceFromSun, transform.position.y, this.distanceFromSun);
	}

	void Update () {
		transform.Rotate (transform.up, rotationSpeed * Time.deltaTime); // вращаемся вокруг себя
		transform.Rotate (rotationCenter.transform.up, movementSpeed * Time.deltaTime); // летим вокруг солнца
	}
}
