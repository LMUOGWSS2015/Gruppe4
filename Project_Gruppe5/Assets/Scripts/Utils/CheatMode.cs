using UnityEngine;
using System.Collections;

public class CheatMode : MonoBehaviour {

	private GameObject[] checkPointsGO;
	
	private int checkPointNumber = -1;
	
	// Use this for initialization
	void Start () {
		// get all check points in the scene
		checkPointsGO = GameObject.FindGameObjectsWithTag ("CheckPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.Next()) {
			if (checkPointNumber >= 0 && checkPointNumber < checkPointsGO.Length - 1) {
				checkPointNumber++;
			} else {
				checkPointNumber = 0;
			}
			Teleport ();
		} else if (InputManager.Prev()) {
			if (checkPointNumber >= 1) {
				checkPointNumber--;
			} else {
				checkPointNumber = checkPointsGO.Length - 1;
			}
			Teleport ();
		}
	}
	
	public void Teleport() {
		/*
		GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);
		
		Transform respawnPoint = checkPointsGO [checkPointNumber].transform;
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
		*/

		Transform oldRespawnPoint = Player.Instance.respawnPoint;
		if (oldRespawnPoint) {
			CheckPoint oldCheckPoint = oldRespawnPoint.gameObject.GetComponent<CheckPoint>();
			if (oldCheckPoint)
				oldCheckPoint.DeActivate();
		}
		
		Player.Instance.respawnPoint = checkPointsGO[checkPointNumber].transform;
		checkPointsGO[checkPointNumber].GetComponent<CheckPoint>().Activate();

		Player.Instance.Kill();
	}
	
}
