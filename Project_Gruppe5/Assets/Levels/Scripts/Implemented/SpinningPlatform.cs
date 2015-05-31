using UnityEngine;
using System.Collections;

public class SpinningPlatform : InteractivePhysicsObject {

	public Vector3 velocity;


	
	// Update is called once per frame
	public override void DoActivation () 
	{
		Quaternion deltaRotation = Quaternion.Euler(velocity * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}

	public override void DoDeactivation ()
	{

	}
}
