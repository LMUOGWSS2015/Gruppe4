using UnityEngine;
using System.Collections;

public class InteractivePhysicsObject : InteractiveObject {

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
