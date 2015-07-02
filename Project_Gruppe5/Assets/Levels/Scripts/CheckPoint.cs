using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public Light spotlight;

	void Start() {
		spotlight.color = Color.red;
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

	public void Activate() {
		spotlight.color = Color.yellow;
	}

	public void DeActivate() {
		spotlight.color = Color.red;
	}
}
