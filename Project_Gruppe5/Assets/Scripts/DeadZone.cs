using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {
	
	void OnTriggerEnter(Collider col) {
		Player player = col.GetComponent<Player> ();
		if (player)
			player.Kill ();
	}
}
