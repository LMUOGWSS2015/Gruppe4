using UnityEngine;
using System.Collections;

/*
 * Implementiert das Menü am Ende eines Levels.
 */
public class EndMenu : AbstractMenu {

	/*
	 * Startet das aktuelle Level neu
	 * oder lädt das MainMenu.
	 */
	public override void DoSetting() {
		if (SettingMenuController.Instance.getCurrentSetting () == 0) {
			LoadingController.Instance.LoadScene(LevelController.Instance.levelName);
		}
		else if (SettingMenuController.Instance.getCurrentSetting () == 1) {
			LoadingController.Instance.LoadScene(LoadingController.Scene.MAIN_MENU);
		}
	}
}
