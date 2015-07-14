using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//NOT IN FINAL GAME
public class Lever : ActivatingObject {

	public Color deactivatedColor;
	public Color activatedColor;

	// Keep track of all colliders that stand on the lever
	private List<Collider> collidingObjects = new List<Collider>();

	public override void StartMe ()
	{
		if(isDeactivated) GetComponent<Renderer>().material.color = deactivatedColor;
		else GetComponent<Renderer>().material.color = activatedColor;
	}

	public void OnTriggerEnter(Collider other) {
		collidingObjects.Add (other);
		// Lever gets activated by a collider standing on it
		Activated();
	}

	public void OnTriggerExit(Collider other) {
		collidingObjects.Remove (other);
		// Deactive the lever if there are no other colliders left standing on it
		if (collidingObjects.Count == 0) {
			Deactivated ();
		}
	}

	public override void Activated()
	{
		base.Activated();

		GetComponent<Renderer>().material.color = activatedColor;
	}

	public override void Deactivated()
	{
		base.Deactivated();

		GetComponent<Renderer>().material.color = deactivatedColor;
	}
}
