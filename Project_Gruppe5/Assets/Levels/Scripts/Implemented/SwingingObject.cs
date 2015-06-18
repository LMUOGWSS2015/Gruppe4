using UnityEngine;
using System.Collections;

public class SwingingObject : InteractivePhysicsObject {

	// The swinging axis
	public AXIS axis;

	// The total swinging angle
	public float angle;

	// The start position of the object (0 = center, positive value = to the right, negative value = to the left)
	public float startAngle;

	// The velocity the swinging object reaches in the center
	public float maxVelocity;

	// The current swinging direction
	public bool swingingRight;

	// The current swinging velocity
	private float currentVelocity;
	
	// The quaternions for the target rotations
	//private Quaternion currentRotation;
	private Quaternion rightEndRotation;
	private Quaternion leftEndRotation;

	public enum AXIS {
		X,
		Y,
		Z
	}

	// Initialization
	public override void StartMe () 
	{
		//base.Start ();

		// Calculate the degrees for the target rotations
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
			currentDegrees = transform.rotation.eulerAngles.y;
			break;
		}
		float rightDegrees = currentDegrees + (angle / 2) - startAngle;
		float leftDegrees = currentDegrees - (angle / 2) - startAngle;

		// Create the euler vectors for the target rotations
		Vector3 currentEuler = new Vector3();
		Vector3 leftEuler = new Vector3();
		Vector3 rightEuler = new Vector3();
		switch(axis) {
		case AXIS.X:
			currentEuler = new Vector3(rightDegrees, 0, 0);
			leftEuler = new Vector3(leftDegrees, 0, 0);
			rightEuler = new Vector3(rightDegrees, 0, 0);
			break;
		case AXIS.Y:
			currentEuler = new Vector3(0, currentDegrees, 0);
			leftEuler = new Vector3(0, leftDegrees, 0);
			rightEuler = new Vector3(0, rightDegrees, 0);
			break;
		case AXIS.Z:
			currentEuler = new Vector3(0, 0, currentDegrees);
			leftEuler = new Vector3(0, 0, leftDegrees);
			rightEuler = new Vector3(0, 0, rightDegrees);
			break;
		default:
			currentEuler = new Vector3(0, currentDegrees, 0);
			leftEuler = new Vector3(0, leftDegrees, 0);
			rightEuler = new Vector3(0, rightDegrees, 0);
			break;
		}

		// Calculate the rotation vectors for the target rotations
		//currentRotation = Quaternion.Euler(currentEuler);
		leftEndRotation = Quaternion.Euler(leftEuler);
		rightEndRotation = Quaternion.Euler(rightEuler);
	}
	
	// Update if the swinging is activated
	public override void DoActivation () 
	{
		//Debug.Log (Quaternion.Angle (transform.rotation, rightEndRotation));
		if (swingingRight) {
			// Check if the target rotation is reached
			if (Quaternion.Angle (transform.rotation, rightEndRotation) <= 0.1f) {
				swingingRight = false;
			}
			// Calculate the new velocity
			currentVelocity = calculateVelocity();
			// Swing (rotate) to the right
			rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rightEndRotation, Time.deltaTime * currentVelocity));
		} else {
			// Check if the target rotation is reached
			if (Quaternion.Angle (transform.rotation, leftEndRotation) <= 0.1f) {
				swingingRight = true;
			}
			// Calculate the new velocity
			currentVelocity = calculateVelocity();
			// Swing (rotate) to the left
			rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, leftEndRotation, Time.deltaTime * currentVelocity));
		}
	}
	
	// Do nothing if the swinging is deactivated
	public override void DoDeactivation ()
	{

	}

	// Calculate the velocity
	public float calculateVelocity() {
		//float leftAngle = Quaternion.Angle (transform.rotation, leftEndRotation);
		//float rightAngle = Quaternion.Angle (transform.rotation, rightEndRotation);
		//return Mathf.Sin (leftAngle) * maxVelocity;
		return maxVelocity;
	}

}
