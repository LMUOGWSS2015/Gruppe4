using UnityEngine;
using System.Collections;

/*
 * Jedes Level hat einen LevelController, der sich um die Verwaltung des Levels kümmert.
 */
public class LevelController : Singleton<LevelController> {

	public LoadingController.Scene levelName; // Der Scenenname des Levels.

	public GameObject levelContent; // Der komplette Levelinhalt.
	public GameObject originalLevelMenu; // Prefab für das Pause-Menü.
	public GameObject originalFinishMenu; // Prefab für das Menü am Ende des Levels.
	public GameObject originalRestartContent; // Prefab für alle Levelobjekte, die bei einem Restart (z.B. nach Tod) wiederhergestellt werden müssen.

	private GameObject levelMenu; // Das aktuell geladene Pause-Menü.
	private GameObject restartContent; // Der aktuell geladene RestartContent.

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

	/*
	 * Fortführen des Levels im aktuellen Zustand.
	 */
	public void ContinueLevel() {
		Destroy (levelMenu);

		levelContent.SetActive (true);
	}

	/*
	 * Beenden des Levels und laden des Ende-Menüs.
	 */
	public void EndLevel() {
		levelContent.SetActive (false);
		
		Instantiate (originalFinishMenu);
	}

	/*
	 * Neustart des Levels am letzten CheckPoint mit wiederhergestellten Levelobjekten.
	 */
	public void restartLevel() {
		restartContent = Instantiate (originalRestartContent);
		restartContent.transform.SetParent (levelContent.transform);
	}
}
