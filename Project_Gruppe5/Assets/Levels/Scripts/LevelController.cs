using UnityEngine;
using System.Collections;

public class LevelController : Singleton<LevelController> {

	public string levelName;

	public GameObject levelContent;
	public GameObject originalLevelMenu;
	public GameObject originalFinishMenu;

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

	public void EndLevel() {
		levelContent.SetActive (false);
		
		Instantiate (originalFinishMenu);
	}
}
