using UnityEngine;
using System.Collections;

public class SettingMenu : AbstractMenu {

	private string[] scenes;

	void Start() {
		scenes = new string[] {"ControllerMenu", "GraphicsMenu", "SoundMenu", "MainMenu"};
	}

	public override void DoSetting() {
		Application.LoadLevel (scenes[settingMenuController.getCurrentSetting()]);
	}
}
