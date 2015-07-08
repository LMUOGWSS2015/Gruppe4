using UnityEngine;
using System.Collections;

public class FlameCircle : InteractiveObject {

	public Firewall[] firewalls;

	private int current = 0;

	public override void StartMe ()
	{
		base.StartMe ();

		Switch ();
	}

	public override void DoActivation ()
	{
		isActivated = false;
		Next ();
	}

	private void Next ()
	{
		current++;
		if(current == firewalls.Length)
			current = 0;
		Switch ();
	}

	private void Switch ()
	{
		for(int i = 0; i < firewalls.Length; i++) {
			if(i == current) {
				firewalls[i].Deactivate ();
			} else {
				firewalls[i].Activate ();
			}
		}
	}
}
