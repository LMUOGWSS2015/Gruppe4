using UnityEngine;
using System.Collections;

/*
 * Finaler CheckPoint.
 * Beim Erreichen des Finish endet das Level
 * und der Spieler kann sich nur noch im Finish-Bereich bewegen.
 */
public class Finish : MonoBehaviour {

	public GameObject activatedSprite;
	public GameObject deactivatedSprite;

	public Light spotLight; // Macht das Finish sichtbar.
	public Light pointLight; // Macht das Finish sichtbar.

	public GameObject border; // Begrenzt den Bewegungsradius des Spielers bei Eintritt in den Finish-Bereich.

	private bool end; // Wird true wenn der Finish-Bereich erreicht wurde.

	void Start() {
		end = false;
		border.SetActive (false);

		spotLight.color = Color.red;
		pointLight.color = Color.red;

		activatedSprite.SetActive (false);
		deactivatedSprite.SetActive (true);
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Player" && !end) {
			Transform oldRespawnPoint = Player.Instance.respawnPoint;
			if (oldRespawnPoint) {
				CheckPoint oldCheckPoint = oldRespawnPoint.gameObject.GetComponent<CheckPoint>();
				if (oldCheckPoint)
					oldCheckPoint.DeActivate();
			}
			
			Player.Instance.respawnPoint = transform;

			Activate();
			border.SetActive(true);

			Player.Instance.finish = true;

			LevelController.Instance.PlayWinMusic();

			end = true;
			StartCoroutine(End());
		}
	}

	/*
	 * Wartet 5sec und beendet dann das Level.
	 */
	public IEnumerator End() {
		yield return new WaitForSeconds(10.0f);
		
		LevelController.Instance.EndLevel();
	}

	/*
	 * Markiert das Finish als erreicht, durch Änderung der Farbe.
	 */
	public void Activate() {
		spotLight.color = Color.cyan;
		spotLight.spotAngle = 120.0f;

		pointLight.color = Color.cyan;
		//pointLight.range = 20.0f;

		deactivatedSprite.SetActive (false);
		activatedSprite.SetActive (true);
	}
}
