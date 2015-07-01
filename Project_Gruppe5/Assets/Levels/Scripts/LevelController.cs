using UnityEngine;
using System.Collections;

public class LevelController : Singleton<LevelController> {

	public Transform cameraCenter;

	private void FixedUpdate() {
		if (InputManager.Esc() || Input.GetKeyDown(KeyCode.Escape)) {

			//Application.LoadLevel ("MainMenu");
		}
	}
}
