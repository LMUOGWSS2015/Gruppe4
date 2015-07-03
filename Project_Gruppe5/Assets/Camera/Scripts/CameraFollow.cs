using UnityEngine;
using System.Collections;

/*
 * Die Kamera folt dem Spieler (target).
 */
public class CameraFollow : MonoBehaviour {

	public Transform target; // Der Spieler, den die Kamera verfolgt.
	public float smoothing = 5.0f; // Trägheit der Kamera beim Verfolgen.

	Vector3 offset;

	void Start() {
		transform.position = target.position;
		offset = transform.position - target.position;
	}

	void FixedUpdate() {
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
