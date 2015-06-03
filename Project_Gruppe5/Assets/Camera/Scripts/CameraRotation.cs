using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	public float rotateSpeed = 3.0f;

	void FixedUpdate() {

		// Freie Rotation
		transform.Rotate (0.0f, Input.GetAxis("Rotate") * rotateSpeed, 0.0f);

		// 4-Stufen Rotation
		/*
		if (Input.GetKeyDown (KeyCode.Q)) {
			transform.Rotate (0.0f, 90.0f, 0.0f);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			transform.Rotate (0.0f, -90.0f, 0.0f);
		}
		*/

		// 8-Stufen Rotation
		/*
		if (Input.GetKeyDown (KeyCode.Q)) {
			transform.Rotate (0.0f, 45.0f, 0.0f);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			transform.Rotate (0.0f, -45.0f, 0.0f);
		}
		*/
	}
}
