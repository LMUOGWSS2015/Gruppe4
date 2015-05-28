using UnityEngine;
using System.Collections;

public class MainController : Singleton<MainController> {

	public enum State {
		PLAYER_TEST = -2,
		MAIN_MENU = -1,
		START = 0,
		TEST_LEVEL = 1
	}

	public State state;

	public LoadingController loadingControllerPrefab;
	private LoadingController loadingController;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this);

		InitControllers();
		InitGame();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.L)) {
			loadingController.LoadLevel(LoadingController.Level.TEST);
		}

		if(Input.GetKeyDown (KeyCode.M)) {
			loadingController.LoadMenu(LoadingController.Menu.MAIN);
		}
	}

	private void InitControllers()
	{
		loadingController = GameObject.Instantiate(loadingControllerPrefab);
		loadingController.StartMe();
	}

	private void InitGame()
	{
		switch(state) {
		case State.MAIN_MENU:
			loadingController.LoadMenu(LoadingController.Menu.MAIN);
			break;
		case State.TEST_LEVEL:
			loadingController.LoadLevel(LoadingController.Level.TEST);
			break;
		case State.PLAYER_TEST:
			loadingController.LoadLevel(LoadingController.Level.PLAYER);
			break;
		}
	}
}
