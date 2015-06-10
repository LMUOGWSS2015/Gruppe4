using UnityEngine;
using System.Collections;

public class CameraRotationX : MonoBehaviour {

	public float rotationSpeed = 1.0f;
	public float minRotation = 10.0f;
	public float maxRotation = 80.0f;

	void FixedUpdate() {
		float rotationAmount = InputManager.RotateX () * InputManager.rotationSensivity * rotationSpeed;

		if (transform.localEulerAngles.x + rotationAmount > maxRotation)
			rotationAmount = maxRotation - transform.localEulerAngles.x;
		else if (transform.localEulerAngles.x + rotationAmount < minRotation)
			rotationAmount = minRotation - transform.localEulerAngles.x;

		transform.Rotate (rotationAmount, 0.0f, 0.0f);
	}
}
