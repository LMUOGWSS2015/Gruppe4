using UnityEngine;
using System.Collections;

/*
 * Implementiert das Pause-Menü während eins Levels.
 */
public class PauseMenu : AbstractMenu {

	/*
	 * Setzt das Level fort oder
	 * startet das Level beim letzten CheckPoint neu oder
	 * lädt das MainMenu.
	 */
	public override void DoSetting() {
		if (SettingMenuController.Instance.getCurrentSetting () == 0) {
			LevelController.Instance.ContinueLevel ();
		}
		else if (SettingMenuController.Instance.getCurrentSetting () == 1) {
			Player.Instance.Kill ();
			LevelController.Instance.ContinueLevel ();
		}
		else if (SettingMenuController.Instance.getCurrentSetting () == 2) {
			LoadingController.Instance.LoadScene(LoadingController.Scene.MAIN_MENU);
		}
	}
}
