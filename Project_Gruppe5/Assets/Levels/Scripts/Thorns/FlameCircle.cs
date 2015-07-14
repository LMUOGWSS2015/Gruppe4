using UnityEngine;
using System.Collections;

//Tracks a list of Firewalls and keeps all on except for one
public class FlameCircle : InteractiveObject {

	//All firewalls that are in the circle
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

	//Deactivates the next firewall and turns the rest on
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
