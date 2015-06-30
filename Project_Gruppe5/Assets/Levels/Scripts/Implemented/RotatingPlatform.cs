using UnityEngine;
using System.Collections;

public class RotatingPlatform : InteractivePhysicsObject {

	public AXIS axis;
	public float speed;
	public float endRotationDegrees;

	private Quaternion startRotation;
	private Quaternion endRotation;
	private bool checkAxis;

	public enum AXIS {
		X,
		Y,
		Z
	}

	// Use this for initialization
	public override void StartMe () 
	{
		checkAxis = true;
	}

	private void SetRotation ()
	{
		Vector3 euler = new Vector3();
		switch(axis) {
		case AXIS.X:
			euler = new Vector3(endRotationDegrees, 0, 0);
			break;
		case AXIS.Y:
			euler = new Vector3(0, endRotationDegrees, 0);
			break;
		case AXIS.Z:
			euler = new Vector3(0, 0, endRotationDegrees);
			break;
		default:
			euler = new Vector3(0, endRotationDegrees, 0);
			break;
		}
		
		endRotation = Quaternion.Euler(euler);
		startRotation = transform.rotation;
	}

	public override void DoActivation ()
	{
		if(checkAxis) {
			SetRotation();
			checkAxis = false;
		}

		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, endRotation, Time.deltaTime * speed));

		if(transform.rotation == endRotation)
			checkAxis = true;
	}

	public override void DoDeactivation ()
	{
		if(checkAxis) {
			SetRotation();
			checkAxis = false;
		}

		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, startRotation, Time.deltaTime * speed));

		if(transform.rotation == startRotation)
			checkAxis = true;
	}
}
