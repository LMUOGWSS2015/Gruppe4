using UnityEngine;
using System.Collections;

/*
 * Startet das Spiel.
 */
[RequireComponent(typeof(AudioSource))]
public class MainController : Singleton<MainController> {

	public LoadingController.Scene startScene; // Die Szene mit der das Spiel starten soll.

	public LoadingController loadingControllerPrefab; // Prefab für den LoadingController.
	private LoadingController loadingController; // Instanz des LoadingController.

	public GameObject menuMusic;
	private AudioSource sound; // AudioSource zum Abspielen von Sounds im Menü.
	
	void Start () 
	{
		DontDestroyOnLoad (this);

		sound = GetComponent<AudioSource> ();

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
