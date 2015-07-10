using UnityEngine;
using System.Collections;

/*
 * Startet das Spiel.
 */
[RequireComponent(typeof(AudioSource))]
public class MainController : Singleton<MainController> {

	public static string DESERT_TIME = "DesertTime";
	public static string ICE_TIME = "IceTime";
	public static string THORN_TIME = "ThornTime";
	public static string FOREST_TIME = "ForestTime";

	public LoadingController.Scene startScene; // Die Szene mit der das Spiel starten soll.

	public LoadingController loadingControllerPrefab; // Prefab für den LoadingController.
	private LoadingController loadingController; // Instanz des LoadingController.

	public GameObject menuMusic;
	private AudioSource sound; // AudioSource zum Abspielen von Sounds im Menü.
	
	void Start () 
	{
		DontDestroyOnLoad (this);

		sound = GetComponent<AudioSource> ();

		if (PlayerPrefs.HasKey (InputManager.CONTROLLER))
			InputManager.controller = InputManager.Controllers ()[PlayerPrefs.GetInt (InputManager.CONTROLLER)];
		else
			PlayerPrefs.SetInt (InputManager.CONTROLLER, 0);

		InitControllers();
		InitGame();
	}

	/*
	 * Instanziiert den LoadingController.
	 */
	private void InitControllers()
	{
		loadingController = GameObject.Instantiate(loadingControllerPrefab);
		loadingController.StartMe();
	}

	/*
	 * Lädt die erste Szene des Spiels.
	 */
	private void InitGame()
	{
		LoadingController.Instance.LoadScene (startScene);
	}

	/*
	 * Spielt einen AudioClip ab.
	 */
	public void PlaySound(AudioClip clip) {
		sound.clip = clip;
		sound.Play ();
	}

	public void PlayMusic() {
		menuMusic.SetActive(true);
	}

	public void StopMusic() {
		menuMusic.SetActive (false);;
	}
}
