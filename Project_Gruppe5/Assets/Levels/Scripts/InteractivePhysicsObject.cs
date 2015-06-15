using UnityEngine;
using System.Collections;

public class InteractivePhysicsObject : InteractiveObject {

	[HideInInspector]
	public Rigidbody rb;
	
	public void Start() 
	{
		base.Start ();
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
	
	void FixedUpdate() 
	{
		if(isActivated) {
			DoActivation ();
		}
		
		if(isDeactivated && !singleUse) {
			DoDeactivation ();
		}
	}
}
