using UnityEngine;
using System.Collections;

public class SpinningPlatform : InteractivePhysicsObject {

	public Vector3 velocity;

	private Vector3 inverseVelocity;

	public bool directionChanger;

	void Start() {
		base.Start ();
		inverseVelocity = velocity * -1;
	}

	// Update is called once per frame
	public override void DoActivation () 
	{
		if (directionChanger)
			Rotate (velocity);
		else
			Rotate (velocity);
	}
	
	public override void DoDeactivation ()
	{
		if (directionChanger)
			Rotate (inverseVelocity);
	}
	
	public void Rotate(Vector3 v) {
		Quaternion deltaRotation = Quaternion.Euler(v * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}
}

