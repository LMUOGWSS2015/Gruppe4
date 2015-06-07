using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public Transform respawnPoint;

	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Player") {
			PlayerUtils.Instance.respawnPoint = respawnPoint;

		}
	}
}
