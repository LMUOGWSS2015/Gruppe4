using UnityEngine;
using System.Collections;

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
