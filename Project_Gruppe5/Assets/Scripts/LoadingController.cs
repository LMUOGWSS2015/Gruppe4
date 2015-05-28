using UnityEngine;
using System.Collections;

public class LoadingController : Singleton<LoadingController> {

	public enum Level 
	{
		PLAYER = -1,
		TEST = 0
	}

	public enum Menu 
	{
		MAIN = 0
	}

	public override void StartMe()
	{
		DontDestroyOnLoad (this);
	}

	public void LoadMenu (Menu menu)
	{
		string sceneName;

		switch (menu) {
		case Menu.MAIN:
			sceneName = "MainMenu";
			break;
		default:
			sceneName = "";
			break;
		}

		StartCoroutine (Load(sceneName));
	}

	public void LoadLevel (Level level)
	{
		string levelName = "Level_" + (int)level;
		StartCoroutine(Load(levelName));
	}

	private IEnumerator Load(string sceneName)
	{
		AsyncOperation async = Application.LoadLevelAsync(sceneName);
		yield return async;
		Debug.Log("Loading of " + sceneName + " complete.");
	}

}
