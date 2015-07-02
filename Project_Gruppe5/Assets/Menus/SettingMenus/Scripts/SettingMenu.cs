using UnityEngine;
using System.Collections;

public class SettingMenu : AbstractMenu {

	private LoadingController.Scene[] scenes;

	void Start() {
		scenes = new LoadingController.Scene[] {LoadingController.Scene.CONTROLLER_MENU, LoadingController.Scene.GRAPHICS_MENU,
			LoadingController.Scene.SOUND_MENU, LoadingController.Scene.MAIN_MENU};
	}

	public override void DoSetting() {
		LoadingController.Instance.LoadScene(scenes[settingMenuController.getCurrentSetting()]);
	}
}
