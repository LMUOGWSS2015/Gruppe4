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
		if (Input.GetKeyDown (KeyCode.E)) {
			if (checkPointNumber >= 0 && checkPointNumber < checkPointsGO.Length - 1) {
				checkPointNumber++;
			} else {
				checkPointNumber = 0;
			}
			Teleport ();
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			if (checkPointNumber >= 1) {
				checkPointNumber--;
			} else {
				checkPointNumber = checkPointsGO.Length - 1;
			}
			Teleport ();
		}
	}
	
	public void Teleport() {
		GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);
		
		Transform respawnPoint = checkPointsGO [checkPointNumber].transform;
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
	}
	
}
