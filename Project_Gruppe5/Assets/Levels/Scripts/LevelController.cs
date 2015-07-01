using UnityEngine;
using System.Collections;

public class LevelController : Singleton<LevelController> {

	public GameObject levelContent;
	public GameObject originalLevelMenu;

	private GameObject levelMenu;

	private void FixedUpdate() {
		if (levelContent.activeSelf) {
			if (InputManager.Esc() || Input.GetKeyDown(KeyCode.Escape)) {
				levelContent.SetActive (false);

				levelMenu = Instantiate (originalLevelMenu);
			}
		}
	}

	public void ContinueLevel() {
		Destroy (levelMenu);

		levelContent.SetActive (true);
	}
}
