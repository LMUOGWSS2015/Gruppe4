using UnityEngine;
using System.Collections;

public class PlatformCollide : MonoBehaviour {

	public GameObject gazeCrystal;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Enter");
		GazeSpinningPlatform sn = (GazeSpinningPlatform)gazeCrystal.GetComponent(typeof(GazeSpinningPlatform));
		sn.AttachPlayerToPlatform ();
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Exit");
		GazeSpinningPlatform sn = (GazeSpinningPlatform)gazeCrystal.GetComponent(typeof(GazeSpinningPlatform));
		sn.DetachPlayerFromPlatform ();
	}
}
