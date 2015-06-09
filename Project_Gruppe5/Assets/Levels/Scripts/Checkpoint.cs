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
			CheckPoint oldCheckPoint = Player.Instance.respawnPoint.gameObject.GetComponent<CheckPoint>();
			if (oldCheckPoint)
				oldCheckPoint.DeActivate();

			Player.Instance.respawnPoint = transform;
			Activate();
		}
	}

	private void Activate() {
		spotlight.color = Color.yellow;
	}

	private void DeActivate() {
		spotlight.color = Color.red;
	}
}
