using UnityEngine;
using System.Collections;

/*
 * Abstrakte Klasse von der alle Menüs erben.
 * 
 * Alle erbenden Klassen müssen die Methode DoSetting() implementieren,
 * als einheitliches Interface für den SettingMenuController.
 */
public class AbstractMenu : MonoBehaviour {

	/*
	 * Diese Methode wird vom SettingMenuController aufgerufen, wenn ein Menüpunkt ausgeählt wird.
	 */
	public virtual void DoSetting() {}
}
