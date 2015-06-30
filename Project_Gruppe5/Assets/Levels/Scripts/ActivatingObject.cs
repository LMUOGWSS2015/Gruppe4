using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatingObject : MyMonoBehaviour {
	
	public bool isActivated;
	public bool isDeactivated;
	public bool isLocked;
	public float lockTime;

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

	private IEnumerator LockActivator ()
	{
		isLocked = true;
		yield return new WaitForSeconds(lockTime);
		isLocked = false;
	}

}
