using UnityEngine;
using System.Collections;

public class TrampolinEnter : MonoBehaviour {

	[SerializeField] float trampolinJumpPower;

	void OnTriggerEnter(Collider other) {
		if(other.transform.tag == "Player") {
			Player.Instance.TrampolinEnter(trampolinJumpPower);
		}
	}
}
