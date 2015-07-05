using UnityEngine;
using System.Collections;

/*
 * Verwaltet und behandelt die Controller- und Maus- bzw. Tastatur-Eingabe.
 */
public class InputManager : MonoBehaviour {

	/*
	 * Liste der verfügbaren Controller.
	 */
	public enum Controller {
		MOUSE,
		XBOX,
		PS2
	}

	public static Controller controller = Controller.MOUSE; // Der aktive Controller.

	public static float rotationSensivity = 2.0f;
	public static float zoomSensivity = 2.0f;

	/*
	 * Gibt den Wert der horizontalen Bewegung des Spielers zurück.
	 */
	public static float Horizontal() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetAxis("Horizontal");
		case Controller.XBOX:
			return Input.GetAxis("XBOXHorizontal");
		case Controller.PS2:
			return Input.GetAxis("PS2Horizontal");
		default:
			return 0;
		}
	}

	/*
	 * Gibt den Wert der vertikalen Bewegung des Spielers zurück.
	 */
	public static float Vertical() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetAxis("Vertical");
		case Controller.XBOX:
			return Input.GetAxis("XBOXVertical");
		case Controller.PS2:
			return Input.GetAxis("PS2Vertical");
		default:
			return 0;
		}
	}

	/*
	 * Gibt true zurück wenn der Spieler die Sprung-Taste drückt.
	 */
	public static bool Jump() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetButtonDown("Jump");
		case Controller.XBOX:
			return Input.GetButtonDown("XBOXJump");
		case Controller.PS2:
			return Input.GetButtonDown("PS2Jump");
		default:
			return false;
		}
	}

	/*
	 * Gibt den Wert der Rotation um die X-Achse zurück.
	 */
	public static float RotateX() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetAxis("RotateX");
		case Controller.XBOX:
			return Input.GetAxis("XBOXRotateX");
		case Controller.PS2:
			return Input.GetAxis("PS2RotateX");
		default:
			return 0;
		}
	}

	/*
	 * Gibt den Wert der Rotation um die Y-Achse zurück.
	 */
	public static float RotateY() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetAxis("RotateY");
		case Controller.XBOX:
			return Input.GetAxis("XBOXRotateY");
		case Controller.PS2:
			return Input.GetAxis("PS2RotateY");
		default:
			return 0;
		}
	}

	/*
	 * Gibt den Wert des Zooms zurück.
	 */
	public static float Zoom() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetAxis("Zoom");
		case Controller.XBOX:
			return Input.GetAxis("XBOXZoom");
		case Controller.PS2:
			return Input.GetAxis("PS2Zoom");
		default:
			return 0;
		}
	}

	/*
	 * Gibt true zurück wenn der Spieler die GazeTrigger-Taste drückt.
	 */
	public static bool GazeTrigger() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetButtonDown("GazeTrigger");
		case Controller.XBOX:
			return Input.GetButtonDown("XBOXGazeTrigger");
		case Controller.PS2:
			return Input.GetButtonDown("PS2GazeTrigger");
		default:
			return false;
		}
	}

	/*
	 * Gibt true zurück wenn der Spieler die Next-Taste drückt.
	 */
	public static bool Next() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetButtonDown("Next");
		case Controller.XBOX:
			return Input.GetButtonDown("XBOXNext");
		case Controller.PS2:
			return Input.GetButtonDown("PS2Next");
		default:
			return false;
		}
	}

	/*
	 * Gibt true zurück wenn der Spieler die Prev-Taste drückt.
	 */
	public static bool Prev() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetButtonDown("Prev");
		case Controller.XBOX:
			return Input.GetButtonDown("XBOXPrev");
		case Controller.PS2:
			return Input.GetButtonDown("PS2Prev");
		default:
			return false;
		}
	}

	/*
	 * Gibt true zurück wenn der Spieler die Abbruch-Taste drückt.
	 */
	public static bool Esc() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetButtonDown("ESC");
		case Controller.XBOX:
			return Input.GetButtonDown("XBOXESC");
		case Controller.PS2:
			return Input.GetButtonDown("PS2ESC");
		default:
			return false;
		}
	}
}
