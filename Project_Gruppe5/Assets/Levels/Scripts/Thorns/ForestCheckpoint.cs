using UnityEngine;
using System.Collections;

public class ForestCheckpoint : MonoBehaviour {

	public LightCircle lightCircle;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player") {
			if(!lightCircle.gameObject.activeSelf) {
				lightCircle.gameObject.SetActive(true);
				lightCircle.DoActivation();
			}

		}
	}
}
