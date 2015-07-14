using UnityEngine;
using System.Collections;

//Complex rotating platform (does the same as rotating platform but needed for special cases)
public class ComplexRotatingPlatform : InteractivePhysicsObject {
	
	public AXIS axis;
	public float speed;
	public float endRotationDegrees;
	public bool waitForFinish;

	private float rotationDegrees;
	private Quaternion startRotation;
	private Quaternion endRotation;
	private bool checkAxis;
	private Vector3 startRotationDegrees;
	private bool reset;
	
	public enum AXIS {
		X,
		Y,
		Z
	}
	
	// Use this for initialization
	public override void StartMe () 
	{
		checkAxis = true;
		startRotationDegrees = transform.rotation.eulerAngles; 
	}
	
	private void SetRotation ()
	{
		Vector3 euler = new Vector3();
		switch(axis) {
		case AXIS.X:
			euler = new Vector3(rotationDegrees, 0, 0);
			break;
		case AXIS.Y:
			euler = new Vector3(0, rotationDegrees, 0);
			break;
		case AXIS.Z:
			euler = new Vector3(0, 0, rotationDegrees);
			break;
		default:
			euler = new Vector3(0, rotationDegrees, 0);
			break;
		}
		
		endRotation = Quaternion.Euler(euler);
		startRotation = transform.rotation;
	}

	private void ResetRotationDegrees (bool reset)
	{
		switch(axis) {
		case AXIS.X:
			rotationDegrees = reset ? startRotationDegrees.x : endRotationDegrees;
			break;
		case AXIS.Y:
			rotationDegrees = reset ? startRotationDegrees.x : endRotationDegrees;
			break;
		case AXIS.Z:
			rotationDegrees = reset ? startRotationDegrees.x : endRotationDegrees;
			break;
		default:
			rotationDegrees = reset ? startRotationDegrees.x : endRotationDegrees;
			break;
		}
	}
	
	public override void DoActivation ()
	{
		if(!(waitForFinish && isActivated)) {
			if(checkAxis) {
				ResetRotationDegrees(reset);
				reset = !reset;
				SetRotation();
				checkAxis = false;
			}
			
			rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, endRotation, Time.deltaTime * speed));
			
			if(transform.rotation == endRotation) {
				checkAxis = true;
				isActivated = false;
			}
		}
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
