using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatingObject : MyMonoBehaviour {
	
	public bool isActivated;
	public bool isDeactivated;

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
		isActivated = true;
		isDeactivated = false;
		foreach(InteractiveObject obj in interactingObjects) {
			obj.Activate();
		}
	}
	
	public virtual void Deactivated()
	{
		isActivated = false;
		isDeactivated = true;
		foreach(InteractiveObject obj in interactingObjects) {
			obj.Deactivate();
		}
	}

}
