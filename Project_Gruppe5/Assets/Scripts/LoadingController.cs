using UnityEngine;
using System.Collections;

/*
 * Verwaltet und Lädt alle Szenen des Spiels.
 */
public class LoadingController : Singleton<LoadingController> {

	/*
	 * Die Szenen-Namen aller Szenen des Spiels.
	 */
	public static string DESERT_LEVEL = "DesertLevel";
	public static string THORN_LEVEL = "Thorns_1";
	public static string FOREST_LEVEL = "ForestLevel";
	public static string ICE_LEVEL = "IceLevel";
	public static string MAIN_MENU = "MainMenu";
	public static string CONTROLLER_MENU = "ControllerMenu";
	public static string SETTING_MENU = "SettingMenu";
	public static string GRAPHICS_MENU = "GraphicsMenu";
	public static string SOUND_MENU = "SoundMenu";

	/*
	 * Eine Liste aller Szenen des Spiels.
	 */
	public enum Scene
	{
		DESERT_LEVEL = 0,
		THORN_LEVEL = 1,
		FOREST_LEVEL = 2,
		ICE_LEVEL = 3,
		CONTROLLER_MENU = 4,
		MAIN_MENU = 5,
		SETTING_MENU = 6,
		GRAPHICS_MENU = 7,
		SOUND_MENU = 8
	}

	public override void StartMe()
	{
		DontDestroyOnLoad (this);
	}

	/*
	 * Ermittelt den Szene-Namen einer Szene die geladen werden soll.
	 * 
	 * Argument: die Szene als Scene-Enum-Element
	 */
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
		case Scene.CONTROLLER_MENU:
			sceneName = CONTROLLER_MENU;
			break;
		case Scene.SETTING_MENU:
			sceneName = SETTING_MENU;
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

	/*
	 * Ermittelt den Szene-Namen einer Szene die geladen werden soll.
	 * 
	 * Argument: der Index der Szene
	 */
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
		case (int)Scene.CONTROLLER_MENU:
			sceneName = CONTROLLER_MENU;
			break;
		case (int)Scene.SETTING_MENU:
			sceneName = SETTING_MENU;
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

	/*
	 * Lädt die Szene mit dem angegebenen Szenen-Namen.
	 */
	private void Load(string sceneName)
	{
		//AsyncOperation async = Application.LoadLevelAsync(sceneName);
		//yield return async;

		Application.LoadLevel (sceneName);

		if (sceneName.Contains ("Menu")) {
			if (!MainController.Instance.menuMusic.activeSelf) {
				MainController.Instance.PlayMusic();
			}
		}
		else {
			MainController.Instance.StopMusic();
		}
	}

}
