using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	public LevelController levelController;

	public Light spotLight;
	public Light pointLight;

	public GameObject border;

	private bool end;

	void Start() {
		end = false;
		border.SetActive (false);
		spotLight.color = Color.red;
		pointLight.color = Color.red;
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

			end = true;

			StartCoroutine(End());
		}
	}

	public IEnumerator End() {
		yield return new WaitForSeconds(5.0f);
		
		levelController.EndLevel();
	}

	public void Activate() {
		spotLight.color = Color.cyan;
		spotLight.spotAngle = 120.0f;

		pointLight.color = Color.cyan;
		pointLight.range = 20.0f;
	}
}
