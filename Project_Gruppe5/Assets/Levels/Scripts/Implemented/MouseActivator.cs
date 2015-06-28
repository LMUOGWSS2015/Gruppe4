using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseActivator : ActivatingObject {

	public bool clickRequired = false; // Is a click necessary to activate or is the mouse movement sufficient? 
	public bool deactivatable = false; // Is the object deactivatable, or is it fixed after one activation?

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
		if (!clickRequired && deactivatable) {
			Deactivated();
		}
	}

	public void OnMouseDown() {
		if (clickRequired) {
			if (isActivated && deactivatable) {
				Deactivated();
			} else {
				Activated();
			}
		}
	}

	public void ExternalActivation() {
		Debug.Log ("External activation - " + isActivated);
		if (isActivated && deactivatable) {
			Deactivated();
		} else {
			Activated();
		}
	}

}
