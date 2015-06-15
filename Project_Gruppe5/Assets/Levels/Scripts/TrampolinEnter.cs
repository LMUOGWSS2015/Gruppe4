using UnityEngine;
using System.Collections;

public class TrampolinEnter : MonoBehaviour {

	[SerializeField] float trampolinJumpPower = 20.0f;

	void OnTriggerEnter(Collider other) {
		if(other.transform.tag == "Player") {
			Player.Instance.TrampolinEnter(trampolinJumpPower);
		}
	}
}
