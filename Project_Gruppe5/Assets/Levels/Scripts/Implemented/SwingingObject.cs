using UnityEngine;
using System.Collections;

public class SwingingObject : InteractivePhysicsObject {

	// The swinging axis
	public AXIS axis;

	// The total swinging angle
	public float totalAngle;

	// The start position of the object (0 = center, positive value = to the right, negative value = to the left)
	public float startAngle;

	// The current swinging direction
	public bool swingingRight;

	// The length of the pendulum
	public float pendulumLength;

	// Factor to multiply with the frequency
	public float frequencyFactor = 1.0f;

	// The current swinging velocity
	private float currentVelocity;

	// Frequency of the swing
	private float frequency;

	// Amplitude of the swing
	private float amplitude;

	// Keeps track of the time
	private float currentTime = 0;

	// Gravity
	private float gravityConstant = 9.81f;

	// Start phase of the rotation
	private float startPhase = 0;

	public enum AXIS {
		X,
		Y,
		Z
	}

	// Initialization
	public override void StartMe () 
	{
		// Calculate the frequency
		frequency = Mathf.Sqrt (gravityConstant / pendulumLength) * frequencyFactor;

		// Calculate the amplitude
		amplitude = (totalAngle / 2);

		// Set the start phase to 180 degrees if the pendulum should start swinging to the left
		if (!swingingRight) {
			startPhase = 180 * Mathf.Deg2Rad;
		} else {
			startPhase = 0;
		}
	}
	
	// Update if the swinging is activated
	public override void DoActivation () 
	{
		currentTime += Time.deltaTime;
		// Calculate the new angle to rotate towards
		float newAngle = amplitude * Mathf.Cos (frequency * currentTime + startPhase) + startAngle * -1;
		//Debug.Log ("New angle: " + newAngle);
		float currentAngle = GetCurrentAngle();
		float deltaAngle = newAngle - currentAngle;
		// Rotate
		rb.MoveRotation(Quaternion.RotateTowards(Quaternion.Euler(AngleToVector(currentAngle)), Quaternion.Euler(AngleToVector(newAngle)), 1.0f));
	}
	
	// Do nothing if the swinging is deactivated
	public override void DoDeactivation ()
	{

	}

	// Returns the current rotation angle in degrees
	public float GetCurrentAngle() {
		float currentDegrees;
		switch (axis) {
		case AXIS.X:
			currentDegrees = transform.rotation.eulerAngles.x;
			break;
		case AXIS.Y:
			currentDegrees = transform.rotation.eulerAngles.y;
			break;
		case AXIS.Z:
			currentDegrees = transform.rotation.eulerAngles.z;
			break;
		default:
			currentDegrees = transform.rotation.eulerAngles.x;
			break;
		}
		return currentDegrees;
	}

	// Returns a Vector3 object for a given angle, depending on the selected rotation axis
	public Vector3 AngleToVector(float angle) {
		Vector3 eulerAngle;
		switch(axis) {
		case AXIS.X:
			eulerAngle = new Vector3(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			break;
		case AXIS.Y:
			eulerAngle = new Vector3(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
			break;
		case AXIS.Z:
			eulerAngle = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
			break;
		default:
			eulerAngle = new Vector3(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			break;
		}
		return eulerAngle;
	}

}
