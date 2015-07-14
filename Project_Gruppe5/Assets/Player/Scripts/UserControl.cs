using System;
using UnityEngine;

/*
 * Behandelt die Nutzereingabe.
 */
[RequireComponent(typeof(Player))]
public class UserControl : MonoBehaviour
{
	private Player player; 			  		// A reference to the Player
	private Transform cam;                  // A reference to the main camera in the scenes transform
	private Vector3 camForward;             // The current forward direction of the camera
	private Vector3 move;					// the world-relative desired move direction, calculated from the camForward and user input.
	private bool jump;                      // true if the User pressed the jump button

	private void Start() {
		// get the third person character ( this should never be null due to require component )
		player = GetComponent<Player>();
	}
	
	
	private void Update() {
		if (cam == null) {
			// get the transform of the main camera
			if (Camera.main != null) {
				cam = Camera.main.transform;
			}
			else {
				Debug.LogWarning(
					"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}
		}

		if (!jump) {
			jump = InputManager.Jump();
		}
	}
	
	
	// Fixed update is called in sync with physics
	private void FixedUpdate() {
		// read inputs
		float h = InputManager.Horizontal ();
		float v = InputManager.Vertical ();
		
		// calculate move direction to pass to character
		if (cam != null) {
			// calculate camera relative direction to move:
			camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
			move = v*camForward + h*cam.right;
		}
		else {
			// we use world-relative directions in the case of no main camera
			move = v*Vector3.forward + h*Vector3.right;
		}

		// pass all parameters to the character control script
		player.Move(move, jump);
		jump = false;
	}
}