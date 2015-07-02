﻿using UnityEngine;
using System.Collections;

public class LoadingController : Singleton<LoadingController> {

	public static string DESERT_LEVEL = "deserttest 1";
	public static string THORN_LEVEL = "Thorns_1";
	public static string FOREST_LEVEL = "treetest3";
	public static string ICE_LEVEL = "PlayerTestScene";
	public static string SETTING_MENU = "SettingMenu";
	public static string MAIN_MENU = "MainMenu";
	public static string CONTROLLER_MENU = "ControllerMenu";
	public static string GRAPHICS_MENU = "GraphicsMenu";
	public static string SOUND_MENU = "SoundMenu";

	public enum Scene
	{
		DESERT_LEVEL = 0,
		THORN_LEVEL = 1,
		FOREST_LEVEL = 2,
		ICE_LEVEL = 3,
		SETTING_MENU = 4,
		MAIN_MENU = 5,
		CONTROLLER_MENU = 6,
		GRAPHICS_MENU = 7,
		SOUND_MENU = 8
	}

	public override void StartMe()
	{
		DontDestroyOnLoad (this);
	}

	public void LoadScene (Scene scene)
	{
		string sceneName;

		switch (scene) {
		case Scene.DESERT_LEVEL:
			sceneName = DESERT_LEVEL;
			break;
		case Scene.THORN_LEVEL:
			sceneName = THORN_LEVEL;
			break;
		case Scene.FOREST_LEVEL:
			sceneName = FOREST_LEVEL;
			break;
		case Scene.ICE_LEVEL:
			sceneName = ICE_LEVEL;
			break;
		case Scene.MAIN_MENU:
			sceneName = MAIN_MENU;
			break;
		case Scene.SETTING_MENU:
			sceneName = SETTING_MENU;
			break;
		case Scene.CONTROLLER_MENU:
			sceneName = CONTROLLER_MENU;
			break;
		case Scene.GRAPHICS_MENU:
			sceneName = GRAPHICS_MENU;
			break;
		case Scene.SOUND_MENU:
			sceneName = SOUND_MENU;
			break;
		default:
			sceneName = "";
			break;
		}
		
		//StartCoroutine(Load(sceneName));
		Load (sceneName);
	}

	public void LoadScene (int scene)
	{
		string sceneName;

		switch (scene) {
		case (int)Scene.DESERT_LEVEL:
			sceneName = DESERT_LEVEL;
			break;
		case (int)Scene.THORN_LEVEL:
			sceneName = THORN_LEVEL;
			break;
		case (int)Scene.FOREST_LEVEL:
			sceneName = FOREST_LEVEL;
			break;
		case (int)Scene.ICE_LEVEL:
			sceneName = ICE_LEVEL;
			break;
		case (int)Scene.MAIN_MENU:
			sceneName = MAIN_MENU;
			break;
		case (int)Scene.SETTING_MENU:
			sceneName = SETTING_MENU;
			break;
		case (int)Scene.CONTROLLER_MENU:
			sceneName = CONTROLLER_MENU;
			break;
		case (int)Scene.GRAPHICS_MENU:
			sceneName = GRAPHICS_MENU;
			break;
		case (int)Scene.SOUND_MENU:
			sceneName = SOUND_MENU;
			break;
		default:
			sceneName = "";
			break;
		}
		
		//StartCoroutine(Load(sceneName));
		Load (sceneName);
	}

	private void Load(string sceneName)
	{
		//AsyncOperation async = Application.LoadLevelAsync(sceneName);
		//yield return async;

		Application.LoadLevel (sceneName);

		Debug.Log("Loading of " + sceneName + " complete.");
	}

}
