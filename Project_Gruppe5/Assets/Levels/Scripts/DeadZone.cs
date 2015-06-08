using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.transform.tag == "Player") {
			Player.Instance.Kill();
		}
	}
}
