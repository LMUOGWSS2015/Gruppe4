using UnityEngine;
using System.Collections;

public class PauseMenu : AbstractMenu {

	private LevelController levelController;

	void Start() {
		levelController = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<LevelController> ();
	}

	public override void DoSetting() {
		if (settingMenuController.getCurrentSetting () == 0) {
			levelController.ContinueLevel();
		}
		else if (settingMenuController.getCurrentSetting () == 1) {
			LoadingController.Instance.LoadScene(LoadingController.Scene.MAIN_MENU);
		}
	}
}
