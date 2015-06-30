using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	public enum Controller {
		MOUSE,
		XBOX,
		PS2
	}

	public static Controller controller = Controller.XBOX;

	public static float rotationSensivity = 2.0f;
	public static float zoomSensivity = 2.0f;

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

	public static bool GazeTrigger() {
		switch (controller) {
		case Controller.MOUSE:
			return Input.GetButtonDown("Trigger");
		case Controller.XBOX:
			return Input.GetButtonDown("XBOXTrigger");
		case Controller.PS2:
			return Input.GetButtonDown("PS2Trigger");
		default:
			return false;
		}
	}
}
