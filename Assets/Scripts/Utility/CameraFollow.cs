using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing;
	public bool PhysicalMovement;
    //public Vector3 playerPos;

    private Vector3 offset;

    void Start() {        
        offset = transform.position - target.position;
    }

    void FixedUpdate() {      
		if (!PhysicalMovement) {
			return;
		}
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

	void Update() {
		if (PhysicalMovement) {
			return;
		}
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
