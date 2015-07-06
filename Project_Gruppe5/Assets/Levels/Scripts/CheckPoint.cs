using UnityEngine;
using System.Collections;

/*
 * Respawn-Punkt an den der Spieler zurückgesetzt wird wenn er stirbt.
 * Durch Reinlaufen in einen CheckPoint wird dieser als aktueller Respawn-Punkt aktiviert
 * und verändert dabei seine Farbe.
 */
public class CheckPoint : MonoBehaviour {

	public Light spotlight; // Macht den CheckPoint sichtbar.

	private Color startColor; // Farbe für deaktivierte CheckPoints.

	void Start() {
		spotlight.color = Color.red;
		startColor = spotlight.color;
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Player") {
			Transform oldRespawnPoint = Player.Instance.respawnPoint;
			if (oldRespawnPoint) {
				CheckPoint oldCheckPoint = oldRespawnPoint.gameObject.GetComponent<CheckPoint>();
				if (oldCheckPoint)
					oldCheckPoint.DeActivate();
			}

			Player.Instance.respawnPoint = transform;
			Activate();
		}
	}

	/*
	 * Markiert den CheckPoint als aktiv (gelbe Farbe).
	 */
	public void Activate() {
		spotlight.color = Color.yellow;
	}

	/*
	 * Markiert den CheckPoint als inaktiv (ursprüngliche Farbe).
	 */
	public void DeActivate() {
		spotlight.color = startColor;
	}
}
