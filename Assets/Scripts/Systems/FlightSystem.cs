using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class FlightSystem : ShipSystem {

	List<ThrusterModule> thrusters;

	public bool DirectControl;

	bool isLanded;
	float maxSpeed;
	float maxRotationSpeed;

	float moveHorizontal;
	float moveVertical;

	public Rigidbody MyBody; // :(
	NavMeshAgent navMeshAgent;
	Planet targetPlanet;

	void Awake () {
		thrusters = new List<ThrusterModule>(GetComponentsInChildren<ThrusterModule> ()); // =\
		navMeshAgent = GetComponentInParent<NavMeshAgent> ();
		maxSpeed = thrusters [0].Thrust / 10.0f;
		maxRotationSpeed = thrusters [0].Thrust / 35.0f;
	}

	void Update () {
		if (DirectControl) {
			return;
		}
		if (targetPlanet != null) {
			navMeshAgent.destination = targetPlanet.transform.position;
		}
		if (Input.GetMouseButtonDown(0) && !Utility.IsPointerOverUIObject()) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100, LayerMask.GetMask("Navigation"))) {
				targetPlanet = null;
				isLanded = false;
				if (hit.collider.gameObject.GetComponent<Planet> () != null) {
					targetPlanet = hit.collider.gameObject.GetComponent<Planet> ();
					navMeshAgent.destination = targetPlanet.transform.position;
				} else {
					navMeshAgent.destination = hit.point;
				}
				navMeshAgent.Resume();
			}
		}

		if (!isLanded && targetPlanet != null && navMeshAgent.remainingDistance <= (navMeshAgent.stoppingDistance + 1.0f)) {
			//if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon) {
				LandOnPlanet (targetPlanet);
			//}				
		}
	}

	void LandOnPlanet (Planet planet) {
		isLanded = true;
		if (planet.GetComponent<TradeSystem> () != null) {
			planet.GetComponent<TradeSystem> ().StartTrade ();
		}
	}

	void FixedUpdate () {
		if (!DirectControl) {
			return;
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
			moveVertical = Input.GetAxis ("Vertical");
		} else if (moveVertical != 0.0f) {
			if (moveVertical > 0.0f) {
				moveVertical -= 0.02f;
			}
			if (moveVertical < 0.0f) {
				moveVertical += 0.02f;
			}
			if ((int)(moveVertical * 100) == 0) {
				moveVertical = 0.0f;
			}
		}
		moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (transform.forward.x * moveVertical * maxSpeed, 0.0f, transform.forward.z * moveVertical * maxSpeed);
		MyBody.velocity = movement;
		MyBody.angularVelocity = new Vector3 (0.0f, moveHorizontal * maxRotationSpeed, 0.0f);
	}
}
