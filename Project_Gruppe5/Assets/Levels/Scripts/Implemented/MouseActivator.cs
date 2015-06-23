using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseActivator : ActivatingObject {

	public bool clickRequired = false;
	public bool deactivateOnExit = false;

	public override void Activated()
	{
		base.Activated();
	}

	public override void Deactivated()
	{
		base.Deactivated();
	}

	public void OnMouseEnter() {
		if (!clickRequired) {
			Activated ();
		}
	}

	public void OnMouseExit() {
		if (!clickRequired && deactivateOnExit) {
			Deactivated();
		}
	}

	public void OnMouseDown() {
		if (clickRequired) {
			if (isActivated) {
				Deactivated();
			} else {
				Activated();
			}
		}
	}

}
