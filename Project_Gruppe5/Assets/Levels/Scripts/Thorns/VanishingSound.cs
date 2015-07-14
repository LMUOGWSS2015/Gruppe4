using UnityEngine;
using System.Collections;

//Sound that playes when the lightcircle hits it
public class VanishingSound : MonoBehaviour {

	private bool played;

	public void Enable()
	{
		if(!played) {
			GetComponent<AudioSource>().Play();
			played = true;
		}
	}

}
