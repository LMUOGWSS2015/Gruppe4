using UnityEngine;
using System.Collections;

public class MainController : Singleton<MainController> {

	public LoadingController.Scene startScene;

	public LoadingController loadingControllerPrefab;
	private LoadingController loadingController;

	private AudioSource sound;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this);

		sound = GetComponent<AudioSource> ();

		InitControllers();
		InitGame();
	}

	private void InitControllers()
	{
		loadingController = GameObject.Instantiate(loadingControllerPrefab);
		loadingController.StartMe();
	}

	private void InitGame()
	{
		LoadingController.Instance.LoadScene (startScene);
	}

	public void PlaySound(AudioClip clip) {
		sound.clip = clip;
		sound.Play ();
	}
}
