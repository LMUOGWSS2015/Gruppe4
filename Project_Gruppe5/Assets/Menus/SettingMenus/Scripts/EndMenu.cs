using UnityEngine;
using System.Collections;

public class EndMenu : AbstractMenu {

	public override void DoSetting() {
		if (settingMenuController.getCurrentSetting () == 0) {
			LoadingController.Instance.LoadScene(LevelController.Instance.levelName);
		}
		else if (settingMenuController.getCurrentSetting () == 1) {
			LoadingController.Instance.LoadScene(LoadingController.Scene.MAIN_MENU);
		}
	}
}
