using UnityEngine;
using System.Collections;

public class EndMenu : AbstractMenu {

	private LevelController levelController;
	
	void Start() {
		levelController = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<LevelController> ();
	}

	public override void DoSetting() {
		if (settingMenuController.getCurrentSetting () == 0) {
			Application.LoadLevel(levelController.levelName);
		}
		else if (settingMenuController.getCurrentSetting () == 1) {
			Application.LoadLevel ("MainMenu");
		}
	}
}
