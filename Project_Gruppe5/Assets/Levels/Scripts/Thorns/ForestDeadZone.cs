using UnityEngine;
using System.Collections;

public class ForestDeadZone : MonoBehaviour {

	public LightCircle lCircle;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player") {
			lCircle.DoActivation ();
		}
	}
}
