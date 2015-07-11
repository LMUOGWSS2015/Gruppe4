using UnityEngine;
using System.Collections;

/*
 * Implementiert das Controller Auswahlmenü.
 */
public class ControllerMenu : AbstractMenu {

	void Start() {
		int currentController = PlayerPrefs.GetInt (InputManager.CONTROLLER);
		for (int i=0; i<InputManager.Controllers().Length; i++) {
			if (currentController == i) {
				SettingMenuController.Instance.settingTexts[i] = "Controller Activated";
			}
			else {
				SettingMenuController.Instance.settingTexts[i] = "Choose Controller";
			}
		}

		if (SettingMenuController.Instance.getCurrentSetting() == currentController)
			SettingMenuController.Instance.settingText.text = "Controller Activated";
		else
			SettingMenuController.Instance.settingText.text = "Choose Controller";
	}

	/*
	 * Setzt den entsprächenden Controller im InputManager aktiv, der im Menü ausgewählt wurde
	 * oder lädt das SettingMenu.
	 */
	public override void DoSetting() {
		if (SettingMenuController.Instance.getCurrentSetting () < InputManager.Controllers().Length) {
			if (SettingMenuController.Instance.getCurrentSetting () != PlayerPrefs.GetInt(InputManager.CONTROLLER)) {
				InputManager.controller = InputManager.Controllers() [SettingMenuController.Instance.getCurrentSetting ()];
				PlayerPrefs.SetInt(InputManager.CONTROLLER, SettingMenuController.Instance.getCurrentSetting ());

				SettingMenuController.Instance.settingText.text = "Controller Activated";

				int currentController = PlayerPrefs.GetInt (InputManager.CONTROLLER);
				for (int i=0; i<InputManager.Controllers().Length; i++) {
					if (currentController == i) {
						SettingMenuController.Instance.settingTexts[i] = "Controller Activated";
					}
					else {
						SettingMenuController.Instance.settingTexts[i] = "Choose Controller";
					}
				}
			}
		}
		else {
			LoadingController.Instance.LoadScene(LoadingController.Scene.MAIN_MENU);;
		}
	}
}
