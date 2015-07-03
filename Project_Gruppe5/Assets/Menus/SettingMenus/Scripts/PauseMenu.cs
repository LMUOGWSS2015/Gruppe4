using UnityEngine;
using System.Collections;

public class PauseMenu : AbstractMenu {

	public override void DoSetting() {
		if (settingMenuController.getCurrentSetting () == 0) {
			LevelController.Instance.ContinueLevel ();
		}
		else if (settingMenuController.getCurrentSetting () == 1) {
			Player.Instance.Kill ();
			LevelController.Instance.ContinueLevel ();
		}
		else if (settingMenuController.getCurrentSetting () == 2) {
			LoadingController.Instance.LoadScene(LoadingController.Scene.MAIN_MENU);
		}
	}
}
