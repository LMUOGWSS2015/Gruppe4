using UnityEngine;
using System.Collections;

public class ControllerMenu : AbstractMenu {

	private InputManager.Controller[] controllers;

	void Start() {
		controllers = new InputManager.Controller[] {InputManager.Controller.XBOX,
			InputManager.Controller.PS2, InputManager.Controller.MOUSE};
	}

	public override void DoSetting() {
		if (settingMenuController.getCurrentSetting () < controllers.Length) {
			InputManager.controller = controllers [settingMenuController.getCurrentSetting ()];
			Debug.Log ("Set Controller: " + controllers [settingMenuController.getCurrentSetting ()]);


		}
		else {
			Application.LoadLevel("SettingMenu");
		}
	}
}
