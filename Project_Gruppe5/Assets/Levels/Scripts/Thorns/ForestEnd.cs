using UnityEngine;
using System.Collections;

public class ForestEnd : MonoBehaviour {

	public InteractivePhysicsObject barriere;
	public ParticleSystem dust;
	public Collider[] deadZones;

	private bool isTriggered;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player") {
			if(!isTriggered) {
				StartCoroutine(CloseExit());
				foreach(Collider col in deadZones)
					col.enabled = true;
			}
		}
	}

	private IEnumerator CloseExit ()
	{
		isTriggered = true;
		Player.Instance.Freeze (true);
		dust.Play ();
		barriere.Activate ();
		yield return new WaitForSeconds (2.0f);
		Player.Instance.Freeze (false);
	}
}
