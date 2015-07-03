using UnityEngine;
using System.Collections;

/*
 * Ein Collider als Trigger der ausgelöst wird wenn der Spieler auf ein Trampolin springt.
 * Beim auslösen des Triggers wird der Spieler nach oben katapultiert.
 */
public class TrampolinEnter : MonoBehaviour {

	[SerializeField] float trampolinJumpPower; // Die Absprungstärke des Trampolins.

	void OnTriggerEnter(Collider other) {
		if(other.transform.tag == "Player") {
			Player.Instance.TrampolinEnter(trampolinJumpPower);
		}
	}
}
