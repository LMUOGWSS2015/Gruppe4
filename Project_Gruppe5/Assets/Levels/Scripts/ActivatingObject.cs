using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Activator for interactive objects
public class ActivatingObject : MyMonoBehaviour {
	
	public bool isActivated;
	public bool isDeactivated;
	public bool isLocked;
	public float lockTime;

	// List of all objects that will be triggered
	public List<InteractiveObject> interactingObjects;

	public virtual void Update() {
		UpdateMe ();
	}

	void Start() 
	{
		isActivated = false;
		isDeactivated = true;
		StartMe ();
	}

	//Activates all objects
	public virtual void Activated()
	{
		if(!isLocked) {
			isActivated = true;
			isDeactivated = false;
			foreach(InteractiveObject obj in interactingObjects) {
				obj.Activate();
			}

			StartCoroutine(LockActivator());
		}
	}

	//Deactivates all objects
	public virtual void Deactivated()
	{
		if(!isLocked) {
			isActivated = false;
			isDeactivated = true;
			foreach(InteractiveObject obj in interactingObjects) {
				obj.Deactivate();
			}

			StartCoroutine(LockActivator());
		}
	}

	//Locks the activator for a certain time
	private IEnumerator LockActivator ()
	{
		isLocked = true;
		yield return new WaitForSeconds(lockTime);
		isLocked = false;
	}

}
