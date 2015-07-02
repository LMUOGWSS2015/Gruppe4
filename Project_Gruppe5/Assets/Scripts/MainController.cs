using UnityEngine;
using System.Collections;

public class MainController : Singleton<MainController> {

	public LoadingController.Scene startScene;

	public LoadingController loadingControllerPrefab;
	private LoadingController loadingController;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this);

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
}
