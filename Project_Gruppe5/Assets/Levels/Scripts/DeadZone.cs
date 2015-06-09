using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.transform.tag == "Player") {
			Player.Instance.Kill();
		}
	}
}
