using UnityEngine;
using System.Collections;

public class ControllerMenu : AbstractMenu {

	private InputManager.Controller[] controllers;
	private bool[] settingState;

	void Start() {
		controllers = new InputManager.Controller[] {InputManager.Controller.XBOX,
			InputManager.Controller.PS2, InputManager.Controller.MOUSE};

		settingState = new bool[] {true, false, false};
	}

	public override void DoSetting() {
		if (settingMenuController.getCurrentSetting () < controllers.Length) {
			InputManager.controller = controllers [settingMenuController.getCurrentSetting ()];

			if (!settingState[settingMenuController.getCurrentSetting ()]) {
				for(int i=0; i<settingState.Length; i++) {
					settingState[i] = false;
					settingMenuController.settingTexts[i] = "Choose Controller";
				}
				settingState[settingMenuController.getCurrentSetting ()] = true;
				settingMenuController.settingText.text = "Controller Activated";

				for(int i=0; i<settingState.Length; i++) {
					if (settingState[i]) {
						settingMenuController.settingTexts[i] = "Controller Activated";
					}
				}
			}

			Debug.Log ("Set Controller: " + controllers [settingMenuController.getCurrentSetting ()]);


		}
		else {
			Application.LoadLevel("SettingMenu");
		}
	}
}
