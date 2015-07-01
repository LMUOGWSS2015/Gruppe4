using UnityEngine;
using System.Collections;

public class LevelController : Singleton<LevelController> {

	private void FixedUpdate() {
		if (InputManager.Esc() || Input.GetKeyDown(KeyCode.Escape)) {

			Application.LoadLevel ("MainMenu");
		}
	}
}
