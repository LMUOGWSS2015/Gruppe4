using UnityEngine;
using System.Collections;

/*
 * Implementiert das Menü zur Auswahl der verschiedenen Einstellungen.
 */
public class SettingMenu : AbstractMenu {

	private LoadingController.Scene[] scenes; // Die Menüpunkte, die ausgewählt werden können.

	void Start() {
		scenes = new LoadingController.Scene[] {LoadingController.Scene.CONTROLLER_MENU, LoadingController.Scene.GRAPHICS_MENU,
			LoadingController.Scene.SOUND_MENU, LoadingController.Scene.MAIN_MENU};
	}

	/*
	 * Lädt das ausgewählte Menü.
	 */
	public override void DoSetting() {
		LoadingController.Instance.LoadScene(scenes[SettingMenuController.Instance.getCurrentSetting()]);
	}
}
