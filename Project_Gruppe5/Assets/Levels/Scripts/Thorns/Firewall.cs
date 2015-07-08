using UnityEngine;
using System.Collections;

public class Firewall : MonoBehaviour {

	public float delay;
	public float durationTime;
	public float cooldownTime;
	public bool alwaysFlame;
	public BoxCollider collider;
	public Light groundLight;
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

	public void Activate ()
	{
		foreach(FirewallEmitter fe in emitters)
			fe.Activate ();
		collider.enabled = true;
		groundLight.enabled = true;
	}

	public void Deactivate ()
	{
		foreach(FirewallEmitter fe in emitters)
			fe.Deactivate ();
		collider.enabled = false;
		groundLight.enabled = false;
	}

}
