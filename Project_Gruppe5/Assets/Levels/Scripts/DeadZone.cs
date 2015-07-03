using UnityEngine;
using System.Collections;

/*
 * Ein Collider als Trigger, der sich unter dem Gesamten Level erstreckt.
 * Beim Eintritt in die DeadZone wird der Spieler zum letzten CheckPoint zurückgesetzt.
 */
public class DeadZone : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.transform.tag == "Player") {
			Player.Instance.Kill();
		}
	}
}
