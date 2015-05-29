using UnityEngine;
using System.Collections;

public class RotatingPlatform : InteractivePhysicsObject {

	public AXIS axis;
	public float speed;
	public float endRotationDegrees;

	private Quaternion startRotation;
	private Quaternion endRotation;

	public enum AXIS {
		X,
		Y,
		Z
	}

	// Use this for initialization
	public override void StartMe () 
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
		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, endRotation, Time.deltaTime * speed));
	}

	public override void DoDeactivation ()
	{
		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, startRotation, Time.deltaTime * speed));
	}
}
