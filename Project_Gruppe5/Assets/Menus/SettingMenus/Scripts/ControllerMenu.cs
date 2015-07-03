using UnityEngine;
using System.Collections;

/*
 * Implementiert das Controller Auswahlmenü.
 */
public class ControllerMenu : AbstractMenu {

	private InputManager.Controller[] controllers; // Die verfügbaren Controller.
	private bool[] settingState; // Gibt an welcher Controller gerade ausgewählt ist.

	void Start() {
		controllers = new InputManager.Controller[] {InputManager.Controller.XBOX,
			InputManager.Controller.PS2, InputManager.Controller.MOUSE};

		settingState = new bool[] {true, false, false};
	}

	/*
	 * Setzt den entsprächenden Controller im InputManager aktiv, der im Menü ausgewählt wurde
	 * oder lädt das SettingMenu.
	 */
	public override void DoSetting() {
		if (SettingMenuController.Instance.getCurrentSetting () < controllers.Length) {
			InputManager.controller = controllers [SettingMenuController.Instance.getCurrentSetting ()];

			if (!settingState[SettingMenuController.Instance.getCurrentSetting ()]) {
				for(int i=0; i<settingState.Length; i++) {
					settingState[i] = false;
					SettingMenuController.Instance.settingTexts[i] = "Choose Controller";
				}
				settingState[SettingMenuController.Instance.getCurrentSetting ()] = true;
				SettingMenuController.Instance.settingText.text = "Controller Activated";

				for(int i=0; i<settingState.Length; i++) {
					if (settingState[i]) {
						SettingMenuController.Instance.settingTexts[i] = "Controller Activated";
					}
				}
			}
		}
		else {
			LoadingController.Instance.LoadScene(LoadingController.Scene.SETTING_MENU);;
		}
	}
}
