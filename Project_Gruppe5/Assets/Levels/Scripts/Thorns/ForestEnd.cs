using UnityEngine;
using System.Collections;

//The exit of the forest in the thornlevel
public class ForestEnd : MonoBehaviour {

	//barriere that closes the forest
	public InteractivePhysicsObject barriere;
	//dust that is on the bottom
	public ParticleSystem dust;

	private bool isTriggered;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player") {
			if(!isTriggered) {
				StartCoroutine(CloseExit());
				GameObject deadZones = GameObject.Find ("DeadZones");
				foreach(Transform dz in deadZones.transform) {
					dz.GetComponent<Collider>().enabled = true;
				}
				GameObject.Find("owls").GetComponent<AudioSource>().Stop ();
				Debug.Log (GameObject.Find("owls"));
			}
		}
	}

	//closes the exit and plays the sound
	private IEnumerator CloseExit ()
	{
		isTriggered = true;
		Player.Instance.Freeze (true);
		dust.Play ();
		GetComponent<AudioSource>().Play();
		barriere.Activate ();
		yield return new WaitForSeconds (2.0f);
		Player.Instance.Freeze (false);
	}
}
