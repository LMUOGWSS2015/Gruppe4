using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatingObject : MyMonoBehaviour {
	
	public bool isActivated;
	public bool isDeactivated;

	public List<InteractiveObject> interactingObjects;

	void Update() {
		UpdateMe ();
	}

	public virtual void Activated()
	{
		foreach(InteractiveObject obj in interactingObjects) {
			obj.Activate();
		}
	}
	
	public virtual void Deactivated()
	{
		foreach(InteractiveObject obj in interactingObjects) {
			obj.Deactivate();
		}
	}

}
