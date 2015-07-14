using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Jedes Level hat einen LevelController, der sich um die Verwaltung des Levels kümmert.
 */
public class LevelController : Singleton<LevelController> {

	public LoadingController.Scene levelName; // Der Scenenname des Levels.

	public GameObject cameraDofOn;
	public GameObject cameraDofOff;

	public GameObject levelMusic;
	public GameObject winMusic;

	public GameObject levelContent; // Der komplette Levelinhalt.
	public GameObject originalLevelMenu; // Prefab für das Pause-Menü.
	public GameObject originalFinishMenu; // Prefab für das Menü am Ende des Levels.
	public GameObject originalRestartContent; // Prefab für alle Levelobjekte, die bei einem Restart (z.B. nach Tod) wiederhergestellt werden müssen.

	private GameObject levelMenu; // Das aktuell geladene Pause-Menü.
	private GameObject restartContent; // Der aktuell geladene RestartContent.

	private bool clock;
	private float startTime;
	private float currentTime;
	private float prevTime;
	private float bestTime;

	public Text minutesText;
	public Text secondsText;
	public Text bestMinutesText;
	public Text bestSecondsText;

	private void Start() {
		if (PlayerPrefs.GetInt (MainController.DOF) == MainController.DOF_OFF) {
			cameraDofOn.SetActive (false);
			cameraDofOff.SetActive (true);
		} else {
			cameraDofOn.SetActive (true);
			cameraDofOff.SetActive (false);
		}

		restartContent = Instantiate (originalRestartContent);
		restartContent.transform.SetParent (levelContent.transform);

		winMusic.SetActive (false);

		switch (levelName) {
		case LoadingController.Scene.DESERT_LEVEL:
			bestTime = PlayerPrefs.GetFloat(MainController.DESERT_TIME);
			break;
		case LoadingController.Scene.ICE_LEVEL:
			bestTime = PlayerPrefs.GetFloat(MainController.ICE_TIME);
			break;
		case LoadingController.Scene.THORN_LEVEL:
			bestTime = PlayerPrefs.GetFloat(MainController.THORN_TIME);
			break;
		case LoadingController.Scene.FOREST_LEVEL:
			bestTime = PlayerPrefs.GetFloat(MainController.FOREST_TIME);
			break;
		default:
			break;
		}

		int minutes = (int)(bestTime / 60);
		int seconds = (int)bestTime - (minutes * 60);

		if (minutes < 10)
			bestMinutesText.text = "0" + minutes.ToString();
		else
			bestMinutesText.text = minutes.ToString();
		
		if (seconds < 10)
			bestSecondsText.text = "0" + seconds.ToString();
		else
			bestSecondsText.text = seconds.ToString();

		clock = true;
		startTime = Time.timeSinceLevelLoad;
	}

	private void FixedUpdate() {
		if (clock) {
			currentTime = (Time.timeSinceLevelLoad - startTime) + prevTime;
			int minutes = (int)(currentTime / 60);
			int seconds = (int)currentTime - (minutes * 60);

			if (minutes < 10)
				minutesText.text = "0" + minutes.ToString();
			else
				minutesText.text = minutes.ToString();

			if (seconds < 10)
				secondsText.text = "0" + seconds.ToString();
			else
				secondsText.text = seconds.ToString();
		}

		if (levelContent.activeSelf) {
			if (InputManager.Esc() || Input.GetKeyDown(KeyCode.Escape)) {
				clock = false;
				prevTime = currentTime;

				levelContent.SetActive (false);

				levelMenu = Instantiate (originalLevelMenu);
			}
		}
	}

	public float CurrentTime() {
		return currentTime;
	}

	public void StopTime() {
		clock = false;
	}

	/*
	 * Fortführen des Levels im aktuellen Zustand.
	 */
	public void ContinueLevel() {
		Destroy (levelMenu);

		levelContent.SetActive (true);

		clock = true;
		startTime = Time.timeSinceLevelLoad;
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
	public void RestartLevel() {
		Destroy (restartContent);
		restartContent = Instantiate (originalRestartContent);
		restartContent.transform.SetParent (levelContent.transform);
	}

	public void PlayWinMusic() {
		levelMusic.SetActive (false);
		winMusic.SetActive (true);
	}
}
