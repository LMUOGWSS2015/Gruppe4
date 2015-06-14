using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GazeActivator : ActivatingObject {

	public KeyCode key = KeyCode.Space;

	public override void Update() {
		base.Update ();
		if (Input.GetKeyDown(key)) {
			if (isActivated) {
				Deactivated();
			} else {
				Activated();
			}
		}
	}

	public override void Activated()
	{
		base.Activated();
	}

	public override void Deactivated()
	{
		base.Deactivated();
	}
}
