using UnityEngine;
using System.Collections;

public class MainMenuController : Singleton<MainMenuController> {

	public void LoadLevel()
	{
		LoadingController.Instance.LoadLevel(LoadingController.Level.TEST);
	}

}
