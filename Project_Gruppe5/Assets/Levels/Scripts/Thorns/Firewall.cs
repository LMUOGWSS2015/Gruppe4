using UnityEngine;
using System.Collections;

//Firewall that emits fire and kills the player
public class Firewall : MonoBehaviour {

	//delay until the fire starts
	public float delay;
	//duration until flames go away again
	public float durationTime;
	//duration between the flames appear
	public float cooldownTime;
	//the flame is always on
	public bool alwaysFlame;
	//collider of the flamewall
	public BoxCollider collider;
	//bottom light
	public Light groundLight;
	//the single firestreams
	public FirewallEmitter[] emitters;

	private bool running;

	// Use this for initialization
	void Start () 
	{
		running = true;
		if(alwaysFlame) {
			Activate ();
		} else {
			StartCoroutine(Init ());
		}
	}

	//Initialize the firwall and take delay, duration and cooldown into account
	private IEnumerator Init ()
	{
		yield return new WaitForSeconds(delay);
		while(running) {
			Activate ();
			yield return new WaitForSeconds (durationTime);
			Deactivate ();
			yield return new WaitForSeconds (cooldownTime);
			yield return null;
		}
	}

	//start the flames
	public void Activate ()
	{
		foreach(FirewallEmitter fe in emitters)
			fe.Activate ();
		collider.enabled = true;
		groundLight.enabled = true;
	}

	//deactivate the flames
	public void Deactivate ()
	{
		foreach(FirewallEmitter fe in emitters)
			fe.Deactivate ();
		collider.enabled = false;
		groundLight.enabled = false;
	}

}
