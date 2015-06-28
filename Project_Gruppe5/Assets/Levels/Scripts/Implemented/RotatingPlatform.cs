using UnityEngine;
using System.Collections;

public class RotatingPlatform : InteractivePhysicsObject {

	public AXIS axis;
	public float speed;
	public float endRotationDegrees;

	private Quaternion startRotation;
	private Quaternion endRotation;

	// Allow rotating the object round 360 degrees in four steps of 90 degrees
	public bool continueRotation = false;
	private float currentStartRotation;
	private float currentEndRotation;

	public enum AXIS {
		X,
		Y,
		Z
	}

	// Use this for initialization
	public override void StartMe () 
	{
		Vector3 startEuler = transform.rotation.eulerAngles;
		switch(axis) {
		case AXIS.X:
			currentStartRotation = startEuler.x;
			break;
		case AXIS.Y:
			currentStartRotation = startEuler.y;
			break;
		case AXIS.Z:
			currentStartRotation = startEuler.z;
			break;
		default:
			currentStartRotation = startEuler.y;
			break;
		}
		currentEndRotation = endRotationDegrees;
		updateRotationState ();
	}

	public override void DoActivation ()
	{
		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, endRotation, Time.deltaTime * speed));
		if (continueRotation && transform.rotation == endRotation) {
			this.Deactivate();
			currentStartRotation += endRotationDegrees;
			currentEndRotation += endRotationDegrees;
			updateRotationState();
		}
	}

	public override void DoDeactivation ()
	{
		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, startRotation, Time.deltaTime * speed));
		if (continueRotation && transform.rotation == endRotation) {
			this.Deactivate();
			currentStartRotation -= endRotationDegrees;
			currentEndRotation -= endRotationDegrees;
			updateRotationState();
		}
	}

	private void updateRotationState() {
		Vector3 eulerStart = new Vector3();
		Vector3 eulerEnd = new Vector3();
		switch(axis) {
		case AXIS.X:
			eulerStart = new Vector3(currentStartRotation, 0, 0);
			eulerEnd = new Vector3(currentEndRotation, 0, 0);
			break;
		case AXIS.Y:
			eulerStart = new Vector3(0, currentStartRotation, 0);
			eulerEnd = new Vector3(0, currentEndRotation, 0);
			break;
		case AXIS.Z:
			eulerStart = new Vector3(0, 0, currentStartRotation);
			eulerEnd = new Vector3(0, 0, currentEndRotation);
			break;
		default:
			eulerStart = new Vector3(0, currentStartRotation, 0);
			eulerEnd = new Vector3(0, currentEndRotation, 0);
			break;
		}
		startRotation = Quaternion.Euler (eulerStart);
		endRotation = Quaternion.Euler(eulerEnd);
	}

}
