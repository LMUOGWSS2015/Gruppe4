using UnityEngine;
using System.Collections;

/*
 * Rotiert die Kamera um die X-Achse.
 */
public class CameraRotationX : MonoBehaviour {

	public float rotationSpeed = 1.0f;
	public float minRotation = 10.0f; // Minimaler Rotationswinkel vom Boden.
	public float maxRotation = 80.0f; // Maximaler Rotationswinkel vom Boden.

	void FixedUpdate() {
		float rotationAmount = InputManager.RotateX () * InputManager.rotationSensivity * rotationSpeed;

		if (transform.localEulerAngles.x + rotationAmount > maxRotation)
			rotationAmount = maxRotation - transform.localEulerAngles.x;
		else if (transform.localEulerAngles.x + rotationAmount < minRotation)
			rotationAmount = minRotation - transform.localEulerAngles.x;

		transform.Rotate (rotationAmount, 0.0f, 0.0f);
	}
}
