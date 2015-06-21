using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseActivator : ActivatingObject {

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
		Activated ();
	}

	public void OnMouseExit() {
		if (deactivateOnExit) {
			Deactivated();
		}
	}

}
