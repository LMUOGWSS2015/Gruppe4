using UnityEngine;
using System.Collections;

//Extension of InteractiveObject for objects that need to use physics
public class InteractivePhysicsObject : InteractiveObject {

	//rigidbody of object
	[HideInInspector]
	public Rigidbody rb;
	public bool canBeDeactivated;
	
	public void Start() 
	{
		isActivated = false;
		isDeactivated = true;
		isUsed = false;
		rb = GetComponent<Rigidbody>();
		if(rb != null) {
			StartMe ();
			if(noTrigger)
				Activate();
		} else {
			Debug.LogWarning("No Rigidbody attached!");
		}
	}

	//overrides InteractiveObject.Update()
	void Update() {}

	//FixedUpdate for physics
	void FixedUpdate() 
	{
		if(isActivated) {
			DoActivation ();
		}
		
		if(isDeactivated && !singleUse && canBeDeactivated) {
			DoDeactivation ();
		}
	}
}
