using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	string name;

	float size;
	float distanceFromSun;
	float rotationSpeed;
	float movementSpeed;

	public void Initialize(string name, float size, float distanceFromSun, float rotationSpeed, float movementSpeed) {
		this.name = name;
		this.size = size;
		this.distanceFromSun = distanceFromSun;
		this.rotationSpeed = rotationSpeed;
		this.movementSpeed = movementSpeed;

		Debug.Log (name);
	}
}
