using UnityEngine;
using System.Collections;

public class GhostCollider : MonoBehaviour {

	public Ghost ghost;

	private bool triggered;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player") {
			StartCoroutine(ghost.AttackPlayer ());
		}
	}
}
