﻿using UnityEngine;
using System.Collections;

//Platform that rotates around the own center by a given number of degrees
public class RotatingPlatform : InteractivePhysicsObject {

	//axis about which the object is rotated
	public AXIS axis;
	//speed of rotation
	public float speed;
	//destination degrees
	public float endRotationDegrees;

	private Quaternion startRotation;
	private Quaternion endRotation;
	//private bool checkAxis;

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
	/*	checkAxis = true;
	}

	private void SetRotation ()
	{*/
		Vector3 euler = new Vector3();
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

		endRotation = Quaternion.Euler(euler);
		startRotation = transform.rotation;

		currentEndRotation = endRotationDegrees;
		updateRotationState ();
	}

	//start the rotation
	public override void DoActivation ()
	{
		/*if(checkAxis) {
			SetRotation();
			checkAxis = false;
		}*/

		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, endRotation, Time.deltaTime * speed));

		/*if(transform.rotation == endRotation)
			checkAxis = true;*/

		//makes it possible to keep rotating the object further after first activation
		if (continueRotation && transform.rotation == endRotation) {
			//this.Deactivate();
			this.isActivated = false;
			currentStartRotation += endRotationDegrees;
			currentEndRotation += endRotationDegrees;
			updateRotationState();
		}
	}

	//deactivates the platform making it rotate back to the original state
	public override void DoDeactivation ()
	{
		/*if(checkAxis) {
			SetRotation();
			checkAxis = false;
		}*/

		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, startRotation, Time.deltaTime * speed));

		/*if(transform.rotation == startRotation)
			checkAxis = true;*/

		//makes it possible to keep rotating the object further after first activation
		if (continueRotation && transform.rotation == endRotation) {
			this.Deactivate();
			currentStartRotation -= endRotationDegrees;
			currentEndRotation -= endRotationDegrees;
			updateRotationState();
		}
	}

	//update the state of the rotation
	private void updateRotationState() {
		Vector3 eulerStart = new Vector3();
		Vector3 eulerEnd = new Vector3();
		switch(axis) {
		case AXIS.X:
			eulerStart = new Vector3(currentStartRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			eulerEnd = new Vector3(currentEndRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			break;
		case AXIS.Y:
			eulerStart = new Vector3(transform.rotation.eulerAngles.x, currentStartRotation, transform.rotation.eulerAngles.z);
			eulerEnd = new Vector3(transform.rotation.eulerAngles.x, currentEndRotation, transform.rotation.eulerAngles.z);
			break;
		case AXIS.Z:
			eulerStart = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentStartRotation);
			eulerEnd = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentEndRotation);
			break;
		default:
			eulerStart = new Vector3(transform.rotation.eulerAngles.x, currentStartRotation, transform.rotation.eulerAngles.z);
			eulerEnd = new Vector3(transform.rotation.eulerAngles.x, currentEndRotation, transform.rotation.eulerAngles.z);
			break;
		}
		startRotation = Quaternion.Euler (eulerStart);
		endRotation = Quaternion.Euler(eulerEnd);
	}

}
