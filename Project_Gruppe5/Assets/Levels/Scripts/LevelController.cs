using UnityEngine;
using System.Collections;

public class LevelController : Singleton<LevelController> {

	public LoadingController.Scene levelName;

	public GameObject levelContent;
	public GameObject originalLevelMenu;
	public GameObject originalFinishMenu;
	public GameObject originalRestartContent;

	private GameObject levelMenu;
	private GameObject restartContent;

	private void Start() {
		restartContent = Instantiate (originalRestartContent);
		restartContent.transform.SetParent (levelContent.transform);
	}

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

	public void restartLevel() {
		restartContent = Instantiate (originalRestartContent);
		restartContent.transform.SetParent (levelContent.transform);
	}
}
